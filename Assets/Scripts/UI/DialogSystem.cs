using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

[System.Serializable]
public class DialogIntro
{
    public bool Player;
    public string Greeting;
}

[System.Serializable]
public class GiftSequence
{
    public string GrowthTalk;
    public string GrowthStatus;
    public string ShrinkTalk;
    public string ShrintStatus;
}


public enum AnswerSide { Left, Right};

public enum DialogCycle {None, Intro, PlayerInput, Answer, Reaction, FinishUp, Gifting};

public enum DialogOutcome { Grow, Shrink };

public delegate void AnswerType(DialogOutcome type, DialogCycle step);
public delegate void CompletedInteraction(DialogOutcome outcome);
public delegate void DialogStep(DialogCycle step);

public class DialogSystem : MonoBehaviour {

    public static event AnswerType OnNewAnswer;
    public static event CompletedInteraction OnCompletedDialog;
    public static event DialogStep OnNewDialogState;

    [SerializeField]
    string sceneName;

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
    Image icon;

    [SerializeField]
    Sprite playerAvatar;

    [SerializeField]
    Sprite monsterAvatar;

    [SerializeField]
    DialogIntro[] introSequence;

    int introIndex;

    [SerializeField]
    DialogState[] questions;

    [SerializeField]
    GiftSequence gifting;

    int questionIndex = 0;

    KeyCode leftKey;
    KeyCode rightKey;

    bool talking;

    DialogCycle cycleStep;
    DialogOutcome answerOutcome;

    int growths = 0;

    Dictionary<AnswerSide, DialogOutcome> answers = new Dictionary<AnswerSide, DialogOutcome>();

    [SerializeField]
    Color32 playerTextColor;

    [SerializeField]
    Color32 monsterTextColor;

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
        if (cycleStep == DialogCycle.PlayerInput)
        {

            answerOutcome = answers[answer];

            if (answerOutcome == DialogOutcome.Grow)
                growths++;

            if (OnNewAnswer != null)
            {
                OnNewAnswer(answerOutcome, DialogCycle.PlayerInput);
            }

        }
        StepCycle();
    }

    bool inAnswerMode {
        set
        {
            buttonLeft.SetActive(value);
            answerLeft.enabled = value;
            answerRight.enabled = value;
            mainText.enabled = !value;
            icon.enabled = !value;
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

        if (introIndex < introSequence.Length)
        {
            mainText.text = introSequence[introIndex].Greeting;
            icon.sprite = introSequence[introIndex].Player ? playerAvatar : monsterAvatar;
            mainText.color = introSequence[introIndex].Player ? playerTextColor : monsterTextColor;
            introIndex++;
        }
        else if (cycleStep == DialogCycle.Intro)
        {
            SetAnswers();
            inAnswerMode = true;
            cycleStep = DialogCycle.PlayerInput;

        }
        else if (cycleStep == DialogCycle.PlayerInput)
        {
            mainText.text = answerOutcome == DialogOutcome.Grow ? questions[questionIndex].AnswerGrow : questions[questionIndex].AnswerShrink;
            mainText.color = playerTextColor;
            icon.sprite = playerAvatar;
            inAnswerMode = false;
            cycleStep = DialogCycle.Answer;
        }
        else if (cycleStep == DialogCycle.Answer)
        {
            if (OnNewAnswer != null)
            {
                OnNewAnswer(answerOutcome, DialogCycle.Reaction);
            }
            icon.sprite = monsterAvatar;
            mainText.color = monsterTextColor;
            mainText.text = answerOutcome == DialogOutcome.Grow ? questions[questionIndex].ReactionGrow : questions[questionIndex].ReactionShrink;

            if (questionIndex == questions.Length - 1 && mainText.text == "")
            {
                cycleStep = DialogCycle.FinishUp;
            }
            else
            {
                cycleStep = DialogCycle.Reaction;
            }
        }
        else if (cycleStep == DialogCycle.Reaction || cycleStep == DialogCycle.None)
        {
            if (cycleStep == DialogCycle.None)
                questionIndex = 0;
            else
                questionIndex++;

            if (questionIndex < questions.Length)
            {
                cycleStep = DialogCycle.Intro;
                icon.sprite = monsterAvatar;
                mainText.color = monsterTextColor;
                mainText.text = questions[questionIndex].Intro;
                if (mainText.text == "")
                    StepCycle();
            } else
            {
                cycleStep = DialogCycle.FinishUp;
            }
        }

        if (cycleStep == DialogCycle.FinishUp) {
            icon.sprite = monsterAvatar;
            mainText.color = monsterTextColor;
            mainText.text = grow ? gifting.GrowthTalk : gifting.ShrinkTalk;
            if (OnNewAnswer != null)
                OnNewAnswer(grow ? DialogOutcome.Grow : DialogOutcome.Shrink, DialogCycle.PlayerInput);

            cycleStep = DialogCycle.Gifting;
        } else if (cycleStep == DialogCycle.Gifting)
        {
                Complete();
        }

        if (OnNewDialogState != null)
            OnNewDialogState(cycleStep);
        talking = cycleStep != DialogCycle.PlayerInput;

    }

    void Complete()
    {
        introIndex = 0;
        talking = false;
        cycleStep = DialogCycle.None;
        if (OnCompletedDialog != null)
        {
            OnCompletedDialog(grow ? DialogOutcome.Grow : DialogOutcome.Shrink);
        }
        SceneManager.UnloadScene(sceneName);
        if (grow)
        {
            GameMonitor.IncreaseTickTick(gifting.GrowthStatus);
        } else
        {
            GameMonitor.DecreaseTickTick(gifting.ShrintStatus);
        }
        growths = 0;

    }

    bool grow
    {
        get
        {
            return growths > questions.Length / 2;

        }
    }

}
