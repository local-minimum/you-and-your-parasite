using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Talker : MonoBehaviour {

    [SerializeField]
    Text text;

    KeyCode leftKey;
    KeyCode rightKey;

    static Queue<string> messages = new Queue<string>();

    void OnEnable () {
        leftKey = GameMonitor.leftKey;
        rightKey = GameMonitor.rightKey;
        GameMonitor.AllowPlayerToWalk = false;
        NextText();
	}
	
	void Update () {
        if (Input.GetKeyDown(leftKey) || Input.GetKeyDown(rightKey))
            NextText();	    
	}

    public void NextText()
    {
        if (messages.Count > 0)
        {
            text.text = messages.Dequeue();
        } else
        {
            Close();
        }
    }

    void Close()
    {
        messages.Clear();
        var player = FindObjectOfType<MovieAutoPlay>();
        if (player)
            player.End();
        else
        {
            GameMonitor.AllowPlayerToWalk = true;
            GameMonitor.WatchingMovie = false;
            //SceneManager.UnloadScene(gameObject.scene.name);
            //Application.UnloadLevel(gameObject.scene.name);
            VideoManager.HideAll();
        }
    }

    public static void PushMessage(string message)
    {
        Debug.Log("Added message: " + message);
        messages.Enqueue(message);
    }


}
