using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class WebcamPlayer : NetworkBehaviour
{
    private GameObject _webcamRawImage;
    public int webcamIndex = 0;

    private void Start()
    {
        if (!IsOwner) return;
        
        _webcamRawImage = transform.GetChild(0).gameObject;

        WebCamTexture laWebcam = new WebCamTexture();
        laWebcam.deviceName = WebCamTexture.devices[webcamIndex].name;
        
        Renderer component = _webcamRawImage.GetComponent<Renderer>();
        component.material.mainTexture = laWebcam;

        _webcamRawImage.GetComponent<RawImage>().texture = laWebcam;
        
        laWebcam.Play();
        
        //Si host
        if (IsServer)
        {
            transform.SetParent(GameObject.Find("Canvas").transform);
            
            transform.localScale = new Vector3(1, 1, 1);
            transform.localPosition = new Vector3(Random.Range(-810, 810), Random.Range(-440, 440), 0);
        }
    }
}