using UnityEngine;
using System.Collections;

public class MusicSwitcher : MonoBehaviour {

    [SerializeField]
    AudioClip clipA;

    [SerializeField]
    AudioClip clipB;

    void OnTriggerEnter(Collider other)
    {
        var audioSource = other.GetComponentInChildren<AudioSource>();
        if (audioSource)
        {
            audioSource.Pause();
            audioSource.clip = audioSource.clip == clipA ? clipB : clipA;
            audioSource.Play();
        }
    }
}
