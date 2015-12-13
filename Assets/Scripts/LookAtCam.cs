using UnityEngine;
using System.Collections;

public class LookAtCam : MonoBehaviour {


	void Update () {
        transform.rotation = Quaternion.LookRotation(transform.position - GameMonitor.worldCam.transform.position, Vector3.up);
	}
}
