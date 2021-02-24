using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    public List<GameObject> tipsList;

    private void OnEnable()
    {
        tipsList[Random.Range(1, 4)].SetActive(true);
    }
}
