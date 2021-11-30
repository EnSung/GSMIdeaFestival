using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningObject : MonoBehaviour
{

    public virtual void Scan(PlayerController player)
    {
        Debug.Log(this.name);
    }
}
