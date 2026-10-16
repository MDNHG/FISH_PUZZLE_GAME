using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour,IPushable
{
    enum Direction
    {
        RIGHT = -1,
        LEFT = 1,
    }
    enum Input
    {
        UP = 1,
        DOWN = 3,
        RIGHT = 2,
        LEFT = 0,
    }
    [SerializeField] Direction turn_direction; 
    [SerializeField] Input input_direction;

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
    public Vector3 GetTurnDirection()
    {
        return new Vector3(1,1*(int)turn_direction,1);
    }
    public Vector3 GetInputDirection()
    {
        return new Vector3(0,0,-90*(int)input_direction);
    }
}