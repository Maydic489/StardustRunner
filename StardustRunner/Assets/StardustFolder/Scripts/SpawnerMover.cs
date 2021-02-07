using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMover : MonoBehaviour
{
    public int stayDuration;
    public float moveSpeed;
    public bool SmoothTurn;
    private int counting;
    protected float ranNum;
    protected float slideDirection;

    private void OnEnable()
    {
        counting = stayDuration;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        counting++;
        if(counting >= stayDuration)
        {
            GetSlideDirection();
            counting = 0;
        }

        if(!SmoothTurn)
            transform.position = new Vector3(slideDirection, transform.position.y, transform.position.z);
        else
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(slideDirection, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
    }

    public virtual void GetSlideDirection()
    {
        ranNum = Random.Range(0, 4);
        if (ranNum <= 1)
        {
            slideDirection = -1.6f;
        }
        else if (ranNum <= 2)
        {
            slideDirection = 0;
        }
        else if (ranNum <= 3)
        {
            slideDirection = 1.6f;
        }
    }
}
