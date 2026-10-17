using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System.Data;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] GameObject levelMenu;
    [SerializeField] Button resetButton;
    [SerializeField] Button homeButton;
    [SerializeField] Button continueButton;
    [SerializeField] TextMeshProUGUI menuTitle;
    [Header("Color setup")]
    [SerializeField] Color win_color;
    [SerializeField] Color lose_color;
    [SerializeField] Color pause_color;
    [SerializeField] Color current_option_color;
    [SerializeField] Color default_color;
    [Header("Timing Setup")]
    [SerializeField] float delay_time = 5;
    Player_Input pI;
    private int current_option = 2;
    bool gameEnded = false;
    
    void Start()
    {
        pI = FindAnyObjectByType<Player_Movement>().pI;
        
        resetButton.onClick.AddListener(()=> Reset());
        homeButton.onClick.AddListener(()=> Home());
        continueButton.onClick.AddListener(()=> NextLevel());
        continueButton.interactable = false;
        Hide(levelMenu);
        HightlightButton();
    }
    void OnEnable()
    {
        Door.killPlayer += Losing;
        Player_WinLoseHandler.Show_Win_Menu += Winning;
        Laser.killPlayer += Losing;
        
    }
    void OnDisable()
    {
        Door.killPlayer -= Losing;
         Player_WinLoseHandler.Show_Win_Menu -= Winning;
        Laser.killPlayer -= Losing;
        
    }
    void Update()
    {
        if (!gameEnded && Keyboard.current.escapeKey.wasPressedThisFrame) {
            Pause();
        } 
        if(Keyboard.current.upArrowKey.wasPressedThisFrame && levelMenu.activeInHierarchy)
        {
            current_option = (current_option + 1)%3;
            HightlightButton();

        }    
        if(Keyboard.current.downArrowKey.wasPressedThisFrame && levelMenu.activeInHierarchy)
        {
            current_option = (current_option - 1)%3;
            HightlightButton();
        }
        if(Keyboard.current.enterKey.wasPressedThisFrame && levelMenu.activeInHierarchy)
        {
            switch(current_option)
        {
            case 0:
            Home();
            break;
            case 1:
            Reset();
            break;
            case 2:
            if(continueButton.interactable)
            NextLevel();
            break;
        }
        }
    }
    void Losing()
    {
       StartCoroutine(ProcessLosing(delay_time));
    }
    void Winning()
    {
        StartCoroutine(ProcessWinning(delay_time));
    }
    void Reset()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Pause()
    {
         if (!levelMenu.activeInHierarchy) {
                Show(levelMenu);
            }
            else {
                Hide(levelMenu);
            }
    }
    void Home()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(1);
    }
    void NextLevel()
    {
        DOTween.KillAll();
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
    IEnumerator ProcessWinning(float delay)
    {
        pI.Player.Disable();
        yield return new WaitForSecondsRealtime(delay);
        menuTitle.text = "YOU ESCAPED";
        menuTitle.color = win_color;
        continueButton.interactable = true;
        gameEnded = true;
        Show(levelMenu);
    }
    IEnumerator ProcessLosing(float delay)
    {
        pI.Player.Disable();
        yield return new WaitForSecondsRealtime(delay);
        menuTitle.text = "YOU DIED";
        menuTitle.color = lose_color;
        continueButton.interactable = false;
        gameEnded = true;
        Show(levelMenu);
    }
    void HightlightButton()
    {
        homeButton.GetComponentInChildren<TextMeshProUGUI>().color = default_color;
        resetButton.GetComponentInChildren<TextMeshProUGUI>().color = default_color;
        continueButton.GetComponentInChildren<TextMeshProUGUI>().color = default_color;
        switch(current_option)
        {
            case 0:
            homeButton.GetComponentInChildren<TextMeshProUGUI>().color = current_option_color;
            break;
            case 1:
            resetButton.GetComponentInChildren<TextMeshProUGUI>().color = current_option_color;
            break;
            case 2:
            continueButton.GetComponentInChildren<TextMeshProUGUI>().color = current_option_color;
            break;
        }
    }
}
