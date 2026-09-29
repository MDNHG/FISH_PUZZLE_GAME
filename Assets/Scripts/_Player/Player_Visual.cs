using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player_Visual : MonoBehaviour
{
    [SerializeField] Sprite front;
    [SerializeField] Sprite back_left, back_right;
    [SerializeField] Sprite side;

    
    [SerializeField] Animator animator;
    private Player_Input pI;
    private SpriteRenderer sprite;
    private Vector2 last_direction;
    void Start()
    {
         pI = GetComponentInParent<Player_Movement>().pI;
         sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(pI.Player.Move.triggered)
        {
            Vector2 moveDir = pI.Player.Move.ReadValue<Vector2>();
            if(last_direction != moveDir)
            {
                ChangeSprite(moveDir);
                last_direction = moveDir;
            }
        } else if(pI.Player.ChangeDirection.triggered)
        {
            Vector2 shootDir = pI.Player.ChangeDirection.ReadValue<Vector2>();
            if(last_direction != shootDir)
            {
                
                ChangeSprite(shootDir);
                last_direction = shootDir;
            }
        }
    }
    void ChangeSprite(Vector2 direction)
    {
        float x = direction.x;
        float y = direction.y;
        switch(x,y)
        {
            case (1,0):
            sprite.sprite = side;
            gameObject.transform.localScale = new Vector3(1,1,1);
            break;
            case (-1,0):
            sprite.sprite = side;
            gameObject.transform.localScale = new Vector3(-1,1,1);
            break;
            case (0,1):
            sprite.sprite = back_left;
            gameObject.transform.localScale = new Vector3(1,1,1);
            break;
            case (0,-1):
            sprite.sprite = front;
            gameObject.transform.localScale = new Vector3(1,1,1);
            break;
        }
    }
}
