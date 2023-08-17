using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoSingeleton<GameManager>
{
    public int index=0;
   

    private void Awake()
    {
        
        if (GameManager.Instance!= null)
        { 
           // Destroy(gameObject);
            return;
        }

   
      
        SceneManager.sceneLoaded += LoadState;// sahne yüklendiğinde otamatik load almaya yarar 
    }
    private void Start()
    {
     

    }


    public void SaveState()
    {
        string st = "";

      
 
        st += "0";
        // st =  120|23|0
        PlayerPrefs.SetString("SaveState", st);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {

        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        
   
    }



}