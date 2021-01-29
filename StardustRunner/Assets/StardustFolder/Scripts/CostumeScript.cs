using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeScript : MonoBehaviour
{
    public GameObject PickEffect;
    public void OnDisable()
    {
        if (PickEffect != null)
        {
            Instantiate(PickEffect, transform.parent.position, transform.rotation);
        }
    }
}

