using UnityEngine;
using System.Collections;

public class BeforeEndResize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var dFilm = GetComponent<DialogFilm>();
        if (GameMonitor.DidGrow)
            dFilm.PlayGrow();
        else
            dFilm.PlayShrink();
	}

    void OnDisable()
    {
        GameMonitor.ResizeDone();
    }
}
