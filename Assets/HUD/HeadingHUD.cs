using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadingHUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHeading(float newHeading)
    {
        this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, newHeading);
    }
}
