using UnityEngine;

/// <summary>
/// 싱글턴 베이스 클래스
/// </summary>
/// <typeparam name="T">T타입 제약조건 : Singleton을 상속받는 T 타입의 클래스</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    /// <summary>
    /// 싱글턴 인스턴스를 찾거나 생성하는 과정 중 다른 스레드에서 사용중인지 판단할 객체
    /// </summary>
    private static object syncObject = new object();

    /// <summary>
    /// T 타입 인스턴스 객체
    /// </summary>
    private static T instance;

    /// <summary>
    /// 외부에서 인스턴스 객체에 접근하기 위한 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            // 인스턴스가 없다면
            if (instance == null)
            {
                // lock을 통한 동기화 과정은 일반적으로 유니티의 싱글 스레드 환경에서는 상관없지만,
                // 멀티스레드 환경에서 싱글턴 초기화 과정을 스레드 세이프하게 하기 위함
                // (이 말은 즉 초기화 과정만 안전하고 그 이후 사용시 별도로 lock을 걸지 않는다면 안전하지 않다는 뜻)
                // 인스턴스를 찾거나 생성하는 과정을 진행 중일 때 다른 스레드에서 
                // 동일한 로직에 접근할 수 없게 락을 건다 (사용중이라면 끝날 때까지 대기)
                lock (syncObject)
                {
                    // 인스턴스를 찾는다.
                    instance = FindObjectOfType<T>();
                    // 찾았는데 없다면?
                    if (instance == null)
                    {
                        // 인스턴스를 새로 생성한다
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
            }

            // 위의 과정을 통해 인스턴스가 있다면 그대로 반환, 없다면 찾거나 생성해서 반환
            return instance;
        }
    }

    protected virtual void Awake()
    {
        lock (syncObject)
        {
            // 모노를 상속받았으므로 모든 객체의 초기화가 끝났을 때
            // Awake가 자동적으로 호출되는데 이 떄 검사를 통해 인스턴스가 없다면 미리 넣어둔다.
            // 이러한 방식으로 Instance 프로퍼티 접근 시 불필요한 검색 과정을 최대한 생략한다.
            if (instance == null)
            {
                instance = this as T;
            }
            // 만약 이 때 인스턴스가 존재한다면 객체가 초기화 되기전에 해당 인스턴스에 접근하였다는 것
            // 해당 클래스는 모노를 상속받았고 일반적으로 씬(하이라키)에 미리 올려서 사용함
            // 그럼 이 때 인스턴스가 존재한다는 것은 씬에 올려진 인스턴스가 초기화 되기전에
            // 타 클래스에서 싱글톤 객체에 접근하여 별도의 인스턴스가 추가적으로 생성되었다는 의미
            // 따라서, 추가로 생성된 인스턴스를 삭제하고 먼저 씬에 배치한 싱글턴 객체가 작동할 수 있도록 한다.
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        lock (syncObject)
        {
            if (instance != this)
                return;

            instance = null;
        }
    }
}