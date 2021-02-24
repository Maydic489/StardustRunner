using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    public float delayInSec;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", delayInSec);   
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
