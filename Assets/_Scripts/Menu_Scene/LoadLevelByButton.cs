using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelByButton : MonoBehaviour
{
    [SerializeField] List<Button> button_list;
    public static event Action playClickButton;
    void Start()
    {
        foreach(Button button in button_list)
        {
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level" + (button_list.IndexOf(button)+1).ToString()); 
                playClickButton?.Invoke();
            });
        }
    }

    void Update()
    {
        
    }
}
