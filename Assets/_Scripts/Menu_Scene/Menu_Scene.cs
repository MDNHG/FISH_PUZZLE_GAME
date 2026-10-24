using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu_Scene : MonoBehaviour {
    public string nextScene;
    public TMP_Text[] menuOption;
    [SerializeField] Button start_button;
    [SerializeField] Button quit_button;

    // Defaults to the first index option
    private int optionIndex = 0;
    private int NO_OF_OPTIONS = 3; // Levels, Settings, Quit

    void Start() {
        // Defaults to selecting first option first
        HighlightOption(optionIndex);
        start_button.onClick.AddListener(()=>
        {
            AudioBus.SendButtonClickAudio();
            SceneManager.LoadScene(nextScene);
        });
        quit_button.onClick.AddListener(()=>
        {
            AudioBus.SendButtonClickAudio();
            Application.Quit(); 
        });
    }
    void Update() {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        if (keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame) {
            optionIndex = (optionIndex + 1) % NO_OF_OPTIONS;
            HighlightOption(optionIndex);
            AudioBus.SendChangeOptionAudio();
        }
        else if (keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame) {
            optionIndex = (optionIndex + (NO_OF_OPTIONS - 1)) % NO_OF_OPTIONS;
            HighlightOption(optionIndex);
            AudioBus.SendChangeOptionAudio();
        }

        // Confirming option
        if (keyboard.zKey.wasPressedThisFrame || keyboard.enterKey.wasPressedThisFrame) {
            ChooseOption();
        }
    }

    private void ChooseOption() {
        // Levels
        if (optionIndex == 0) {
            if (!string.IsNullOrEmpty(nextScene))
                SceneManager.LoadScene(nextScene);
        }

        // Quit; always the last option
        if (optionIndex == NO_OF_OPTIONS - 1) {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false; // Editor
#endif
            Application.Quit(); // Build
        }
    }

    // Swaps options' color between highlighted (yellow) and normal (white)
    private void HighlightOption(int index) {
        for (int i = 0; i < NO_OF_OPTIONS; i++) {
            var text = menuOption[i].GetComponentInChildren<TMP_Text>();
            text.color = (i == index) ? Color.yellow : Color.white;
        }
    }
}