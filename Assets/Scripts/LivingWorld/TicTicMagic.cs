using UnityEngine;
using System.Collections;

public class TicTicMagic : MonoBehaviour {

    [SerializeField, Range(0, 360)]
    float rotation;

	void Update () {
        transform.Rotate(transform.forward, Random.Range(-1, 1) * rotation * Time.deltaTime);	
	}
}
