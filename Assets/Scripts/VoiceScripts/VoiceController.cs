//using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceController : MonoBehaviour//, IMixedRealityFocusHandler
{/*
    public GameObject SyntheticOverlay;
    public GameObject EnhancedOverlay;
    DictationHandler dictation;
    public TextMesh textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dictation = GetComponent<DictationHandler>();
    }

    // Update is called once per frame
    public void DictationHypothesis(string text)
    {
        Debug.Log($"DictationHypothesis: {text}");
        textDisplay.text = text;
    }

    public void DictationResult(string text)
    {
        Debug.Log($"DictationResult: {text}");
        textDisplay.text = text;
    }

    public void DictationCompleted(string text)
    {
        Debug.Log($"DictationCompleted: {text}");
        textDisplay.text = text;
    }

    public void DictationError(string text)
    {
        Debug.Log($"DictationError: {text}");
        textDisplay.text = text;
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        if(dictation != null)
        {
            dictation.StartRecording();
            textDisplay.text = "Dictation starting";
        }
    }

    public void OnFocusExit(FocusEventData eventData)
    {
        if (dictation != null)
        {
            dictation.StopRecording();
            textDisplay.text = "Dictation stopping";
        }
    }

    public void Activate1()
    {
        if (SyntheticOverlay.activeSelf)
        {
            SyntheticOverlay.SetActive(false);
        }
        else
        {
            SyntheticOverlay.SetActive(true);
        }
    }

    public void Activate2()
    {
        if (EnhancedOverlay.activeSelf)
        {
            EnhancedOverlay.SetActive(false);
        }
        else
        {
            EnhancedOverlay.SetActive(true);
        }
    }

    public void Jump()
    {
        this.transform.position += new Vector3(0f, 100f, 0f);
    }*/
}
