using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button continueButton=null;
    [SerializeField] private string[] RandNames; 

    private const string PlayerPrefsNameKey="Player";
    private void Start()
    {
        SetUpInputField();
    }
    public void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) return;

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        nameInputField.text = defaultName;
        setPlayerName(defaultName);
    }

    public void setPlayerName(string defaultName)
    {
        continueButton.interactable = !string.IsNullOrEmpty(defaultName);
        
    }

public void savePlayerName()
    {
        string playerName = nameInputField.text;
        PhotonNetwork.NickName = playerName;
        PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);

    }


    public void RandomNameButton()
    {

        int randomInt = UnityEngine.Random.Range(0, RandNames.Length-1);
        nameInputField.text = RandNames[randomInt];

    }
}
