using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class DialogState
{
    public string Intro;

    public string HintGrow;
    public string HintShrink;

    public string AnswerGrow;    
    public string AnswerShrink;

    public string ReactionGrow;
    public string ReactionShrink;
}

public enum AnswerSide { Left, Right};

public delegate void AnswerType(DialogOutcome type);

public class DialogSystem : MonoBehaviour {

    public static event AnswerType OnNewAnswer;

    [SerializeField]
    Text mainText;

    [SerializeField]
    GameObject buttonLeft;

    [SerializeField]
    GameObject buttonRight;

    [SerializeField]
    Text answerLeft;

    [SerializeField]
    Text answerRight;

    [SerializeField]
    DialogState[] questions;

    int questionIndex = 0;    

    Dictionary<AnswerSide, DialogOutcome> answers = new Dictionary<AnswerSide, DialogOutcome>();
    
	void Start () {
        mainText.enabled = false;
        inAnswerMode = false;
	}

    public void Answer(AnswerSide answer)
    {
        if (OnNewAnswer != null)
        {
            OnNewAnswer(answers[answer]);
        }
        mainText.text = answers[answer] == DialogOutcome.Grow ? questions[questionIndex].ReactionGrow : questions[questionIndex].ReactionShrink;
        questionIndex++;
        inAnswerMode = false;
    }

    bool inAnswerMode {
        set
        {
            buttonLeft.SetActive(value);
            buttonRight.SetActive(value);
            answerLeft.enabled = value;
            answerRight.enabled = value;
        }
    }

    void SetAnswers()
    {
        bool reversed = Random.value < 0.5f;
        answers[reversed ? AnswerSide.Left : AnswerSide.Right] = DialogOutcome.Grow;
        answers[reversed ? AnswerSide.Right : AnswerSide.Left] = DialogOutcome.Shrink;

        answerLeft.text = reversed ? questions[questionIndex].HintGrow : questions[questionIndex].HintShrink;
        answerRight.text = reversed ? questions[questionIndex].HintShrink : questions[questionIndex].HintGrow;

        inAnswerMode = true;
    }

}
