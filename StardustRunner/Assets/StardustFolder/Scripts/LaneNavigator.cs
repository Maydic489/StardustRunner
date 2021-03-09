using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class LaneNavigator : MonoBehaviour
    {
        public char whatLane;
        public char safeLane;
        public bool firstTurn;
        //private bool secondTurn;
        public bool isReset;
        private Coroutine resetCo;
        private Coroutine confirmCo;
        public BoxCollider colliderComp;
        private int randomNum;

        private void Start()
        {
            safeLane = 'm';
            colliderComp = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            ChooseLane();
            if (whatLane == safeLane && !isReset && safeLane != 'm')
            {
                isReset = true;
                resetCo = StartCoroutine(DelayReset());
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag.Substring(0,3) != "Obs") { return; }

            switch(Random.Range(1,3))
            {
                case 1:
                    MidLaneChange(-1.6f);
                    break;
                case 2:
                    MidLaneChange(1.6f);
                    break;
                default:
                    break;
            }

            if (whatLane != 'm' && firstTurn && safeLane != whatLane)
            {
                ResetCollider();
                if(whatLane == 'l')
                    transform.position = new Vector3(1.6f, transform.position.y, transform.position.z);
                else if(whatLane == 'r')
                    transform.position = new Vector3(-1.6f, transform.position.y, transform.position.z);

                if (confirmCo != null)
                    StopCoroutine(confirmCo);
                confirmCo = StartCoroutine(Confirm());
            }
        }

        private void MidLaneChange(float direction)
        {
            if (whatLane == 'm' && !firstTurn)
            {
                transform.position = new Vector3(direction, transform.position.y, transform.position.z);
                colliderComp.center = new Vector3(0, 0, 2f);
                colliderComp.size = new Vector3(1, 1, 6.8f);
                firstTurn = true;

                if (confirmCo != null)
                    StopCoroutine(confirmCo);
                confirmCo = StartCoroutine(Confirm());
            }
            else if (whatLane != 'm' && !firstTurn)
            {
                if (whatLane == 'l')
                    transform.position = new Vector3(-1.6f, transform.position.y, transform.position.z);
                else if (whatLane == 'r')
                    transform.position = new Vector3(1.6f, transform.position.y, transform.position.z);
                ResetCollider();
            }
        }

        public IEnumerator DelayReset()
        {
            yield return new WaitForSeconds(3/LevelManager.Instance.Speed);
            ResetCollider();
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            isReset = false;
            firstTurn = false;
            if (confirmCo != null)
                StopCoroutine(confirmCo);
            confirmCo = StartCoroutine(Confirm());
        }

        public IEnumerator Confirm()
        {
            yield return new WaitForSeconds(3/LevelManager.Instance.Speed);
            ResetCollider();
            safeLane = whatLane;
            firstTurn = false;
            //secondTurn = false;
        }

        public void ChooseLane()
        {
            if (transform.position.x > -0.7f && transform.position.x < 0.7f)
            {
                whatLane = 'm';
            }
            else if (transform.position.x < -0.1f)
            {
                whatLane = 'l';
            }
            else if (transform.position.x > 0.1f)
            {
                whatLane = 'r';
            }
            else
            {
                return;
            }
        }

        private void ResetCollider()
        {
            colliderComp.center = new Vector3(0, 0, 0);
            colliderComp.size = new Vector3(1, 1, 2.7f);
        }
    }
}
