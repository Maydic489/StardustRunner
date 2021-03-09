using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;


public class LaneManager : MonoBehaviour
{
    private int randomNum;
    [SerializeField]
    private int oldCount;
    [SerializeField]
    private List<GameObject> obstaclesList;
    public int useLane;
    public int maxInLane = 1;
    public bool PushMode;
    

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        TriggerExit(other.gameObject);
    }

    private void TriggerEnter(GameObject collidingObject)
    {
        if (collidingObject.tag.Substring(0, 3) != "Obs") { return; }
        obstaclesList.Add(collidingObject);
        useLane += collidingObject.GetComponent<AdditionalProperties>().laneSize;

        if (useLane > maxInLane && (useLane - oldCount) >= maxInLane+1)
            RandomCreateSpace();
        else if(useLane > maxInLane)
            CreateSpace(collidingObject);
    }
    private void TriggerExit(GameObject collidingObject)
    {
        if (collidingObject.tag.Substring(0, 3) != "Obs") { return; }

        useLane -= collidingObject.GetComponent<AdditionalProperties>().laneSize;
        obstaclesList.Remove(collidingObject);
        oldCount = useLane;
    }

    private void RandomCreateSpace()
    {
        randomNum = Random.Range(0, obstaclesList.Count);
        if (!PushMode)
            obstaclesList[randomNum].GetComponent<MMPoolableObject>().Destroy();
        else
            obstaclesList[randomNum].transform.position += Vector3.forward * 2.5f;

        oldCount = useLane;
        //obstaclesList.Remove(obstaclesList[randomNum]);
    }
    private void CreateSpace(GameObject surplus)
    {
        if (!PushMode)
            surplus.GetComponent<MMPoolableObject>().Destroy();
        else
            surplus.transform.position += Vector3.forward * 2.5f;

        oldCount = useLane;
        //obstaclesList.Remove(surplus);
    }
}
