﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePrefabColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().color = GameObject.Find("UIEditor").GetComponent<ColorPicker>().GetCurrentUIColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
