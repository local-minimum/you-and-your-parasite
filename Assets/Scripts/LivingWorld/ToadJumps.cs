using UnityEngine;
using System.Collections.Generic;

public class ToadJumps : MonoBehaviour {

    [SerializeField]
    AnimationCurve jumpHeight;

    [SerializeField]
    AnimationCurve jumpLength;

    [SerializeField]
    Transform[] jumpTargets;

    bool jumping = false;

    [SerializeField, Range(0, 5)]
    float jumpDuration = 2f;

    [SerializeField]
    Transform toad;

    int nextTarget = 1;

    [SerializeField, Range(0, 10)]
    float jumpPower = 2f;

    [SerializeField, Range(0, 1)]
    float jumpRate = 0.5f;

    IEnumerator<WaitForSeconds> Jump()
    {
        jumping = true;
        float startTime = Time.timeSinceLevelLoad;
        float progress = 0f;
        Vector3 jumpSource = toad.position;
        Vector3 jumpTarget = jumpTargets[nextTarget].position;

        do
        {
            progress = Mathf.Clamp01((Time.timeSinceLevelLoad - startTime) / jumpDuration);
            toad.transform.position = Vector3.Lerp(jumpSource, jumpTarget, jumpLength.Evaluate(progress)) + Vector3.up * jumpHeight.Evaluate(progress) * jumpPower;
            yield return new WaitForSeconds(0.05f);

        } while (progress < 1f);

        nextTarget++;
        nextTarget %= jumpTargets.Length;
        toad.transform.position = jumpTarget;
        jumping = false;
    }

    void Update() {

        if (!jumping && Random.value  < jumpRate * Time.deltaTime)
            StartCoroutine(Jump());
    }



}
