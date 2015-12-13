using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogSpawner : MonoBehaviour {

    [SerializeField]
    string dialog;

    [SerializeField]
    bool allowed;
    
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Controller3D>();

        if (player != null)
        {
            if (allowed)
            {
                allowed = false;
                player.allowWalk = false;
                SceneManager.LoadScene(dialog, LoadSceneMode.Additive);
            }
        }
    }
}
