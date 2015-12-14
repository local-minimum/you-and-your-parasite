using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PickupTicTic : MonoBehaviour {

    [SerializeField]
    string introScene;

    float intoSceneDuration = .3f;

    bool introing = false;

    QuestGiver questGiver;

	void OnTriggerEnter(Collider other)
    {
        questGiver = other.GetComponentInChildren<QuestGiver>();

        if (questGiver)
            introSequence();

    }

    void introSequence()
    {
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
    }
}
