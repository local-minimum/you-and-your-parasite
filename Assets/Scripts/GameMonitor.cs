using UnityEngine;
using System.Collections;

public class GameMonitor : MonoBehaviour {

    int ticTicSize = 0;

    [SerializeField]
    Controller3D playerController;

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

    public static void IncreaseTickTick()
    {
        instance.ticTicSize++;
        AllowPlayerToWalk();
    }

    public static void DecreaseTickTick()
    {
        instance.ticTicSize--;
        AllowPlayerToWalk();
    }

    public static void AllowPlayerToWalk()
    {
        instance.playerController.allowWalk = true;
    }
}
