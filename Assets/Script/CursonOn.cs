using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursonOn : MonoBehaviour
{
    private bool _leCurseurEstOn = false;
    
    public bool LeCurseur()
    {
        return _leCurseurEstOn;
    }
    
    private void OnMouseEnter()
    {
        _leCurseurEstOn = true;
    }
    
    private void OnMouseExit()
    {
        _leCurseurEstOn = false;
    }
}
