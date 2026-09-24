using UnityEngine;
using UnityEngine.InputSystem;

public class WarpGate : MonoBehaviour
{
    private SpriteRenderer spt;
    private BoxCollider2D col;
    Color default_cl = Color.white;


    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        spt = gameObject.GetComponent<SpriteRenderer>();        
        default_cl =  spt.color ;
        default_cl.a = 0.1f;
        spt.color = default_cl;
        col.enabled = false;
    }

    void Update()
    {
    }
    public void Activate()
    {
        col.enabled = true;
        default_cl.a = 1f;
        spt.color = default_cl;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Player_Movement>() != null)
        {
            ProccessWining();
        }
    }
    void ProccessWining()
    {
        Debug.Log("You win");
    }
}
