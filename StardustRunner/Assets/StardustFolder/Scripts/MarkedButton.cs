using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkedButton : MonoBehaviour
{
    public CheckMarkMover checkMark;

    public void TriggerMove()
    {
        checkMark.GetComponent<CheckMarkMover>().Move(this.gameObject);
    }
}
