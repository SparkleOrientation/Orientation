using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.MonoBehaviour
{

    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    public Text PingText;

    private void Awake()
    {
        GameCanvas.SetActive(true);
    }

    private void Update()
    {
        PingText.text = "Ping " + PhotonNetwork.GetPing();
    }

    public void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(-76, 38), Quaternion.identity, 0);
        GameCanvas.SetActive(false);  
        SceneCamera.SetActive(false);
    }

}
