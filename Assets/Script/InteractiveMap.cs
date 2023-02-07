using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMap : MonoBehaviour
{
    void Start()
    {
        adaptScale();
    }

    public void adaptScale()
    {
        Transform enfantImage = transform.GetChild(0);
        GetComponent<RectTransform>().sizeDelta = new Vector2(enfantImage.GetComponent<RectTransform>().rect.width, enfantImage.GetComponent<RectTransform>().rect.height);
        
        GameObject parent = transform.parent.gameObject;
        float ratioParent = parent.GetComponent<RectTransform>().rect.width / parent.GetComponent<RectTransform>().rect.height;
        float ratioChild = GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.height;

        if (GetComponent<RectTransform>().rect.width < parent.GetComponent<RectTransform>().rect.width)
        {
            GetComponent<RectTransform>().transform.localScale = new Vector3(parent.GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.width, parent.GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.width, 1);
        }
        if (GetComponent<RectTransform>().rect.height * GetComponent<RectTransform>().localScale.x < parent.GetComponent<RectTransform>().rect.height)
        {
            GetComponent<RectTransform>().transform.localScale = new Vector3(parent.GetComponent<RectTransform>().rect.height / GetComponent<RectTransform>().rect.height, parent.GetComponent<RectTransform>().rect.height / GetComponent<RectTransform>().rect.height, 1);
        }
    }
}
