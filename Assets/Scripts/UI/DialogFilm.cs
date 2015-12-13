using UnityEngine;
using System.Collections;

public class DialogFilm : MonoBehaviour {
    [SerializeField]
    MeshRenderer movieScreen;

    [SerializeField]
    MovieTexture baseFilm;

    [SerializeField]
    MovieTexture growthFilm;

    [SerializeField]
    MovieTexture shrinkFilm;


    void PlayFilm(MovieTexture film, bool loop)
    {
        if (movieScreen.material.mainTexture == film)
            return;
        film.loop = loop;
        movieScreen.material.mainTexture = film;
        film.Play();
    }

    void OnEnable()
    {
        DialogSystem.OnNewAnswer += AnswerReaction;
        DialogSystem.OnNewDialogState += DialogStateChange;
    }

    private void DialogStateChange(DialogCycle step)
    {
        if (step == DialogCycle.PlayerInput)
            PlayFilm(baseFilm, true);
    }

    void OnDisable()
    {
        DialogSystem.OnNewAnswer -= AnswerReaction;
        DialogSystem.OnNewDialogState -= DialogStateChange;
    }

    private void AnswerReaction(DialogOutcome type)
    {
        if (type == DialogOutcome.Grow)
            AnswerGrow();
        else
            AnswerShrink();
    }

    void Update()
    {
        if (!(movieScreen.material.mainTexture as MovieTexture).isPlaying)
            PlayFilm(baseFilm, true);

    }

    void AnswerGrow()
    {
        PlayFilm(growthFilm, true);
    }

    void AnswerShrink()
    {
        PlayFilm(shrinkFilm, true);
    }


}
