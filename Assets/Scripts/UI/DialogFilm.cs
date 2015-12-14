using UnityEngine;
using System.Collections.Generic;

public class DialogFilm : MonoBehaviour {
    [SerializeField]
    MeshRenderer movieScreen;

    [SerializeField]
    MovieTexture baseFilm;

    [SerializeField]
    MovieTexture growthFilm;

    [SerializeField]
    MovieTexture shrinkFilm;

    [SerializeField, Range(0, 5)]
    float delayReaction = 1f;


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

    private void AnswerReaction(DialogOutcome type, DialogCycle step)
    {
        if (step != DialogCycle.PlayerInput)
            return;

        if (type == DialogOutcome.Grow)
            StartCoroutine(AnswerGrow());
        else
            StartCoroutine(AnswerShrink());
    }

    void Update()
    {
        if (!(movieScreen.material.mainTexture as MovieTexture).isPlaying)
            PlayFilm(baseFilm, true);

    }

    IEnumerator<WaitForSeconds> AnswerGrow()
    {
        yield return new WaitForSeconds(delayReaction);
        PlayFilm(growthFilm, true);
    }

    IEnumerator<WaitForSeconds> AnswerShrink()
    {
        yield return new WaitForSeconds(delayReaction);
        PlayFilm(shrinkFilm, true);
    }

    public void RandomPlayOne()
    {
        if (Random.value < 0.33f)
            PlayFilm(baseFilm, true);
        else if (Random.value < 0.5f)
            PlayFilm(growthFilm, true);
        else
            PlayFilm(shrinkFilm, true);
    }
}
