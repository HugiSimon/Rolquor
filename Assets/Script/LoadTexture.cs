using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTexture : MonoBehaviour
{
    public byte[] laTextures;
    public int ID;

    public void intergreTableau(byte[] tableau, int taillePaquet, int nombrePaquet)
    {
        Debug.Log("intergreTableau / tableau.Length : " + tableau.Length + " / taillePaquet : " + taillePaquet + " / nombrePaquet : " + nombrePaquet);
        if (nombrePaquet == 0)
        {
            laTextures = tableau;
        }
        else
        {
            tableau.CopyTo(laTextures, taillePaquet * nombrePaquet);
        }

        if (taillePaquet < 65000)
        {
            afficherTexture();
        }
    }
    
    public void afficherTexture()
    {
        Texture2D texture = new Texture2D(300, 175);
        texture.LoadImage(laTextures);
        GetComponent<RawImage>().texture = texture;
        
        Debug.Log("afficherTexture / laTextures.Length : " + laTextures.Length);
        
        Array.Clear(laTextures, 0, laTextures.Length);
        //Destroy(texture);
    }
}