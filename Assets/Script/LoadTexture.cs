using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTexture : MonoBehaviour
{
    public byte[] laTextures;
    public int ID;

    private void Start()
    {
        //Taille de laTextures maximal est de 1000000
        laTextures = new byte[1000000];
    }

    public void intergreTableau(byte[] tableau, int taillePaquet, int nombrePaquet)
    {
        if (nombrePaquet == 0)
        {
            laTextures = tableau;
        }
        else
        {
            Array.Resize(ref laTextures, laTextures.Length + taillePaquet);
            Array.Copy(tableau, 0, laTextures, 65000 * nombrePaquet, taillePaquet);
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

        Array.Clear(laTextures, 0, laTextures.Length);
        //Destroy(texture);
    }
}