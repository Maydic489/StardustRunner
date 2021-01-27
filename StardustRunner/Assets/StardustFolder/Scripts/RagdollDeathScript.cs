using UnityEngine;

public class RagdollDeathScript : MonoBehaviour
{
    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;

    private void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool state)
    {
        foreach(Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
            rb.detectCollisions = state;
        }

        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
    }
}
