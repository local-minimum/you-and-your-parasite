using UnityEngine;
using System.Collections;

public class OneCamToRuleAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Camera.main != null && Camera.main != GetComponent<Camera>())
            Destroy(gameObject);	
	}
	
}
