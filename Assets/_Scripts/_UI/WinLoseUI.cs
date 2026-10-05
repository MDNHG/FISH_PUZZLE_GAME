using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework.Constraints;
using System.Threading;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] GameObject losingMenu;
    [SerializeField] GameObject winningMenu;
    [SerializeField] List<Button> resetButton;
    [SerializeField] List<Button> homeButton;
    [SerializeField] List<Button> continueButton;
    
    void Start()
    {
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
    }
    void Update()
    {
        
    }
    void Losing()
    {
        losingMenu.SetActive(true);   
    }
    void Winning()
    {
        winningMenu.SetActive(true);
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
}
