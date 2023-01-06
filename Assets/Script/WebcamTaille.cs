using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamTaille : MonoBehaviour
{
    [SerializeField] private GameObject Masque;
    
    public void ResolutionCam()
    {
        Debug.Log("Ratio Masque : " + Masque.GetComponent<RectTransform>().localScale.x / Masque.GetComponent<RectTransform>().localScale.y 
                                    + " Ratio Webcam : " + this.GetComponent<Webcam>().resolution.x / this.GetComponent<Webcam>().resolution.y);
    }
}
