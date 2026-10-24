using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using TMPro;

public class WinLoseUI : MonoBehaviour {
    const int HOME = 0;
    const int RESET = 1;
    const int CONTINUE = 2;
    const int OPTION_COUNT = 3;

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
    int current_option = RESET;
    bool gameEnded = false;

    void Start() {
        pI = FindAnyObjectByType<Player_Movement>().pI;

        // FIX: always start a scene with normal time.
        Time.timeScale = 1;

        resetButton.onClick.AddListener(Reset);
        homeButton.onClick.AddListener(Home);
        continueButton.onClick.AddListener(NextLevel);
        continueButton.interactable = false;

        Hide(levelMenu);
        HighlightButton();
    }

    void OnEnable() {
        Door.killPlayer += Losing;
        Player_WinLoseHandler.Show_Win_Menu += Winning;
        Laser.killPlayer += Losing;
    }

    void OnDisable() {
        Door.killPlayer -= Losing;
        Player_WinLoseHandler.Show_Win_Menu -= Winning;
        Laser.killPlayer -= Losing;
    }

    void Update() {
        Keyboard kb = Keyboard.current;
        if (kb == null)
            return;

        if (!gameEnded && kb.escapeKey.wasPressedThisFrame) {
            Pause();
        }

        if (!levelMenu.activeInHierarchy)
            return;

        if (kb.upArrowKey.wasPressedThisFrame) {
            MoveOption(-1);
        }
        if (kb.downArrowKey.wasPressedThisFrame) {
            MoveOption(1);
        }
        if (kb.enterKey.wasPressedThisFrame) {
            switch (current_option) {
                case HOME:
                    Home();
                    break;
                case RESET:
                    Reset();
                    break;
                case CONTINUE:
                    if (continueButton.interactable)
                        NextLevel();
                    break;
            }
        }
    }

    // FIX: C# modulo can return a negative number.
    // This code adds OPTION_COUNT first, and it skips disabled buttons.
    void MoveOption(int step) {
        for (int i = 0; i < OPTION_COUNT; i++) {
            current_option = (current_option + step + OPTION_COUNT) % OPTION_COUNT;
            if (IsOptionEnabled(current_option))
                break;
        }
        HighlightButton();
    }

    bool IsOptionEnabled(int option) {
        return option != CONTINUE || continueButton.interactable;
    }

    void Losing() {
        // FIX: Laser calls this event each frame. Run it one time only.
        if (gameEnded)
            return;
        gameEnded = true;
        StartCoroutine(ProcessLosing(delay_time));
    }

    void Winning() {
        if (gameEnded)
            return;
        gameEnded = true;
        StartCoroutine(ProcessWinning(delay_time));
    }

    void Reset() {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Home() {
        LoadScene(1);
    }

    void NextLevel() {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next >= SceneManager.sceneCountInBuildSettings)
            next = 1;
        LoadScene(next);
    }

    // FIX: set timeScale back to 1 before a scene load.
    // Before this fix, the new scene stayed frozen.
    void LoadScene(int index) {
        DOTween.KillAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    void Pause() {
        if (levelMenu.activeInHierarchy) {
            Hide(levelMenu);
        }
        else {
            // FIX: show the pause title and use pause_color.
            menuTitle.text = "PAUSED";
            menuTitle.color = pause_color;
            current_option = RESET;
            HighlightButton();
            Show(levelMenu);
        }
    }

    void Show(GameObject g) {
        g.SetActive(true);
        pI.Player.Disable();
        Time.timeScale = 0;
    }

    void Hide(GameObject g) {
        g.SetActive(false);
        // FIX: do not enable input again after the game ends.
        if (!gameEnded)
            pI.Player.Enable();
        Time.timeScale = 1;
    }

    IEnumerator ProcessWinning(float delay) {
        pI.Player.Disable();
        yield return new WaitForSecondsRealtime(delay);
        menuTitle.text = "YOU ESCAPED";
        menuTitle.color = win_color;
        continueButton.interactable = true;
        current_option = CONTINUE;
        HighlightButton();
        Show(levelMenu);
    }

    IEnumerator ProcessLosing(float delay) {
        pI.Player.Disable();
        yield return new WaitForSecondsRealtime(delay);
        menuTitle.text = "YOU DIED";
        menuTitle.color = lose_color;
        continueButton.interactable = false;
        current_option = RESET;
        HighlightButton();
        Show(levelMenu);
    }

    void HighlightButton() {
        SetColor(homeButton, current_option == HOME);
        SetColor(resetButton, current_option == RESET);
        SetColor(continueButton, current_option == CONTINUE);
    }

    void SetColor(Button button, bool selected) {
        button.GetComponentInChildren<TextMeshProUGUI>().color =
            selected ? current_option_color : default_color;
    }
}