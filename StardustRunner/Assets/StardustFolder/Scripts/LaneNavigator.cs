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
            if (other.gameObject.tag != "Obstacle") { return; }

            if (whatLane == 'm' && !firstTurn)
            {
                transform.position = new Vector3(-1.6f, transform.position.y, transform.position.z);
                colliderComp.center = new Vector3(0, 0, 2f);
                colliderComp.size = new Vector3(1, 1, 6.5f);
                firstTurn = true;

                if (confirmCo != null)
                    StopCoroutine(confirmCo);
                confirmCo = StartCoroutine(Confirm());
            }
            else if (whatLane == 'r' && !firstTurn)
            {
                transform.position = new Vector3(1.6f, transform.position.y, transform.position.z);
                ResetCollider();
            }

            if (whatLane == 'l' && firstTurn && safeLane != 'l')
            {
                ResetCollider();
                transform.position = new Vector3(1.6f, transform.position.y, transform.position.z);

                if (confirmCo != null)
                    StopCoroutine(confirmCo);
                confirmCo = StartCoroutine(Confirm());
            }
        }

        public IEnumerator DelayReset()
        {
            yield return new WaitForSeconds(3/LevelManager.Instance.Speed);
            ResetCollider();
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            isReset = false;
            firstTurn = false;
            Debug.Log("back to center");
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
            colliderComp.size = new Vector3(1, 1, 2.5f);
        }
    }
}
