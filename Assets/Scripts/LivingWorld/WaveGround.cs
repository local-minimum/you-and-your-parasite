using UnityEngine;
using System.Collections;

public class WaveGround : MonoBehaviour {

    [SerializeField]
    AnimationCurve wave;

    [SerializeField, Range(0, 5f)]
    float waveHeight = 0.05f;

    [SerializeField, Range(1, 10)]
    float waveDuration = 7;

    [SerializeField, Range(0, 1)]
    float delta;

	// Update is called once per frame
	void Update () {
        float fraction = (Time.timeSinceLevelLoad % waveDuration) / waveDuration;
        
        for (int i=0, l=transform.childCount; i<l; i++)
        {
            var pos = transform.GetChild(i).localPosition;
            pos.z = waveHeight * wave.Evaluate(fraction);
            transform.GetChild(i).localPosition = pos;
            fraction += delta;
            fraction %= 1.0f;
        }	
	}
}
