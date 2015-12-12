using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Meter : MonoBehaviour {

    Image[] levels;

    int level = 0;

    public void Increase()
    {
        level = Mathf.Clamp(level + 1, 0, levels.Length);
        UpdateUI();        
    }

    public void Reset()
    {
        level = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i=0; i<levels.Length; i++)
        {
            levels[i].enabled = i < level;
        }

    }
}
