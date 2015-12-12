using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum DialogOutcome { Grow, Shrink};

public delegate void CompletedInteraction(DialogOutcome outcome);

public class DialogMonitor : MonoBehaviour {

    public static event CompletedInteraction OnCompletedInteraction;

    [SerializeField]
    DialogMeter meter;

	void Response(DialogOutcome response)
    {
        meter.Increase(response);
        if (meter.Completed)
        {
            if (OnCompletedInteraction != null)
                OnCompletedInteraction(meter.GrowthOutcome ? DialogOutcome.Grow : DialogOutcome.Shrink);
            meter.Reset();
        }
    }
}
