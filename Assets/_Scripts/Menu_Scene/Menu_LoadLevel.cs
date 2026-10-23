using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu_LoadLevel : MonoBehaviour {
    public string backScene;
    public TMP_Text[] menuOption;

    // Defaults to the first index option
    private int optionIndex = 0;
    private int NO_OF_OPTIONS = 16; // 15 levels + back button
    void Start() {
        // Defaults to selecting first option first
        HighlightOption(optionIndex);
    }

    void Update() {
        // Get the current keyboard. It is null if no keyboard is connected.
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        // Alternating between menu options
        if (keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame) {
            optionIndex = (optionIndex + 5) % NO_OF_OPTIONS;
            HighlightOption(optionIndex);
        }
        else if (keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame) {
            optionIndex = (optionIndex + (NO_OF_OPTIONS - 5)) % NO_OF_OPTIONS;
            HighlightOption(optionIndex);
        }
        else if (keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame) {
            optionIndex = (optionIndex + NO_OF_OPTIONS - 1) % NO_OF_OPTIONS;
            HighlightOption(optionIndex);
        }
        else if (keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame) {
            optionIndex = (optionIndex + 1) % NO_OF_OPTIONS;
            HighlightOption(optionIndex);
        }

        // Confirming option
        if (keyboard.zKey.wasPressedThisFrame || keyboard.enterKey.wasPressedThisFrame) {
            ChooseOption();
            AudioBus.SendButtonClickAudio();
        }
    }

    private void ChooseOption() {
        if (optionIndex == 0) {
            if (!string.IsNullOrEmpty(backScene))
                SceneManager.LoadScene(backScene);
        }
        else {
            var text = menuOption[optionIndex].GetComponent<MenuNumber_LoadLevel>().level;
            if (text != null && LevelUnlock.level_unlock_state[optionIndex] == 1) {
                SceneManager.LoadScene(text);
            }
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