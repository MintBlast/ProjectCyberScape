﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoSampleScene()
    {
        //loads the game test scene
        SceneManager.LoadScene("TestScene");
    }

    public void GotoMenuScene()
    {
        //loads the menu scene 
        SceneManager.LoadScene("MenuScene");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void GotoSampleScene()
    {
        SceneManager.LoadScene("");
    }
}

