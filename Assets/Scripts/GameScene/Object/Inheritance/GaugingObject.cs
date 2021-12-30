using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugingObject : ScanningObject
{

    bool flag;
    float curGauge;
    public float maxGauge;

    public float incleaseGauge;

    public float decreaseSpeed;
    float time;

    [SerializeField] Slider gaugeBar;
    protected virtual void Start()
    {
        gaugeBar.maxValue = maxGauge;
    }

    protected virtual void Update()
    {

        time += Time.deltaTime;
        if (curGauge >= maxGauge)
        {
            complete();
        }

        if(time >=3  && !flag)
        {
           curGauge =  Mathf.Lerp(curGauge, 0, decreaseSpeed * Time.deltaTime);
            if(curGauge <= 0.1)
            {
                curGauge = 0;
            }
        }
        UI_update();
    }
    public override void Scan(PlayerController player)
    {

        curGauge += incleaseGauge;
        time = 0;
    }


    public void complete()
    {
        if (!flag)
        {
            completingPlayFunc();
        }
        flag = true;

    }

    public virtual void completingPlayFunc()
    {

    }

    public void UI_update()
    {
        if(curGauge == 0)
        {
            gaugeBar.gameObject.SetActive(false);
        }
        else
        {
            gaugeBar.gameObject.SetActive(true);
        }
        gaugeBar.value = curGauge;
    }
}
