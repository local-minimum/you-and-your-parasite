using UnityEngine;
using System.Collections.Generic;

public class QuestGiver : MonoBehaviour {

    [SerializeField]
    DialogSpawner firstQuest;

    [SerializeField]
    List<DialogSpawner> otherQuests = new List<DialogSpawner>();

    DialogSpawner nextQuest;

	void Update () {
        if (nextQuest && GameMonitor.statusText.PushText(nextQuest.text))
            ClearNextQuest();
	}

    void ClearNextQuest()
    {
        nextQuest.allowed = true;
        if (nextQuest != firstQuest)
            otherQuests.Remove(nextQuest);
        nextQuest = null;
    }

    public void QueueFirstQuest() {
        nextQuest = firstQuest;        
    }

    public bool QueueQuest()
    {
        if (otherQuests.Count == 0)
            return false;

        nextQuest = otherQuests[Random.Range(0, otherQuests.Count)];
        return true;
    }

}
