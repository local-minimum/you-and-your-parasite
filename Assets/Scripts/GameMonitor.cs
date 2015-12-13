using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameMonitor : MonoBehaviour {

    int ticTicSize = 0;

    bool didGrow = false;

    [SerializeField]
    Camera _worldCam;

    [SerializeField]
    Controller3D playerController;

    [SerializeField]
    string ticTicResize;

    StatusText _statusText;

    bool givenFirstQuest = false;

    [SerializeField, Range(0, 10)]
    float firstQuestDelay = 1;

    [SerializeField, Range(0, 20)]
    float beforeEndDelay = 10f;

    QuestGiver questGiver;

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

    void Start()
    {
        _statusText = FindObjectOfType<StatusText>();
        questGiver = FindObjectOfType<QuestGiver>();
    }

    public static void IncreaseTickTick(string status)
    {
        instance.ticTicSize++;
        instance.didGrow = true;
        statusText.ForceText(status);
        SceneManager.LoadScene(instance.ticTicResize, LoadSceneMode.Additive);
    }

    public static void DecreaseTickTick(string status)
    {
        instance.ticTicSize--;
        instance.didGrow = false;
        statusText.ForceText(status);
        SceneManager.LoadScene(instance.ticTicResize, LoadSceneMode.Additive);
    }

    public static void ResizeDone()
    {
        AllowPlayerToWalk();
        instance.NextQuest();
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

    public static StatusText statusText
    {
        get
        {
            return instance._statusText;
        }
    }
    
    public static Camera worldCam
    {
        get
        {
            return instance._worldCam;
        }
    }

    void NextQuest()
    {
        if (Mathf.Abs(ticTicSize) == 3 || !questGiver.QueueQuest())
            StartCoroutine(End());

    }

    IEnumerator<WaitForSeconds> End()
    {
        yield return new WaitForSeconds(beforeEndDelay);
        if (ticTicSize == -3)
            _statusText.ForceText("Free from tictic");
        else if (ticTicSize == 3)
            _statusText.ForceText("Tictic is the world");
        else
            _statusText.ForceText("Stuck in limbo");
    }
}
