using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Color DesColor;
    public List<Transform> Images;
    public List<Transform> Texts;

// Start is called before the first frame update
    void Start()
    {
        foreach (Transform image in Images)
        {
            image.GetComponent<Image>().color = DesColor;
        }
        foreach (Transform text in Texts)
        {
            text.GetComponent<Text>().color = DesColor;
        }
    }

    public Color GetCurrentUIColor()
    {
        return DesColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
