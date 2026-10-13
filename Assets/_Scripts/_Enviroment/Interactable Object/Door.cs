using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public static event Action killPlayer;
    int tick_left = 0;
    private SpriteRenderer spt;
    private BoxCollider2D col;
    public Sprite door_open;
    public Sprite door_closed;


    void Start()
    {
        Player_Movement.Move += DecreaseTickOnPlayerMove;
        col = gameObject.GetComponent<BoxCollider2D>();
        spt = gameObject.GetComponent<SpriteRenderer>();        
        Deactivate();
    }

    void Update()
    {
        if(tick_left > 0 && col.enabled == true)
        {
            Activate();
        }
        else if(tick_left == 0 && col.enabled == false)
        {
            Deactivate();
        }
    }
    private void Activate()
    {
        col.enabled = false;
        spt.sprite = door_open;
    }
    private void Deactivate()
    {    
        CheckOverlap();
        col.enabled = true;
        spt.sprite = door_closed;
    }
    
    public void ChangeTick(int amount)
    {
        tick_left += amount;
        Debug.Log(tick_left);
    }
    private void DecreaseTickOnPlayerMove()
    {
        if(tick_left > 0)
        {
        tick_left--;
        Debug.Log(tick_left);
        }
    }
    private void CheckOverlap()
    {
        Vector2 center = col.bounds.center;
        Vector2 size = col.bounds.size;
        Collider2D hit = Physics2D.OverlapBox(center, size, 0f);
        if (hit != null && hit.gameObject.GetComponent<Player_Movement>() != null)
        {
            killPlayer?.Invoke();
        }
    }
    public int GetTickLeft()
    {
        return tick_left;
    }
    
}
