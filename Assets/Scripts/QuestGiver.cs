using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuestGiver : MonoBehaviour {

    [SerializeField]
    AudioSource aSource;

    [SerializeField]
    string talkScene;

    [SerializeField]
    DialogSpawner firstQuest;

    [SerializeField]
    List<DialogSpawner> otherQuests = new List<DialogSpawner>();

    [SerializeField]
    string[] upsetPhrases;

    [SerializeField]
    AudioClip upsetClip;

    [SerializeField, Range(0, 1)]
    float upsetVolume;

    [SerializeField]
    AudioClip talkClip;

    [SerializeField, Range(0, 1)]
    float talkVolume;

    [SerializeField, Range(0, 10)]
    float questDelay;

    [SerializeField]
    float[] scales;

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
        aSource.PlayOneShot(talkClip, talkVolume);
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

    public bool HasMoreQuests
    {
        get
        {
            return otherQuests.Count > 0;
        }
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
        aSource.PlayOneShot(upsetClip, upsetVolume);
        Talker.PushMessage(upsetPhrases[Random.Range(0, upsetPhrases.Length)]);
        SceneManager.LoadScene(talkScene, LoadSceneMode.Additive);
    }

    public void SetSize(int size)
    {
        if (size < 0)
            size += scales.Length;

        transform.GetChild(0).localScale = Vector2.one * scales[size];
            
    }
}
