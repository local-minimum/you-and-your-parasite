using UnityEngine;
using System.Collections;

[System.Serializable]
public class VidScene
{
    public Videos type;
    public GameObject canvas;
    public GameObject film;

    public void ShowContent()
    {
        if (film)
            film.SetActive(true);
        if (canvas)
            canvas.SetActive(true);
    }

    public void HideContent()
    {
        if (film)
            film.SetActive(false);
        if (canvas)
            canvas.SetActive(false);
    }
}

public enum Videos { Intro, Talk, Resize, Amobea, Chippy, SilkWorm, Plastic, SantaGod, Toad, BeforeEndResize, EndSmall, EndBoring, EndBig};

public class VideoManager : MonoBehaviour {

    [SerializeField]
    VidScene[] videos;    

    static VideoManager _instance;

    static VideoManager instance
    {
        get
        {
            if (_instance == null)
                SetInstance();
            return _instance;
        }
    }

    static void SetInstance()
    {
        _instance = FindObjectOfType<VideoManager>();
        if (_instance == null)
            SpawnInstance();
    }

    static void SpawnInstance()
    {
        var GO = new GameObject();
        GO.name = "Video Manager";
        _instance = GO.AddComponent<VideoManager>();       
    }

    void Awake() {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(this);
    }

    public static void Show(Videos video)
    {
        for (int i=0, l=instance.videos.Length; i<l; i++)
        {
            if (instance.videos[i].type == video)
                instance.videos[i].ShowContent();

        }
    }

    public static void HideAll()
    {
        for (int i = 0, l = instance.videos.Length; i < l; i++)
        {
            instance.videos[i].HideContent();
        }

    }
}
