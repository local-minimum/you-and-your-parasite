using UnityEngine;
using System.Collections;

[System.Serializable]
public class VidScene
{
    public Videos type;
    public GameObject canvas;
    public GameObject film;

}

public enum Videos { Intro, Talk, Resize, Amobea, Chippy, SilkWorm, Plastic, SantaGod, Toad, BeforeEndResize, EndSmall, EndBoring, EndBig};

public class VideoManager : MonoBehaviour {

    [SerializeField]
    VidScene[] videos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
