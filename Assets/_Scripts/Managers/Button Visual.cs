using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class ButtonVisual : MonoBehaviour
{

    private List<Image> button_image_list;
    [SerializeField] Color unlock_color;
    [SerializeField] Color lock_color;
    
    void Start()
    {
        button_image_list = new List<Image>();
        foreach(Image temp in GetComponentsInChildren<Image>())
        {
            button_image_list.Add(temp);
        }
        if(LevelUnlock.level_unlock_state == null)
        {
            for(int i = 2; i <= button_image_list.Count; i++)
            {
            button_image_list[i-1].color = lock_color;
            }
        }
        else
        {
            for(int i = 1; i <= button_image_list.Count; i++)
            {
            if(LevelUnlock.level_unlock_state[i] == 0)
            button_image_list[i-1].color = lock_color;
            else
            {
            button_image_list[i-1].color = unlock_color;
            }

        }

    }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
