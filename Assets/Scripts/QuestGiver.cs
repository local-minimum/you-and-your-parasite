using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuestGiver : MonoBehaviour {

    [SerializeField]
    string talkScene;

    [SerializeField]
    DialogSpawner firstQuest;

    [SerializeField]
    List<DialogSpawner> otherQuests = new List<DialogSpawner>();

    [SerializeField]
    string[] upsetPhrases;

    [SerializeField, Range(0, 10)]
    float questDelay;

    float questSelectTime;

    DialogSpawner nextQuest;

    void Start()
    {
        firstQuest.allowed = false;
        for (int i = 0; i < otherQuests.Count; i++)
            otherQuests[i].allowed = false;
    }

	void Update () {
        if (nextQuest && Time.timeSinceLevelLoad - questSelectTime > questDelay)
            GiveQuest();
	}

    void GiveQuest()
    {
        nextQuest.allowed = true;
        Talker.PushMessage(nextQuest.text);
        SceneManager.LoadScene(talkScene, LoadSceneMode.Additive);
        if (nextQuest != firstQuest)
            otherQuests.Remove(nextQuest);
        nextQuest = null;
    }

    public void QueueFirstQuest() {
        if (nextQuest == null)
            nextQuest = firstQuest;

        questSelectTime = Time.timeSinceLevelLoad;
        transform.GetChild(0).gameObject.SetActive(true);    
    }

    public bool QueueQuest()
    {
        if (otherQuests.Count == 0)
            return false;

        questSelectTime = Time.timeSinceLevelLoad;
        nextQuest = otherQuests[Random.Range(0, otherQuests.Count)];        
        return true;
    }

    public void BecomeUpset()
    {
        Talker.PushMessage(upsetPhrases[Random.Range(0, upsetPhrases.Length)]);
        SceneManager.LoadScene(talkScene, LoadSceneMode.Additive);
    }
}
