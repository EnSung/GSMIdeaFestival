using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionScript : MonoBehaviour
{

    public GameObject[] Description;
    public int index;
    void Start()
    {
        
    }

    void Update()
    {
        Description[index].SetActive(true);

        for (int i = 0; i < Description.Length; i++)
        {
            if (i == index) continue;

            Description[i].SetActive(false);
        }
    }


    public void next()
    {
        if(index+1 != Description.Length)
        {
            index++;
        }
    }

    public void prev()
    {
        if (index  > 0)
        {
            index--;
        }
    }
}
