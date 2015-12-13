using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMonitor : MonoBehaviour {

    int ticTicSize = 0;

    bool didGrow = false;

    [SerializeField]
    Controller3D playerController;

    [SerializeField]
    string ticTicResize;

    static GameMonitor _instance;

    static GameMonitor instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Spawn();
            }
            return _instance;
        }
    }

    static GameMonitor Spawn()
    {
        var GO = new GameObject();
        GO.name = "Game Monitor";
        return GO.AddComponent<GameMonitor>();
    }

	void Awake () {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(this.gameObject);
	}

    public static void IncreaseTickTick(string status)
    {
        instance.ticTicSize++;
        instance.didGrow = true;
        SceneManager.LoadScene(instance.ticTicResize, LoadSceneMode.Additive);
    }

    public static void DecreaseTickTick(string status)
    {
        instance.ticTicSize--;
        instance.didGrow = false;
        SceneManager.LoadScene(instance.ticTicResize, LoadSceneMode.Additive);
    }

    public static void AllowPlayerToWalk()
    {
        instance.playerController.allowWalk = true;
    }

    public static int TicTicSize
    {
        get
        {
            return instance.ticTicSize;
        }
    }

    public static bool DidGrow
    {
        get
        {
            return instance.didGrow;
        }
    }
}
