using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;


public class LaneManager : MonoBehaviour
{
    public int randomNum;
    [SerializeField]
    public int oldCount;
    [SerializeField]
    private List<GameObject> obstaclesList;
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
        if (collidingObject.tag != "Obstacle") { return; }

        obstaclesList.Add(collidingObject);

        if (obstaclesList.Count > 2 && (obstaclesList.Count - oldCount) >= 3)
            RandomCreateSpace();
        else if(obstaclesList.Count > 2)
            CreateSpace(collidingObject);
    }
    private void TriggerExit(GameObject collidingObject)
    {
        if (collidingObject.tag != "Obstacle") { return; }

        obstaclesList.Remove(collidingObject);
        oldCount = obstaclesList.Count;
    }

    private void RandomCreateSpace()
    {
        Debug.Log("random");
        randomNum = Random.Range(0, obstaclesList.Count);
        if (!PushMode)
            obstaclesList[randomNum].GetComponent<MMPoolableObject>().Destroy();
        else
            obstaclesList[randomNum].transform.position += Vector3.forward * 2.5f;

        oldCount = obstaclesList.Count;
        //obstaclesList.Remove(obstaclesList[randomNum]);
    }
    private void CreateSpace(GameObject surplus)
    {
        Debug.Log("not random");
        if (!PushMode)
            surplus.GetComponent<MMPoolableObject>().Destroy();
        else
            surplus.transform.position += Vector3.forward * 2.5f;

        oldCount = obstaclesList.Count;
        //obstaclesList.Remove(surplus);
    }
}
