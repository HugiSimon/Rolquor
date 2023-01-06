using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Webcam : MonoBehaviour
{
    public Vector2 resolution;
    
    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();
        
        if (devices.Length > 0)
        {
            webcamTexture.deviceName = devices[0].name;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = webcamTexture;
            
            gameObject.GetComponent<RawImage>().texture = webcamTexture;

            webcamTexture.Play();
            
            resolution = new Vector2(webcamTexture.width, webcamTexture.height);
            GetComponent<WebcamTaille>().ResolutionCam();
        }
    }
}
