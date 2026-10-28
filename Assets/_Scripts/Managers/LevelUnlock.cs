using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelUnlock : MonoBehaviour
{
    public static LevelUnlock Instance {get; private set;}

    public static int[] level_unlock_state;
    void OnEnable()
    {
       Player_WinLoseHandler.Winning += LevelUnlockbyID;
    }
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy the duplicate object immediately
            Destroy(gameObject);
            return;
        }

        // Set this as the unique instance and persist it across scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);
        level_unlock_state = new int[16];
        level_unlock_state[1] = 1;
        for(int i = 2; i <= 15; i++)
        {
            level_unlock_state[i] = 0;
        }
        
    }
    void Update()
    {
        if(Keyboard.current.f1Key.wasPressedThisFrame)
        {
            for(int i = 2; i <= 15; i++)
        {
            level_unlock_state[i] = 1;
        }
        }
    }
    void LevelUnlockbyID()
    {
        LevelManager levelManager = FindAnyObjectByType<LevelManager>();
        int lv_id = levelManager.GetLevelID() + 1;
        level_unlock_state[lv_id] = 1;
        Debug.Log("Unlock level" + lv_id);
    }
}
