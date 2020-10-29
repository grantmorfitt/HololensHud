using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttitudeHeadingTick : MonoBehaviour
{
    public Text MyText;
    int RealNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(int NumToConvert)
    {
        RealNum = NumToConvert;
        NumToConvert = NumToConvert%36;

        if (NumToConvert == 0)//Cardinal Exceptions
        {
            MyText.text = "N";
        }
        else if (NumToConvert == 9)
        {
            MyText.text = "E";
        }
        else if (NumToConvert == 18)
        {
            MyText.text = "S";
        }
        else if (NumToConvert == 27)
        {
            MyText.text = "W";
        }
        else
        {
            MyText.text = NumToConvert.ToString();
        }
    }

    public int GetRealNum()
    {
        return RealNum;
    }
}
