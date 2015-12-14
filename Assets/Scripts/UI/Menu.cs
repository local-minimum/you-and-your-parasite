using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    [SerializeField]
    string gameScene;

	public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void BrowseTo(string url)
    {
        Application.OpenURL(url);
    }

    public void Quit() {
        Application.Quit();
    }

}
