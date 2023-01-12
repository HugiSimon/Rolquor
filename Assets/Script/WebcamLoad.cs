using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WebcamLoad : MonoBehaviour
{
    public Vector2 resolution;
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices; // Liste des webcams
        WebCamTexture webcamTexture = new WebCamTexture();
        
        if (devices.Length <= 0) return; // Rempli le dropdown de toutes les webcams differentes
        
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < devices.Length; i++)
        {
            options.Add(devices[i].name);
        }

        dropdown.AddOptions(options);
    }

    public void ChargeCam() // Quand on click sur OK
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();

        if (devices.Length <= 0) return;
        
        webcamTexture.deviceName = devices[dropdown.value].name;
            
        Renderer renderer = GetComponent<Renderer>(); // On recupere le renderer de l'objet
        renderer.material.mainTexture = webcamTexture; // On lui donne la texture de la webcam
            
        gameObject.GetComponent<RawImage>().texture = webcamTexture;

        webcamTexture.Play(); // On lance la webcam

        resolution = new Vector2(webcamTexture.width, webcamTexture.height); // On stocke la resolution de la webcam
        GetComponent<WebcamTaille>().ResolutionCam(); // On appelle la fonction qui va changer la taille de la webcam
    }
}
