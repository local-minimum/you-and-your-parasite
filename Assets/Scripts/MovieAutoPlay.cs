using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieAutoPlay : MonoBehaviour {

    [SerializeField]
    string sceneName;

    [SerializeField]
    MovieTexture[] growthMovies;

    [SerializeField]
    MovieTexture[] shrinkMovies;

    MovieTexture movie;

    void Start() {
        movie = GetMovie();
        GetComponent<Renderer>().material.mainTexture = movie;
        if (movie == null)
        {
            End();
        }
        else
        {
            movie.loop = false;
            movie.Play();
        }
	}
	
    MovieTexture GetMovie()
    {

        int index = GameMonitor.TicTicSize;
        if (GameMonitor.DidGrow) {
            if (index < 0)
                index = growthMovies.Length + index;

            return growthMovies[index];
        }
        else {
            if (index < 0)
                index = shrinkMovies.Length + index;
            return shrinkMovies[index];
        }
    }

    void Update()
    {
        if (movie != null && !movie.isPlaying)
        {
            End();
        }
    }

    void End()
    {
        movie = null;
        GameMonitor.AllowPlayerToWalk();
        SceneManager.UnloadScene(sceneName);
    }
}
