﻿using UnityEngine;
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

    [SerializeField]
    string ticTicBeforeEndSize;

    [SerializeField]
    string endBig;

    [SerializeField]
    string endSmall;

    [SerializeField]
    string endBoring;

    [SerializeField]
    string menu;

    [SerializeField]
    AudioClip ticTicIncrease;

    [SerializeField]
    AudioClip ticTicDecrease;

    bool endingIt = false;

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

    void Awake() {
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
        var audio = instance.playerController.GetComponentInChildren<AudioSource>();
        if (audio)
            audio.PlayOneShot(instance.ticTicIncrease);
        _dialogOutcome(status);
    }

    public static void DecreaseTickTick(string status)
    {
        instance.ticTicSize--;
        instance.didGrow = false;
        var audio = instance.playerController.GetComponentInChildren<AudioSource>();
        if (audio)
            audio.PlayOneShot(instance.ticTicDecrease);
        _dialogOutcome(status);
    }

    static void _dialogOutcome(string status)
    {
        Talker.PushMessage(status);
        if (Mathf.Abs(instance.ticTicSize) == 3)
            SceneManager.LoadScene(instance.ticTicBeforeEndSize, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(instance.ticTicResize, LoadSceneMode.Additive);
    }

    public static void ResizeDone()
    {
        AllowPlayerToWalk = true;
        instance.NextQuest();
    }

    public static bool AllowPlayerToWalk
    {
        set
        {
            instance.playerController.allowWalk = value;
        }
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

    public static KeyCode leftKey {
        get {
            return (KeyCode)PlayerPrefs.GetInt("Key.X", (int)'x');
        }
    }

    public static KeyCode rightKey
    {

        get
        {
            return (KeyCode)PlayerPrefs.GetInt("Key.C", (int)'c');
        }
    }

    IEnumerator<WaitForSeconds> End()
    {
        yield return new WaitForSeconds(beforeEndDelay);
        endingIt = true;
        if (ticTicSize == -3)
            SceneManager.LoadScene(endSmall, LoadSceneMode.Additive);
        else if (ticTicSize == 3)
            SceneManager.LoadScene(endBig, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(endBoring, LoadSceneMode.Additive);
    }

    void Update() {
        if (endingIt && SceneManager.sceneCount == 1 || Input.GetButtonDown("Cancel"))
            SceneManager.LoadScene(menu);

    }

}
