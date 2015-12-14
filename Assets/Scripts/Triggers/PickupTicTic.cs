using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PickupTicTic : MonoBehaviour {

    [SerializeField]
    string introScene;

    [SerializeField]
    AudioClip clip;

    float intoSceneDuration = .3f;

    bool introing = false;

    [SerializeField, Range(0, 2)]
    float delayedClip = 0.5f;

    [SerializeField, Range(0, 1)]
    float clipVolume = 0.8f;

    float delayStart;

    QuestGiver questGiver;
    AudioSource questAudioSource;


	void OnTriggerEnter(Collider other)
    {
        questGiver = other.GetComponentInChildren<QuestGiver>();
        questAudioSource = other.GetComponentInChildren<AudioSource>();

        if (questGiver)
            introSequence();

    }

    void introSequence()
    {
        delayStart = Time.timeSinceLevelLoad;
        GameMonitor.AllowPlayerToWalk = false;
        introing = true;
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        //SceneManager.LoadScene(introScene, LoadSceneMode.Additive);
        GameMonitor.WatchingMovie = true;
        Application.LoadLevelAdditive(introScene);
    }

    void Update()
    {
        if (introing && !GameMonitor.WatchingMovie) // SceneManager.sceneCount == 1)
        {

            Debug.Log("Giving quest");

            questGiver.QueueFirstQuest();
            GameMonitor.AllowPlayerToWalk = true;
            introing = false;

        }
        else if (questAudioSource != null && Time.timeSinceLevelLoad - delayStart > delayedClip)
        {

            questAudioSource.PlayOneShot(clip, clipVolume);
            questAudioSource = null;
        }
    }
}
