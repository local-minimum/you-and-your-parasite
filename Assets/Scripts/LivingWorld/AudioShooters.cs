using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class AudioShooters : MonoBehaviour {

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    float[] volumes;

    AudioSource aSource;

    [SerializeField, Range(0, 10)]
    float minDelay;

    [SerializeField, Range(0, 20)]
    float maxDelay;

    float nextShot;

    bool waitingToPlay = false;

	void Start () {
        aSource = GetComponent<AudioSource>();
        aSource.loop = false;
	}
	
	void Update () {
        if (waitingToPlay && Time.timeSinceLevelLoad > nextShot)
            Shoot();
        else if (!waitingToPlay && !aSource.isPlaying)
            SetNextStart();

	}

    void SetNextStart()
    {
        nextShot = Time.timeSinceLevelLoad + Random.Range(minDelay, maxDelay);
        waitingToPlay = true;
    }

    void Shoot()
    {
        waitingToPlay = false;
        var index = Random.Range(0, clips.Length);
        aSource.clip = clips[index];
        aSource.Play();
    }
}
