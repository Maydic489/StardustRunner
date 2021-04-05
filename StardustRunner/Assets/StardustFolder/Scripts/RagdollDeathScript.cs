using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class RagdollDeathScript : MonoBehaviour
    {
        public Rigidbody[] ragdollBodies;
        public Collider[] ragdollColliders;

        private bool isFirstTime = true;

        private void Awake()
        {
            ragdollBodies = GetComponentsInChildren<Rigidbody>();
            ragdollColliders = GetComponentsInChildren<Collider>();

            ToggleRagdoll(false);
        }

        private void OnDisable()
        {
            ToggleRagdoll(false);
            //Debug.Log("check first time " + isFirstTime);
        }

        public void ToggleRagdoll(bool state)
        {
            ragdollBodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in ragdollBodies)
            {
                rb.isKinematic = !state;
                //rb.detectCollisions = state;
                rb.freezeRotation = !state;
                if (!state)
                {
                    rb.Sleep();
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                else
                    rb.constraints = RigidbodyConstraints.None;
            }

            if (!this.CompareTag("Obstacle_Car"))
            {
                foreach (Collider collider in ragdollColliders)
                {
                    collider.enabled = state;
                }
            }

            if (state)
                isFirstTime = false;
        }

        public void ResetIgnore()
        {
            foreach (Collider collider in this.GetComponent<RagdollDeathScript>().ragdollColliders)
            {
                if (LevelManager.Instance.CurrentPlayableCharacters != null)
                {
                    Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<BoxCollider>(), collider, false);
                    Physics.IgnoreCollision(LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<CapsuleCollider>(), collider, false);
                }
            }

        }
    }
}
