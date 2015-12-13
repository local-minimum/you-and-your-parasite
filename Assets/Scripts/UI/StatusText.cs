using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class StatusText : MonoBehaviour {

    Text textField;
    Image background;

    [SerializeField]
    AnimationCurve backgroundEasing;

    [SerializeField]
    AnimationCurve textEasing;

    [SerializeField, Range(0, 100)]
    float duration = 20f;

    bool isEasing = false;

    float startTime;

	// Use this for initialization
	void Start () {
        background = GetComponentInChildren<Image>();
        textField = GetComponentInChildren<Text>();
        textField.enabled = false;
        background.enabled = false;
	}

    public bool PushText(string text)
    {
        if (isEasing)
            return false;
        ForceText(text);
        return true;
    }

    public void ForceText(string text)
    {
        startTime = Time.timeSinceLevelLoad;
        textField.text = text;
        if (!isEasing)
            StartCoroutine(playMessage());
    }

    public void ClearText()
    {
        if (isEasing)
            StartCoroutine(easeOut());
    }

    public void ClearTextNow()
    {
        isEasing = false;
    }

    IEnumerator<WaitForSeconds> easeOut()
    {
        while (isEasing)
        {
            startTime -= 1f;
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator<WaitForSeconds> playMessage()
    {
        isEasing = true;
        float easeTime = 0f;
        var bgColor = background.color;
        var textColor = textField.color;
        bgColor.a = 0;
        textColor.a = 0;
        background.color = bgColor;
        textField.color = textColor;
        background.enabled = true;
        textField.enabled = true;
        do
        {
            easeTime = (Time.timeSinceLevelLoad - startTime) / duration;
            bgColor.a = backgroundEasing.Evaluate(easeTime);
            textColor.a = textEasing.Evaluate(easeTime);
            background.color = bgColor;
            textField.color = textColor;
            yield return new WaitForSeconds(0.1f);

        } while (easeTime < 1f && isEasing);
        background.enabled = false;
        textField.enabled = false;
        isEasing = false;
    }

    public bool showingText
    {
        get
        {
            return isEasing;
        }
    }
}
