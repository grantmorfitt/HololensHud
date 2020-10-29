using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltitudeHUD : MonoBehaviour
{
    public float indicatedAltitude = 0;
    public bool SetUpComplete;
    public GameObject TickPrefab;
    public GameObject MasterIndicator;
    // Start is called before the first frame update
    void Start()
    {
        SetUpComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAltitude(float NewAltitude)
    {
        if(!SetUpComplete)
        {
            SetUp();
        }
        //Tracks to see if the aircraft has moved to the next 200 ft margin.
        //If it has, generate a new higher or lower tick mark.
        int OldAltitudeDivisor = (int)(indicatedAltitude / 200); //Checks to see if tape has moved down one segment.
        indicatedAltitude = NewAltitude;
        int NewAltitudeDivisor = (int)(indicatedAltitude / 200); 

        float YOffset = indicatedAltitude / 200 * 6;//Six units of Y movement per 200 feet.
        //Debug.Log(YOffset);
        int intAlt = (int)NewAltitude; //Remove decimal places.
        MasterIndicator.GetComponent<TickHUD>().SetExactAltitude(intAlt); //Set exact altitude indicator.
        this.transform.localPosition = new Vector3(this.transform.position.x, -YOffset);//Move tape down when moving up.
        if (this.transform.childCount > 0)
        {
            if (NewAltitudeDivisor > OldAltitudeDivisor)
            {
                int TickDiscrepancy = NewAltitudeDivisor - OldAltitudeDivisor; //In case of data flow interruption, detects how many divisions apart the gauge and data is.
                while (TickDiscrepancy > 0)
                {
                    GenerateNextHigherIndicator();
                    TickDiscrepancy--;
                }
            }
            else if (NewAltitudeDivisor < OldAltitudeDivisor)
            {
                int TickDiscrepancy = NewAltitudeDivisor - OldAltitudeDivisor; //In case of data flow interruption, detects how many divisions apart the gauge and data is.
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
        if (indicatedAltitude != 0f)
        {
            int InitialYOffset = (int)indicatedAltitude / 200;

            //Debug.Log(indicatedAltitude);
            //Debug.Log(InitialYOffset);

            /*GameObject NewTick1 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick2 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick3 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick4 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick5 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
            GameObject NewTick6 = Instantiate(TickPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);*/

            GameObject NewTick1 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick2 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick3 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick4 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick5 = Instantiate(TickPrefab, this.transform, false);
            GameObject NewTick6 = Instantiate(TickPrefab, this.transform, false);

            NewTick1.transform.localPosition = new Vector3(TickPrefab.transform.localPosition.x, InitialYOffset*6 - 12);
            NewTick2.transform.localPosition = new Vector3(TickPrefab.transform.localPosition.x, InitialYOffset*6 - 6);
            NewTick3.transform.localPosition = new Vector3(TickPrefab.transform.localPosition.x, InitialYOffset*6 - 0);
            NewTick4.transform.localPosition = new Vector3(TickPrefab.transform.localPosition.x, InitialYOffset*6 + 6);
            NewTick5.transform.localPosition = new Vector3(TickPrefab.transform.localPosition.x, InitialYOffset*6 + 12);
            NewTick6.transform.localPosition = new Vector3(TickPrefab.transform.localPosition.x, InitialYOffset*6 + 18);

            NewTick1.GetComponent<TickHUD>().SetAltitude(InitialYOffset * 200 - 400);
            NewTick2.GetComponent<TickHUD>().SetAltitude(InitialYOffset * 200 - 200);
            NewTick3.GetComponent<TickHUD>().SetAltitude(InitialYOffset * 200 - 0);
            NewTick4.GetComponent<TickHUD>().SetAltitude(InitialYOffset * 200 + 200);
            NewTick5.GetComponent<TickHUD>().SetAltitude(InitialYOffset * 200 + 400);
            NewTick6.GetComponent<TickHUD>().SetAltitude(InitialYOffset * 200 + 600);

            SetUpComplete = true;
        }
    }
    private void GenerateNextHigherIndicator()
    {
        GameObject LowestTick = this.transform.GetChild(0).gameObject; //Lowest Tick used to create next higher tick.
        LowestTick.transform.localPosition += new Vector3(0f, 36f); //Move up to higher position.
        LowestTick.GetComponent<TickHUD>().SetAltitude(LowestTick.GetComponent<TickHUD>().GetShownAltitude() + 1200); //Add height to make it the new highest.
        LowestTick.transform.SetAsLastSibling(); //Last sibling should always be the highest number.
    }

    private void GenerateNextLowerIndicator()
    {
        GameObject HighestTick = this.transform.GetChild(5).gameObject; //Highest Tick used to create next lowest tick.
        HighestTick.transform.localPosition += new Vector3(0f, -36f); //Move down to lower position.
        HighestTick.GetComponent<TickHUD>().SetAltitude(HighestTick.GetComponent<TickHUD>().GetShownAltitude() - 1200); //Lower height to make it the new lowest.
        HighestTick.transform.SetAsFirstSibling(); //Last sibling should always be the lowest number.
    }
}
