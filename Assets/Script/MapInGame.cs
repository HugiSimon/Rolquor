using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInGame : MonoBehaviour
{
    private int _maxX = 0;
    private int _maxY = 0;
    
    public GameObject marqueur;

    public void UpdateMax()
    {
        _maxX = (int)gameObject.GetComponent<RectTransform>().rect.width;
        _maxY = (int)gameObject.GetComponent<RectTransform>().rect.height;
        Debug.Log(_maxX + " " + _maxY);
    }

    private void Update()
    {
        if (transform.GetComponentInParent<CursonOn>().LeCurseur())
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    //Quand on clique sur la carte (image) on deplace le marqueur
    public void OnClick()
    {
        Vector2 position = Input.mousePosition;
        position.x -= Screen.width / 2;
        position.y -= Screen.height / 2;
        
        Debug.Log("horizontal:" + transform.parent.GetComponent<ScrollRect>().horizontalNormalizedPosition + " vertical:" + transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition);

        marqueur.transform.localPosition = new Vector3(-174.6f * position.x / -313.18f, (-147.3f * position.y / -250.10f) + ((Screen.height / 2) * (transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition - 0.5f)), 0);
    }
}
