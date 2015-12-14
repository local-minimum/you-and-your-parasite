using UnityEngine;
using System.Collections;

public class DialogFilmRandomizer : MonoBehaviour {


	void Start () {
        var dFilm = GetComponent<DialogFilm>();

        dFilm.RandomPlayOne();  
	}

}
