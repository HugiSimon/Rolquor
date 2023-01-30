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
    
    public byte[][] bytesArray;

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

        bytesArray = new byte[5][];
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
            
            Texture2D resizedTexture = new Texture2D(300, 175);
            resizedTexture = Resizing(_textureToSend, 300, 175);
            
            bytes = resizedTexture.EncodeToPNG();
            
            // Boucle pour n'evoyer que des paquets de 65000 bytes.Length
            int bytesLong = bytes.Length, j = 0, taille = 65000;
            for (int i = 0; i < bytesLong; i += taille)
            {
                byte[] bytesToSend = new byte[65000];
                if (bytesLong - (taille * j) > taille)
                {
                    Array.Copy(bytes, i, bytesToSend, 0, taille);
                    TextureServerRpc(bytesToSend, (int)NetworkManager.Singleton.LocalClientId, taille, j);
                }
                else
                {
                    Array.Copy(bytes, i, bytesToSend, 0, bytesLong - (taille * j));
                    TextureServerRpc(bytesToSend, (int)NetworkManager.Singleton.LocalClientId, bytesLong - (taille * j), j);
                }
                j++;
                Array.Clear(bytesToSend, 0, bytesToSend.Length);
            }
            
            Array.Clear(bytes, 0, bytes.Length);
            Destroy(_textureToSend);
            Destroy(resizedTexture);
        }
    }
    
    [ServerRpc]
    private void TextureServerRpc(byte[] bytesTexture, int clientId, int taillePaquet, int nombrePaquets)
    {
        TextureClientRpc(bytesTexture, clientId, taillePaquet, nombrePaquets);
    }
    
    [ClientRpc]
    private void TextureClientRpc(byte[] bytesLaTexture, int clientId, int taillePaquet, int nombrePaquets)
    {
        GameObject[] testTexture = GameObject.FindGameObjectsWithTag("TestTexture");
        for (int i = 0; i < testTexture.Length; i++)
        {
            if (testTexture[i].GetComponent<LoadTexture>().ID == clientId)
            {
                testTexture[i].GetComponent<LoadTexture>().intergreTableau(bytesLaTexture, taillePaquet, nombrePaquets);
                Debug.Log("Trouvé");
            }
        }
    }
    
    public static Texture2D Resizing(Texture2D source, int newWidth, int newHeight)
    {
        source.filterMode = FilterMode.Bilinear;
        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        rt.filterMode = FilterMode.Bilinear;
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);
        Texture2D nTex = new Texture2D(newWidth, newHeight);
        nTex.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0,0);
        nTex.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        return nTex;
    }
}

