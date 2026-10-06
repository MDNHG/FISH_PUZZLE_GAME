using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework.Constraints;
using System.Threading;
using UnityEngine.InputSystem;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] GameObject losingMenu;
    [SerializeField] GameObject winningMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] List<Button> resetButton;
    [SerializeField] List<Button> homeButton;
    [SerializeField] List<Button> continueButton;
    Player_Input pI;
    
    void Start()
    {
        pI = FindAnyObjectByType<Player_Movement>().pI;
        foreach(Button button in resetButton)
        {
           button.onClick.AddListener(()=> Reset());
        }
         foreach(Button button in homeButton)
        {
           button.onClick.AddListener(()=> Home());
        }
          foreach(Button button in continueButton)
        {
           button.onClick.AddListener(()=> NextLevel());
        }
        Door.crushPlayer += Losing;
        Player_Movement.EnterWarpGate += Winning;
        Laser.roastPlayer += Losing;
        losingMenu.SetActive(false);
        winningMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame && !pauseMenu.activeInHierarchy)
        {
            Pause();
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && pauseMenu.activeInHierarchy)
        {
            UnPause();
        }
        
    }
    void Losing()
    {
        losingMenu.SetActive(true);
        pI.Player.Disable();   
    }
    void Winning()
    {
        winningMenu.SetActive(true);
        pI.Player.Disable();
    }
    void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Home()
    {
        Debug.Log("Return");
        SceneManager.LoadScene(1);
    }
    void NextLevel()
    {
        Debug.Log("Next");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        pI.Player.Disable();

    }
    void UnPause()
    {
        pauseMenu.SetActive(false);
        pI.Player.Enable();
    }
}
