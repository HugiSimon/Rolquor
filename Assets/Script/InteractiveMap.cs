using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMap : MonoBehaviour
{
    private float _actualScale = 1;
    
    public float zoom = 1;
    [SerializeField] private CursonOn curseur;
    
    public bool tropLong = false;
    public bool tropLarge = false;
    
    void Start()
    {
        adaptScale();
    }
    
    void Update()
    {
        if (curseur.LeCurseur())
        {
            OnScroll();
        }
    }

    public void adaptScale()
    {
        //Adaptation du groupe Map
        Transform enfantImage = transform.GetChild(0);
        GetComponent<RectTransform>().sizeDelta = new Vector2(enfantImage.GetComponent<RectTransform>().rect.width, enfantImage.GetComponent<RectTransform>().rect.height);
        
        //Adaptation de la carte (image)
        GameObject parent = transform.parent.gameObject;
        float ratioParent = parent.GetComponent<RectTransform>().rect.width / parent.GetComponent<RectTransform>().rect.height;
        float ratioChild = GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.height;

        if (GetComponent<RectTransform>().rect.width < parent.GetComponent<RectTransform>().rect.width)
        {
            tropLong = true;
            GetComponent<RectTransform>().transform.localScale = new Vector3(parent.GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.width, parent.GetComponent<RectTransform>().rect.width / GetComponent<RectTransform>().rect.width, 1);
        }
        if (GetComponent<RectTransform>().rect.height * GetComponent<RectTransform>().localScale.x < parent.GetComponent<RectTransform>().rect.height)
        {
            tropLarge = true;
            GetComponent<RectTransform>().transform.localScale = new Vector3(parent.GetComponent<RectTransform>().rect.height / GetComponent<RectTransform>().rect.height, parent.GetComponent<RectTransform>().rect.height / GetComponent<RectTransform>().rect.height, 1);
        }
        _actualScale = GetComponent<RectTransform>().localScale.x;
        
        gameObject.GetComponent<MapInGame>().UpdateMax();
    }
    
    // Quand on scroll sur la carte (image) on zoom
    public void OnScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            zoom += 0.1f;
        }
        else if (scroll < 0f)
        {
            zoom -= 0.1f;
        }
        zoom = Mathf.Clamp(zoom, 1, 10);
        GetComponent<RectTransform>().transform.localScale = new Vector3(_actualScale * zoom, _actualScale * zoom, 1);
    }
}
