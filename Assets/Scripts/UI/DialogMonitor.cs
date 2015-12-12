using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DialogMonitor : MonoBehaviour {


    [SerializeField]
    DialogMeter meter;

    void OnEnable()
    {
        DialogSystem.OnNewAnswer += Response;
    }

    void OnDisable()
    {
        DialogSystem.OnNewAnswer -= Response;
    }

	void Response(DialogOutcome response)
    {
        meter.Increase(response);

    }
}
