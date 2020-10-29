using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPathVector : MonoBehaviour
{
    public float RelativePitch;
    public float RelativeHeading;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateFPV(float relativePitch, float relativeHeading)
    {
        RelativePitch = relativePitch;
        RelativeHeading = relativeHeading;
        this.transform.localPosition = new Vector3(-RelativeHeading * 6.6f, -RelativePitch * 2.08f);
        //10.4 Units of Y movement per 5 degrees pitch = 2.08
        //66 movements of X movement per 10 degrees heading = 6.6
    }
}
