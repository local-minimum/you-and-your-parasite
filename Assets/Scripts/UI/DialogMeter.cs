using UnityEngine;
using UnityEngine.UI;

public class DialogMeter : MonoBehaviour {

    Image[] levels;

    int level = 0;

    [SerializeField]
    Sprite growthSprite;

    [SerializeField]
    Sprite shrinkSprite;

    int growthVotes = 0;

    public void Increase(DialogOutcome type)
    {
        level = Mathf.Clamp(level + 1, 0, levels.Length);
        SetSprite(level, type);
        UpdateUI();        
    }

    void SetSprite(int level, DialogOutcome type)
    {
        levels[level - 1].sprite = type == DialogOutcome.Grow ? growthSprite : shrinkSprite;
    }

    public void Reset()
    {
        level = 0;
        UpdateUI();
    }

    void Start()
    {
        levels = GetComponentsInChildren<Image>();
        Reset();
    }

    void UpdateUI()
    {
        for (int i=0; i<levels.Length; i++)
        {
            levels[i].enabled = i < level;
        }

    }

    public bool Completed
    {
        get
        {
            return levels.Length == level;
        }
    }

    public bool GrowthOutcome
    {
        get {
            return growthVotes > levels.Length / 2;
        }        
    }
}
