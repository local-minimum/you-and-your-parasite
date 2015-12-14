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

    void Start () {
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
        var player = FindObjectOfType<MovieAutoPlay>();
        if (player)
            player.End();
        else
        {
            GameMonitor.AllowPlayerToWalk = true;
            GameMonitor.WatchingMovie = false;
            //SceneManager.UnloadScene(gameObject.scene.name);
            Application.UnloadLevel(gameObject.scene.name);
        }
    }

    public static void PushMessage(string message)
    {
        messages.Enqueue(message);
    }


}
