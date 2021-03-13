using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class ShadowScript : MonoBehaviour
    {
        private bool inAir;
        public GameObject shockwaveFX;
        public AudioClip smackSFX;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter(other.gameObject);
        }

        protected virtual void TriggerEnter(GameObject collidingObject)
        {
            if (collidingObject.tag == "Road" && inAir)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;

                Instantiate(shockwaveFX, this.transform.position, shockwaveFX.transform.rotation, this.transform);

                if (Camera.main.GetComponent<CameraShake>() != null)
                {
                    Camera.main.GetComponent<CameraShake>().isShake = true;
                    Camera.main.GetComponent<CameraShake>().shakeDuration = 0.2f;
                }

                SoundManager.Instance.PlaySound(smackSFX, transform.position);
                inAir = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit(other.gameObject);
        }

        protected virtual void TriggerExit(GameObject collidingObject)
        {
            if (collidingObject.tag == "Ramp")
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                inAir = true;
            }
        }
    }
}
