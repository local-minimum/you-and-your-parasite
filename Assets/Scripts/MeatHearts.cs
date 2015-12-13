using UnityEngine;
using System.Collections;

public class MeatHearts : MonoBehaviour {

    float X;
    float speed;

    [SerializeField]
    float minSize;

    [SerializeField]
    float maxSize;

	// Use this for initialization
	void Start () {
        X = Random.value * 1000f;
        speed = Random.Range(2f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, (Mathf.PerlinNoise(X, Time.timeSinceLevelLoad * speed) + Mathf.PerlinNoise(X, Time.timeSinceLevelLoad)));
	}
}
