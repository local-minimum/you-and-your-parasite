using UnityEngine;
using System.Collections;

public class MovieAutoPlay : MonoBehaviour {

	void Start () {
        (GetComponent<Renderer>().material.mainTexture as MovieTexture).Play();
	}
	
}
