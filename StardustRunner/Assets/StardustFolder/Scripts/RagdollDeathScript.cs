using UnityEngine;

public class RagdollDeathScript : MonoBehaviour
{
    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;

    private void Awake()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool state)
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
            rb.detectCollisions = state;
            rb.freezeRotation = !state;
            if (!state)
            {
                rb.Sleep();
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
                rb.constraints = RigidbodyConstraints.None;
        }

        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
    }
}
