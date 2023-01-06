using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamTaille : MonoBehaviour
{
    [SerializeField] private GameObject Masque;
    
    public void ResolutionCam()
    {
        float ratioMasque = Masque.GetComponent<RectTransform>().localScale.x / Masque.GetComponent<RectTransform>().localScale.y;
        float ratioWebcam = this.GetComponent<Webcam>().resolution.x / this.GetComponent<Webcam>().resolution.y;
        
        if (ratioMasque > ratioWebcam)
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(1, ratioMasque/ratioWebcam, 1);
        }
        else
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(ratioWebcam/ratioMasque, 1, 1);
        }
    }
}
