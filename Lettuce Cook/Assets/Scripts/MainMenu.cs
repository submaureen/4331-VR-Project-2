﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayGameVR()
    {
        SceneManager.LoadScene("VR Main");

    }

    public void Quit()
    {
        Application.Quit();
    }


}
