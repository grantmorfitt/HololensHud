using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineHUD : MonoBehaviour
{
    public GameObject Dial;
    public Text EnginePercentDisplay;
    // Start is called before the first frame update
    public float EngineNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEngine(float newNumber)
    {
        EngineNumber = newNumber;

        string EngineNumberToString = "";
        float candidate = Mathf.Round(EngineNumber * 10f) / 10f; //Round 1 decimal point.
        if(candidate < 100 && candidate.ToString().Length == 2) //Make sure .0 is appended on multiples of 10!
        {
            EngineNumberToString = candidate.ToString() + ".0";
        }
        else if(candidate == 100)
        {
            EngineNumberToString = candidate.ToString() + ".0";
        }
        else
        {
            EngineNumberToString = candidate.ToString();
        }
        EnginePercentDisplay.text = EngineNumberToString + "%";
        EngineNumber = EngineNumber * 2.0197f;//205 degrees of freedom for 101.5 percent.
        Dial.transform.localRotation = Quaternion.Euler(0, 0, -EngineNumber); //Negative rotation to increase.
    }
}
