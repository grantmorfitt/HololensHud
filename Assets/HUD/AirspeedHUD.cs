using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirspeedHUD : MonoBehaviour
{
    public float indicatedAirspeed = 0;
    public bool SetUpComplete;
    public GameObject TickPrefab;
    public Text MasterIndicator;
    // Start is called before the first frame update
    void Start()
    {
        SetUpComplete = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAirspeed(float NewAirspeed)
    {
        if (!SetUpComplete)
        {
            SetUp();
        }
        //Tracks to see if the aircraft has moved to the next 200 ft margin.
        //If it has, generate a new higher or lower tick mark.
        int OldAirspeedDivisor = (int)(indicatedAirspeed / 20);
        indicatedAirspeed = NewAirspeed;
        int NewAirspeedDivisor = (int)(indicatedAirspeed / 20);

        float YOffset = indicatedAirspeed / 20 * 4;//Four units of Y movement per 20 knots.
        //Debug.Log(YOffset);
        int intAir = (int)indicatedAirspeed; //Remove decimal places.
        MasterIndicator.text = intAir.ToString(); //Set exact airspeed indicator.
        this.transform.localPosition = new Vector3(this.transform.position.x, -YOffset);//Move tape down when speeding up.

        if (this.transform.childCount > 0)
        {
            if (NewAirspeedDivisor > OldAirspeedDivisor)
            {
                int TickDiscrepancy = NewAirspeedDivisor - OldAirspeedDivisor; //In case of data flow interruption, detects how many divisions apart the gauge and data is.
                while (TickDiscrepancy > 0)
                {
                    GenerateNextHigherIndicator();
                    TickDiscrepancy--;
                }
            }
            else if (NewAirspeedDivisor < OldAirspeedDivisor)
            {
                int TickDiscrepancy = NewAirspeedDivisor - OldAirspeedDivisor; //In case of data flow interruption, detects how many divisions apart the gauge and data is.
                while (TickDiscrepancy < 0)
                {
                    GenerateNextLowerIndicator();
                    TickDiscrepancy++;
                }
            }
        }
    }

    private void SetUp() //Place initial ticks marks depending on altitude at the moment.
    {
        if (indicatedAirspeed != 0f)
        {
            int InitialYOffset = (int)indicatedAirspeed / 20;

            //Debug.Log(indicatedAirspeed);
            //Debug.Log(InitialYOffset);

            /*GameObject NewTick1 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick2 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick3 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick4 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick5 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick6 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick7 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick8 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick9 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);*/

            GameObject NewTick1 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick2 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick3 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick4 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick5 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick6 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick7 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick8 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick9 = Instantiate(TickPrefab, this.transform, false);

            NewTick1.transform.localPosition = new Vector3(13f, InitialYOffset * 4 - 15);
            NewTick2.transform.localPosition = new Vector3(13f, InitialYOffset * 4 - 11);
            NewTick3.transform.localPosition = new Vector3(13f, InitialYOffset * 4 - 7);
            NewTick4.transform.localPosition = new Vector3(13f, InitialYOffset * 4 - 3);
            NewTick5.transform.localPosition = new Vector3(13f, InitialYOffset * 4 + 1);
            NewTick6.transform.localPosition = new Vector3(13f, InitialYOffset * 4 + 5);
            NewTick7.transform.localPosition = new Vector3(13f, InitialYOffset * 4 + 9);
            NewTick8.transform.localPosition = new Vector3(13f, InitialYOffset * 4 + 13);
            NewTick9.transform.localPosition = new Vector3(13f, InitialYOffset * 4 + 17);

            NewTick1.GetComponent<Text>().text = (InitialYOffset * 20 - 80).ToString();
            NewTick2.GetComponent<Text>().text = (InitialYOffset * 20 - 60).ToString();
            NewTick3.GetComponent<Text>().text = (InitialYOffset * 20 - 40).ToString();
            NewTick4.GetComponent<Text>().text = (InitialYOffset * 20 - 20).ToString();
            NewTick5.GetComponent<Text>().text = (InitialYOffset * 20 - 0).ToString();
            NewTick6.GetComponent<Text>().text = (InitialYOffset * 20 + 20).ToString();
            NewTick7.GetComponent<Text>().text = (InitialYOffset * 20 + 40).ToString();
            NewTick8.GetComponent<Text>().text = (InitialYOffset * 20 + 60).ToString();
            NewTick9.GetComponent<Text>().text = (InitialYOffset * 20 + 80).ToString();

            SetUpComplete = true;
        }
    }

    private void GenerateNextHigherIndicator()
    {
        GameObject LowestTick = this.transform.GetChild(0).gameObject; //Lowest Tick used to create next higher tick.
        LowestTick.transform.localPosition += new Vector3(0f, 36f); //Move up to higher position.
        LowestTick.GetComponent<Text>().text = (int.Parse(LowestTick.GetComponent<Text>().text) + 180).ToString(); //Add speed to make it the new highest.
        LowestTick.transform.SetAsLastSibling(); //Last sibling should always be the highest number.
    }

    private void GenerateNextLowerIndicator()
    {
        GameObject HighestTick = this.transform.GetChild(8).gameObject; //Highest Tick used to create next lowest tick.
        HighestTick.transform.localPosition += new Vector3(0f, -36f); //Move down to lower position.
        HighestTick.GetComponent<Text>().text = (int.Parse(HighestTick.GetComponent<Text>().text) - 180).ToString(); //Lower speed to make it the new lowest.
        HighestTick.transform.SetAsFirstSibling(); //Last sibling should always be the lowest number.
    }
}
