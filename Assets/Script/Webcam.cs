using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Webcam : MonoBehaviour
{
    public Vector2 resolution;
    [SerializeField] private TMP_Dropdown dropdown;
    
    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();
        
        if (devices.Length > 0)
        {
            dropdown.ClearOptions();
            List<string> options = new List<string>();
            for (int i = 0; i < devices.Length; i++)
            {
                options.Add(devices[i].name);
            }
            dropdown.AddOptions(options);
        }
    }

    public void ChargeCam()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();

        if (devices.Length > 0)
        {
            webcamTexture.deviceName = devices[dropdown.value].name;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = webcamTexture;
            
            gameObject.GetComponent<RawImage>().texture = webcamTexture;

            webcamTexture.Play();
            
            resolution = new Vector2(webcamTexture.width, webcamTexture.height);
            GetComponent<WebcamTaille>().ResolutionCam(); 
        }
    }
}
