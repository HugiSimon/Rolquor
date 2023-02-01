using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHostJoin : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;

    private GameObject[] _lesCam;
    
    private void Start()
    {
        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
        
        _lesCam = GameObject.FindGameObjectsWithTag("TestTexture");
        
        foreach (GameObject cam in _lesCam)
        {
            cam.SetActive(false);
        }
    }
    
    private void Host()
    {
        if (NetworkManager.Singleton.IsListening)
        {
            NetworkManager.Singleton.Shutdown();
        }
        
        NetworkManager.Singleton.StartHost();
        
        hostButton.gameObject.SetActive(false);
        joinButton.gameObject.SetActive(false);
        
        foreach (GameObject cam in _lesCam)
        {
            cam.SetActive(true);
        }
    }
    
    private void Join()
    {
        NetworkManager.Singleton.StartClient();
        
        hostButton.gameObject.SetActive(false);
        joinButton.gameObject.SetActive(false);
        
        foreach (GameObject cam in _lesCam)
        {
            cam.SetActive(true);
        }
    }
}
