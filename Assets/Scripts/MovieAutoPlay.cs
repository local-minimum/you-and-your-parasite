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

    [SerializeField]
    bool endOnClipEnd = true;

    void OnEnable() {
        movie = GetMovie();
        GetComponent<Renderer>().material.mainTexture = movie;
        if (movie == null)
        {
            if (endOnClipEnd)
                End();
        }
        else
        {
            movie.Stop();
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
            if (endOnClipEnd)
                End();
        }
    }

    public void End()
    {
        movie = null;
        GameMonitor.ResizeDone();
        //SceneManager.UnloadScene(gameObject.scene.name);
        GameMonitor.WatchingMovie = false;
        //Application.UnloadLevel(gameObject.scene.name);
        VideoManager.HideAll();
    }
}
