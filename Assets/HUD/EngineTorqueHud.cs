using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineTorqueHud : MonoBehaviour
{
    public Text EngineTorqueDisplay;
    public GameObject Dial;
    // Start is called before the first frame update
    public float EngineTorque;
    public float max = 22000f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateEngineTorque(float newNumber)
    {
        EngineTorque = newNumber;
        float EngineTorquePercent = newNumber / max * 100;

        string EngineNumberToString = "";
        float candidate = Mathf.Round(EngineTorquePercent * 10f) / 10f; //Round 1 decimal point.
        if (candidate < 100 && candidate.ToString().Length == 2) //Make sure .0 is appended on multiples of 10!
        {
            EngineNumberToString = candidate.ToString() + ".0";
        }
        else if (candidate == 100)
        {
            EngineNumberToString = candidate.ToString() + ".0";
        }
        else
        {
            EngineNumberToString = candidate.ToString();
        }
        EngineTorqueDisplay.text = EngineNumberToString + "%";
        EngineTorquePercent = EngineTorquePercent * 2.0197f;//205 degrees of freedom for 101.5 percent.
        Dial.transform.localRotation = Quaternion.Euler(0, 0, -EngineTorquePercent); //Negative rotation to increase.
    }
}
