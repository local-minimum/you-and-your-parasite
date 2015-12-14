using UnityEngine;
using UnityEngine.SceneManagement;

public class MovieAutoPlay : MonoBehaviour {

    [SerializeField]
    MovieTexture[] growthMovies;

    [SerializeField]
    MovieTexture[] shrinkMovies;

    MovieTexture movie;

    [SerializeField]
    bool loop = false;

    void Start() {
        movie = GetMovie();
        GetComponent<Renderer>().material.mainTexture = movie;
        if (movie == null)
        {
            End();
        }
        else
        {
            movie.loop = loop;
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

    public void End()
    {
        movie = null;
        GameMonitor.ResizeDone();
        SceneManager.UnloadScene(gameObject.scene.name);
    }
}
