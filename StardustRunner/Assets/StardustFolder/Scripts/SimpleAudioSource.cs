using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioSource : MonoBehaviour
{
    private AudioSource thisAudio;
    private void Start()
    {
        thisAudio = this.GetComponent<AudioSource>();
    }

    public void PlayAudioSource()
    {
        MoreMountains.InfiniteRunnerEngine.SoundManager.Instance.PlaySoundSource(thisAudio, transform.position);
    }
}
