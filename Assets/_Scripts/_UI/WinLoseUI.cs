using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            button.onClick.AddListener(Reset);
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
}
