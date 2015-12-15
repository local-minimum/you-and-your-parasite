using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogSpawner : MonoBehaviour {

    [SerializeField]
    string dialogScene;

    [SerializeField]
    Videos dialogVideo;

    public bool allowed;
    public string text;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Controller3D>();

        if (player != null)
        {
            if (allowed)
            {
                allowed = false;
                player.allowWalk = false;
                GameMonitor.statusText.ClearText();
                GameMonitor.WatchingMovie = true;
                //Application.LoadLevelAdditive(dialogScene);
                //SceneManager.LoadScene(dialogScene, LoadSceneMode.Additive);
                VideoManager.Show(dialogVideo);
            } else
            {
                var tictic = other.GetComponentInChildren<QuestGiver>();
                if (tictic)
                    tictic.BecomeUpset();
            }

        }
    }
}
