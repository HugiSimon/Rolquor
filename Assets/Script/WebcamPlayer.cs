using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.IO;
using Unity.VisualScripting;


public class WebcamPlayer : NetworkBehaviour
{
    private GameObject _webcamRawImage;
    public int webcamIndex = 0;

    public byte[] bytes;

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

        ParentServerRpc();
        
        Array.Resize(ref bytes, laWebcam.width * laWebcam.height * 4);
    }
    
    [ServerRpc]
    private void ParentServerRpc()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        //Debug.Log("Parent : " + transform.parent.name);

        for (int i = transform.parent.childCount - 1; i >= 0; i--)
        {
            transform.parent.GetChild(i).GetComponent<WebcamPlayer>().PositionAleatoireClientRpc();
            //Debug.Log("Child no " + i + " : " + transform.parent.GetChild(i).name);
        }
    }

    [ClientRpc]
    public void PositionAleatoireClientRpc()
    {
        if (transform.localScale.x > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, 1f);
        }
        
        transform.localPosition = new Vector3(Random.Range(-810, 810), Random.Range(-440, 440), 0);
        
        //Debug.Log("ClientRpc position");
    }

    private void Update()
    {
        if (IsOwner)
        {
            Texture2D _textureToSend = new Texture2D(_webcamRawImage.GetComponent<RawImage>().texture.width, _webcamRawImage.GetComponent<RawImage>().texture.height);
            _textureToSend.SetPixels(((WebCamTexture) _webcamRawImage.GetComponent<RawImage>().texture).GetPixels());
            _textureToSend.Apply();
            
            Array.Clear(bytes, 0, bytes.Length);
            bytes = _textureToSend.EncodeToPNG();
            Debug.Log("bytes : " + bytes.Length);
            
            
            
            TextureServerRpc(bytes, (int)NetworkManager.Singleton.LocalClientId);
        }
    }
    
    [ServerRpc]
    private void TextureServerRpc(byte[] bytesTexture, int clientId)
    {
        Debug.Log("ServerRpc texture " + bytesTexture.Length);
        //TextureClientRpc(bytesTexture, clientId);
    }
    
    [ClientRpc]
    private void TextureClientRpc(byte[] bytesLaTexture, int clientId)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytesLaTexture);
        
        Debug.Log("texture : " + texture.width + "x" + texture.height);
        
        GameObject.Find("TestTexture").GetComponent<RawImage>().texture = texture;
    }
}