using UnityEngine;
using System.Collections;

public class PickupTicTic : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        var questGiver = other.GetComponentInChildren<QuestGiver>();

        if (questGiver)
            questGiver.QueueFirstQuest();

        gameObject.SetActive(false);
    }
}
