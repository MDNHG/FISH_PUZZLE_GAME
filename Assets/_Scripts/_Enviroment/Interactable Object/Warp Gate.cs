using UnityEngine;
using UnityEngine.InputSystem;

public class WarpGate : MonoBehaviour
{
    private SpriteRenderer spt;
    private BoxCollider2D col;
    Color color = Color.white;


    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        spt = gameObject.GetComponent<SpriteRenderer>();        
        color = spt.color;
        color.a = 0.1f;
        spt.color = color;
        col.enabled = false;
    }

    public void Activate()
    {
        col.enabled = true;
        //Se tach ra script visual rieng
        color.a = 1f;
        spt.color = color;
    }
}
