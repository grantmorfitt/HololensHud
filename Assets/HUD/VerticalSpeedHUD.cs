using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSpeedHUD : MonoBehaviour
{
    public float verticalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVerticalSpeed(float newVerticalSpeed)
    {
        verticalSpeed = newVerticalSpeed;
        if(verticalSpeed >= 0)
        {
            if (verticalSpeed < 2000)
            {
                //0.377 per first 2000 fpm = 0.0001885
                verticalSpeed = verticalSpeed * 0.0001885f;
            }
            else if(verticalSpeed < 6000)
            {
                //(0.517 - 0.377) = 0.14 per last 4000 fpm = 0.000035
                verticalSpeed = (verticalSpeed - 2000f) * 0.000035f + 0.377f; 
            }
            else
            {
                verticalSpeed = 0.517f; //Limit positive value at 6000 feet.
            }
        }
        else
        {
            if (verticalSpeed > -2000)
            {
                //0.377 per first 2000 fpm = 0.0001885
                verticalSpeed = verticalSpeed * 0.0001885f;
                
            }
            else if(verticalSpeed > - 7000)
            {
                //(0.517 - 0.377) = 0.14 per last 4000 fpm = 0.000035
                verticalSpeed = (verticalSpeed + 2000f) * 0.000035f - 0.377f;
            }
            else
            {
                verticalSpeed = -0.517f; //Limit negative value at 6000 feet.
            }
        }
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, verticalSpeed);
    }
}
