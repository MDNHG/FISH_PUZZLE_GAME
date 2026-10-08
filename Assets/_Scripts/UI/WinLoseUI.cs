using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
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
    bool gameEnded = false;
    
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
        Hide(losingMenu);
        Hide(pauseMenu);
        Hide(winningMenu);
    }
    void OnEnable()
    {
        Door.killPlayer += Losing;
        Player_Movement.EnterWarpGate += Winning;
        Laser.killPlayer += Losing;
        
    }
    void OnDisable()
    {
        Door.killPlayer -= Losing;
        Player_Movement.EnterWarpGate -= Winning;
        Laser.killPlayer -= Losing;
        
    }
    void Update()
    {
        if (!gameEnded && Keyboard.current.escapeKey.wasPressedThisFrame) {
            if (!pauseMenu.activeInHierarchy) {
                Show(pauseMenu);
            }
            else {
                Hide(pauseMenu);
            }
        }        
    }
    void Losing()
    {
        gameEnded = true;
        Show(losingMenu);
    }
    void Winning()
    {
        gameEnded = true;
        Show(winningMenu);
    }
    void Reset()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Home()
    {
        SceneManager.LoadScene(1);
    }
    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Show(GameObject g)
    {
        g.SetActive(true);
        pI.Player.Disable();
        Time.timeScale = 0;

    }
    void Hide(GameObject g)
    {
        g.SetActive(false);
        pI.Player.Enable();
        Time.timeScale = 1;
    }
}
