using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttitudeHeadingHUD : MonoBehaviour
{
    public float indicatedHeading;
    public GameObject TickPrefab;
    public bool SetUpComplete;
    // Start is called before the first frame update
    void Start()
    {
        indicatedHeading = 0;
        SetUpComplete = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAttitudeHeading(float newHeading)
    {
        if (!SetUpComplete)
        {
            SetUp();
        }

        int OldAltitudeHeadingDivisor = (int)(indicatedHeading / 10);//Check to see if Tape has moved left or right one segment.
        indicatedHeading = newHeading;
        int newAltitudeHeadingDivisor = (int)(indicatedHeading / 10);


        float Xoffset = (indicatedHeading / 10);
        this.gameObject.transform.localPosition = new Vector3(-Xoffset * 50f, 0f); //50 x movement per 10 degrees.


        if (this.transform.childCount > 0)//Since heading can start anywhere, this puts the avaiable headings in view.
        {
            int TickDiscrepancy = newAltitudeHeadingDivisor - OldAltitudeHeadingDivisor;
            if (newAltitudeHeadingDivisor > OldAltitudeHeadingDivisor)
            {
                while (TickDiscrepancy > 0)
                {
                    GenerateNextHigherIndicator();
                    TickDiscrepancy--;
                }
            }
            else if (newAltitudeHeadingDivisor < OldAltitudeHeadingDivisor)
            {
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
        if (indicatedHeading != 0)
        {
            int AdjustedHeading = (int)indicatedHeading / 10;

            GameObject NewTick1 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick2 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick3 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);

            NewTick1.transform.localPosition = new Vector3(-50f + AdjustedHeading * 50, TickPrefab.transform.localPosition.y);
            NewTick2.transform.localPosition = new Vector3(0f + AdjustedHeading * 50, TickPrefab.transform.localPosition.y);
            NewTick3.transform.localPosition = new Vector3(50f + AdjustedHeading * 50, TickPrefab.transform.localPosition.y);

            NewTick1.transform.localRotation = Quaternion.identity;
            NewTick2.transform.localRotation = Quaternion.identity;
            NewTick3.transform.localRotation = Quaternion.identity;

            NewTick1.GetComponent<AttitudeHeadingTick>().SetText((AdjustedHeading - 1));
            NewTick2.GetComponent<AttitudeHeadingTick>().SetText((AdjustedHeading + 0));
            NewTick3.GetComponent<AttitudeHeadingTick>().SetText((AdjustedHeading + 1));

            SetUpComplete = true;
        }
    }

    private void GenerateNextHigherIndicator()
    {
        GameObject HighestTick = this.transform.GetChild(0).gameObject; //Lowest Tick used to create next higher tick.
        HighestTick.transform.localPosition += new Vector3(150f, 0f); //Move up to higher position. Next 3 numbers up is 150 away.
        int newNum = HighestTick.GetComponent<AttitudeHeadingTick>().GetRealNum() + 3;//Add span to make it the new highest.
        HighestTick.GetComponent<AttitudeHeadingTick>().SetText(newNum);
        HighestTick.transform.SetAsLastSibling(); //Last sibling should always be the highest number.
    }

    private void GenerateNextLowerIndicator()
    {
        GameObject LowestTick = this.transform.GetChild(2).gameObject; //Highest Tick used to create next lower tick.
        LowestTick.transform.localPosition -= new Vector3(150f, 0f); //Move down to lower position. Next 3 numbers down is 150 away.
        int newNum = LowestTick.GetComponent<AttitudeHeadingTick>().GetRealNum() - 3;//subtract span to make it the new lowest.
        LowestTick.GetComponent<AttitudeHeadingTick>().SetText(newNum);
        LowestTick.transform.SetAsFirstSibling(); //First sibling should always be the lowest number.
    }
}
