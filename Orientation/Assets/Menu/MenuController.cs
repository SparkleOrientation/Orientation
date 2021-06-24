using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;
    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;
    [SerializeField] private GameObject StartButton;
    
    private void Awake()
    {

        PhotonNetwork.ConnectUsingSettings(VersionName);
    }
    private void Start()
    {

        UsernameMenu.SetActive(true);
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        SoundManagerScript.PlaySound("menuMove");
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateRoom()
    {
        SoundManagerScript.PlaySound("menuMove");
        PhotonNetwork.CreateRoom(JoinGameInput.text, new RoomOptions() {maxPlayers = 5}, null);
        PlayerPrefs.SetInt("gamecreated",1);
    }

    public void JoinGame()
    {
        SoundManagerScript.PlaySound("menuMove");
        RoomOptions roomOptions = new RoomOptions();
         roomOptions.maxPlayers = 5;
         PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
         PlayerPrefs.SetInt("gamecreated",0);
    }

    private void OnJoinedRoom()
    {
         PhotonNetwork.LoadLevel("MainGame");
    }
}
