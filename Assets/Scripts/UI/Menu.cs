using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    
    public void ResumeGame()
    {
        GameManager.isResume = true;
        SceneManager.LoadScene("Game");
    }

    public void StartNewGame()
    {
        //TODO add nick
        GameManager.isResume = false;
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        resumeButton.interactable = SaveManager.ContinuationAvailable();
    }
}
