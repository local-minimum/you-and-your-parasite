using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogSpawner : MonoBehaviour {

    [SerializeField]
    string dialogScene;

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
                SceneManager.LoadScene(dialogScene, LoadSceneMode.Additive);
            } else
            {
                var tictic = other.GetComponentInChildren<QuestGiver>();
                if (tictic)
                    tictic.BecomeUpset();
            }

        }
    }
}
