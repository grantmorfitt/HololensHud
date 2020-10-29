using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickHUD : MonoBehaviour
{
    public Text ThousandsPlace;
    public Text HundredsPlace;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetThousandsPlace(int Thousands)
    {
        ThousandsPlace.text = Thousands.ToString();
    }

    public void SetHundredsPlace(int Hundreds)
    {
        HundredsPlace.text = Hundreds.ToString() + "00";
    }

    public void SetAltitude(int Altitude)
    {
        int Thousands = Altitude / 1000;
        //Debug.Log(Thousands);
        Altitude -= Thousands * 1000;
        int Hundreds = Altitude / 100;
        //Debug.Log(Hundreds);
        
        SetThousandsPlace(Thousands);
        SetHundredsPlace(Hundreds);
    }

    public void SetExactAltitude(int Altitude)
    {
        int Thousands = Altitude / 1000;
        //Debug.Log(Thousands);
        Altitude -= Thousands * 1000;
        //Debug.Log(Hundreds);
        SetThousandsPlace(Thousands);
        if(Altitude > 99)
        {
            HundredsPlace.text = Altitude.ToString();
        }
        else if(Altitude > 9)
        {
            HundredsPlace.text = "0" + Altitude.ToString();
        }
        else
        {
            HundredsPlace.text = "00" + Altitude.ToString();
        }
    }

    public int GetShownAltitude()
    {
        int thousands = int.Parse(ThousandsPlace.text) * 1000;
        int hundreds = int.Parse(HundredsPlace.text);

        return thousands + hundreds;
    }
}
