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

public enum DialogCycle {None, Intro, PlayerInput, Answer, Reaction};

public delegate void AnswerType(DialogOutcome type);

public enum DialogOutcome { Grow, Shrink };

public delegate void CompletedInteraction(DialogOutcome outcome);

public class DialogSystem : MonoBehaviour {

    public static event AnswerType OnNewAnswer;
    public static event CompletedInteraction OnCompletedDialog;

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

    KeyCode leftKey;
    KeyCode rightKey;

    bool talking;

    DialogCycle cycleStep;
    DialogOutcome answerOutcome;

    int growths = 0;

    Dictionary<AnswerSide, DialogOutcome> answers = new Dictionary<AnswerSide, DialogOutcome>();
    
	void Start () {
        leftKey = (KeyCode)PlayerPrefs.GetInt("Key.X", (int)'x');
        rightKey = (KeyCode)PlayerPrefs.GetInt("Key.C", (int)'c');
        mainText.enabled = false;
        inAnswerMode = false;
	}

    public void AnswerLeft()
    {
        Answer(AnswerSide.Left);
    }

    public void AnswerRight()
    {
        Answer(AnswerSide.Right);
    }

    public void Answer(AnswerSide answer)
    {
        if (OnNewAnswer != null)
        {
            OnNewAnswer(answers[ answer]);
        }
        answerOutcome = answers[answer];

        if (answerOutcome == DialogOutcome.Grow)
            growths++;

        StepCycle();
    }

    bool inAnswerMode {
        set
        {
            buttonLeft.SetActive(value);
            buttonRight.SetActive(value);
            answerLeft.enabled = value;
            answerRight.enabled = value;
            mainText.enabled = !value;
        }
    }

    void SetAnswers()
    {
        bool reversed = Random.value < 0.5f;
        answers[reversed ? AnswerSide.Left : AnswerSide.Right] = DialogOutcome.Grow;
        answers[reversed ? AnswerSide.Right : AnswerSide.Left] = DialogOutcome.Shrink;

        answerLeft.text = reversed ? questions[questionIndex].HintGrow : questions[questionIndex].HintShrink;
        answerRight.text = reversed ? questions[questionIndex].HintShrink : questions[questionIndex].HintGrow;
    }

    void Update()
    {
        if (talking)
        {
            if (Input.GetKeyDown(leftKey) || (Input.GetKeyDown(rightKey)))
                StepCycle();
        }
        else if (cycleStep == DialogCycle.PlayerInput)
        {
            if (Input.GetKeyDown(leftKey))
                AnswerLeft();
            else if (Input.GetKeyDown(rightKey))
                AnswerRight();
        }
        else if (cycleStep == DialogCycle.None)
            StepCycle();

    }

    void StepCycle()
    {
        Debug.Log(cycleStep);
        if (cycleStep == DialogCycle.Intro)
        {         
            SetAnswers();
            inAnswerMode = true;
            cycleStep = DialogCycle.PlayerInput;

        } else if (cycleStep == DialogCycle.PlayerInput)
        {
            mainText.text = answerOutcome == DialogOutcome.Grow ? questions[questionIndex].AnswerGrow : questions[questionIndex].AnswerShrink;
            inAnswerMode = false;
            cycleStep = DialogCycle.Answer;
        } else if (cycleStep == DialogCycle.Answer)
        {
            if (questionIndex == questions.Length - 1)
            {
                Complete();
            } else
            {
                mainText.text = answerOutcome == DialogOutcome.Grow ? questions[questionIndex].ReactionGrow : questions[questionIndex].ReactionShrink;
            }

            cycleStep = DialogCycle.Reaction;
        } else if (cycleStep == DialogCycle.Reaction || cycleStep == DialogCycle.None)
        {
            if (cycleStep == DialogCycle.None)
                questionIndex = 0;
            else
                questionIndex++;

            if (questionIndex < questions.Length)
            {
                cycleStep = DialogCycle.Intro;
                mainText.text = questions[questionIndex].Intro;
            }
        }

        talking = cycleStep != DialogCycle.PlayerInput;

    }

    void Complete()
    {
        talking = false;
        cycleStep = DialogCycle.None;
        if (OnCompletedDialog != null)
        {
            OnCompletedDialog(growths > questions.Length / 2 ? DialogOutcome.Grow : DialogOutcome.Shrink);
        }
        growths = 0;
        Application.UnloadLevel("/interaction");
    }

}
