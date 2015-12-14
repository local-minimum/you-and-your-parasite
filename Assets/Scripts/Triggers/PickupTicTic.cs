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
        SceneManager.LoadScene(introScene, LoadSceneMode.Additive);
        introing = true;
    }

    void Update()
    {
        if (introing && SceneManager.sceneCount == 1)
        {

            Debug.Log("Giving quest");

            questGiver.QueueFirstQuest();
            GameMonitor.AllowPlayerToWalk = true;
            gameObject.SetActive(false);

        }
        else if (questAudioSource != null && Time.timeSinceLevelLoad - delayStart > delayedClip)
        {

            questAudioSource.PlayOneShot(clip, clipVolume);
            questAudioSource = null;
        }
    }
}
