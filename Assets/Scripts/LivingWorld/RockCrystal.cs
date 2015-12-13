using UnityEngine;
using System.Collections;

public class RockCrystal : MonoBehaviour {

    GameObject[] others;

    float nextSpawn;

    [SerializeField, Range(0, 10)]
    float minTime = 1;

    [SerializeField, Range(0, 10)]
    float maxTime = 3;

    int visibleIndex = -1;

	// Use this for initialization
	void Start () {
        others = new GameObject[transform.childCount - 1];
        for (int i = 0; i < others.Length; i++)
            others[i] = transform.GetChild(i + 1).gameObject;
	}

    void Update()
    {
        if (Time.timeSinceLevelLoad > nextSpawn)
        {
            visibleIndex++;
            if (visibleIndex > others.Length)
                visibleIndex = 0;
            SetVisible();
            nextSpawn = Time.timeSinceLevelLoad + Random.Range(minTime, maxTime);

        }
    }

    void SetVisible() {
        for (int i = 0; i < others.Length; i++)
            others[i].SetActive(i < visibleIndex);
    }	
}
