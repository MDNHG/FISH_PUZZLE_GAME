using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    int tick_left = 0;
    private SpriteRenderer spt;
    private BoxCollider2D col;
    Color default_cl = Color.white;


    void Start()
    {
        Player_Movement.Move += DecreaseTickOnPlayerMove;
        col = gameObject.GetComponent<BoxCollider2D>();
        spt = gameObject.GetComponent<SpriteRenderer>();        
        default_cl = spt.color;
    }

    void Update()
    {
        if(tick_left > 0 && col.enabled == true)
        {
            Activate();
        }
        else if(tick_left == 0)
        {
            Deactivate();
        }
    }
    private void Activate()
    {
        col.enabled = false;
        default_cl.a = 0.5f;
        spt.color = default_cl;
    }
    private void Deactivate()
    {
        col.enabled = true;
        default_cl.a = 1;
        spt.color = default_cl;
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
}
