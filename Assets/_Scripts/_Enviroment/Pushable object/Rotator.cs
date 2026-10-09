using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour,IPushable
{
    enum Direction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
    }
    enum Input
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
    }
    [SerializeField] Direction turn_direction; 
    [SerializeField] Input input_direction;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Start() {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateSpriteRotation();
    }

    void UpdateSpriteRotation() {
        float rot = 0f;
        if (input_direction == Input.UP) {
            if (turn_direction == Direction.LEFT) {
                rot = 0f;
            }
            else if (turn_direction == Direction.RIGHT) {
                rot = -90f;
            }
        }
        else if (input_direction == Input.DOWN) {
            if (turn_direction == Direction.LEFT) {
                rot = 90f;
            }
            else if (turn_direction == Direction.RIGHT) {
                rot = 180f;
            }
        }
        else if (input_direction == Input.RIGHT) {
            if (turn_direction == Direction.UP) {
                rot = -90f;
            }
            else if (turn_direction == Direction.DOWN) {
                rot = 180f;
            }
        }
        else if (input_direction == Input.LEFT) {
            if (turn_direction == Direction.UP) {
                rot = 0f;
            }
            else if (turn_direction == Direction.DOWN) {
                rot = 90f;
            }
        }
        spriteRenderer.transform.localEulerAngles =
            new Vector3(0, 0, rot);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Bullet>() != null)
            {
                Rigidbody2D rb = collider.attachedRigidbody;
                if(Vector2.Dot(ToVector2(input_direction), rb.linearVelocity.normalized) == -1)
                {
                    rb.gameObject.transform.position = gameObject.transform.position;
                    rb.linearVelocity = Quaternion.Euler(0,0,(int)turn_direction * 90) * rb.linearVelocity;   
                }
                else
                {
                    Destroy(collider.gameObject);
                }
            }
    }
    private Vector2 ToVector2(Input input)
    {
        return input switch
        {
            Input.UP => new Vector2(0, 1),
            Input.DOWN  => new Vector2(0, -1),
            Input.LEFT  => new Vector2(-1, 0),
            Input.RIGHT => new Vector2(1, 0),
            _  =>  Vector2.zero 
        };
    }
}