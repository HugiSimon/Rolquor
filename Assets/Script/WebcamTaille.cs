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
        float ratioWebcam = this.GetComponent<WebcamLoad>().resolution.x / this.GetComponent<WebcamLoad>().resolution.y;
        
        if (ratioMasque > ratioWebcam) // Si le masque est plus large que la webcam
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(1, ratioMasque/ratioWebcam, 1); // On redimensionne la webcam pour qu'elle ait la même hauteur que le masque
        }
        else
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(ratioWebcam/ratioMasque, 1, 1); // On redimensionne pour la même largeur que le masque
        }
    }
}
