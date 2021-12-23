using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInRoom : MonoBehaviour
{
    [SerializeField] float reTime;

    public bool isObserve;

    [SerializeField] GameObject sight;

    PlayerController player;

    public SpriteRenderer window;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(Observe());
    }


    IEnumerator Observe()
    {
        yield return null;
        while (true)
        {
            yield return null;

            yield return new WaitForSeconds(reTime);

            isObserve = true;
            sight.SetActive(true);
            window.color = new Color(0, 104, 255);

            yield return new WaitForSeconds(reTime);

            window.color = new Color(0, 0,0);

            isObserve = false;
            sight.SetActive(false);
            player.canMove_master = true;
        }
    }
}
