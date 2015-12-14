using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovieAutoPlay2 : MonoBehaviour {

    [SerializeField]
    bool loop;

    [SerializeField, Range(0, 10)]
    float afterPlayWait = 0.5f;

    float lastPlay = 0;

    [SerializeField]
    MovieTexture movie;

    [SerializeField, Range(0, 10)]
    float allowExitAfter = 0.1f;

    float sceneStart;

    KeyCode leftKey;
    KeyCode rightKey;

	void Start () {
        leftKey = GameMonitor.leftKey;
        rightKey = GameMonitor.rightKey;
        sceneStart = Time.timeSinceLevelLoad;

        GetComponentInChildren<Renderer>().material.mainTexture = movie;
        movie.loop = loop;
        movie.Play();
	}
	
	void Update () {
        if (movie.isPlaying)
            lastPlay = Time.timeSinceLevelLoad;
        else if (Time.timeSinceLevelLoad - lastPlay > afterPlayWait)
        {
            //SceneManager.UnloadScene(gameObject.scene.name);
            GameMonitor.WatchingMovie = false;
            Application.UnloadLevel(gameObject.scene.name);
        }

        if (Time.timeSinceLevelLoad - sceneStart > allowExitAfter && (Input.GetKeyDown(leftKey) || Input.GetKeyDown(rightKey)))
        {
            GameMonitor.WatchingMovie = false;
            // SceneManager.UnloadScene(gameObject.scene.name);
            Application.UnloadLevel(gameObject.scene.name);
        }

	}
}
