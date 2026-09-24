using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour,IPushable
{
    enum Direction
    {
        LEFT = 1,
        RIGHT = -1,
    }
    enum Input
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
    }
    [SerializeField] GameObject input_indicator, output_indicator;
    [SerializeField] Direction turn_direction; 
    [SerializeField] Input input_direction;
    private float turn_angle = 90;
    void Start()
    {
        input_indicator.transform.localPosition = ToVector2(input_direction);
        input_indicator.transform.eulerAngles = new Vector3(0,0,ToAngle(input_direction));
        if(turn_direction == Direction.LEFT)
        {
            output_indicator.transform.localPosition = new Vector3(-2,2,0);
            output_indicator.transform.localEulerAngles = new Vector3(0,0,90);
        }
        else
        {
            
            output_indicator.transform.localPosition = new Vector3(2,2,0);
            output_indicator.transform.localEulerAngles = new Vector3(0,0,-90);
        }
    }
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Bullet>() != null)
            {
                Rigidbody2D rb = collider.attachedRigidbody;
                if(Vector2.Dot(ToVector2(input_direction), rb.linearVelocity.normalized) == -1)
                {
                    rb.gameObject.transform.position = gameObject.transform.position;
                    rb.linearVelocity = Quaternion.Euler(0,0,(int)turn_direction*turn_angle)*rb.linearVelocity;   
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
    private float ToAngle(Input input) // Code tam thoi de handle visual cua input ouput
    {
    return input switch
        {
            Input.UP => 0,
            Input.DOWN  => 180,
            Input.LEFT  => -90,
            Input.RIGHT => 90,
            _  =>  0 
        };
    }
}