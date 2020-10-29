using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttitudeHUD : MonoBehaviour
{
    public GameObject PitchTicks;
    public GameObject RollHud;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePitch(float pitch)//In degrees
    {
        PitchTicks.transform.localPosition = new Vector3(0, -pitch*4f); //20 units Y per 5 degrees
    }

    public void UpdateRoll(float newRoll)
    {
        RollHud.transform.localRotation = Quaternion.Euler(0, 0, newRoll);
    }
}
