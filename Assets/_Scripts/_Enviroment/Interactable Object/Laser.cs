using System;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public static event Action roastPlayer;
    enum Direction
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
    }
    [SerializeField] float laser_duration;
    [SerializeField] Direction SHOOT_DIRECTION;
    [SerializeField] int tick_left;
    [SerializeField] int tick_cap;
    private Vector2 shoot_direction; 
    private bool is_firing;
    void Start()
    {
        shoot_direction = ToVector2(SHOOT_DIRECTION);
        Player_Movement.Move += DecreaseTick;
    }

    // Update is called once per frame
    void Update()
    {
        if(tick_left == 0)
        {
            FireLaser();
            tick_left = tick_cap;
        }
    }
    void DecreaseTick()
    {
        if (is_firing) return;
        tick_left--;
    }
    void FireLaser()
    {
        Debug.Log("Fire");
        StartCoroutine(FireLaserForSecond(laser_duration));
        
    }
    private Vector2 ToVector2(Direction dir)
    {
        return dir switch
        {
            Direction.UP => new Vector2(0, 1),
            Direction.DOWN  => new Vector2(0, -1),
            Direction.LEFT  => new Vector2(-1, 0),
            Direction.RIGHT => new Vector2(1, 0),
            _  =>  Vector2.zero 
        };
    }
    IEnumerator FireLaserForSecond(float second)
    {
        float timer = 0;
        Physics2D.queriesStartInColliders = false;
        is_firing = true;
        while(timer < second)
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, shoot_direction, 100f);
            Debug.DrawRay(gameObject.transform.position, shoot_direction, Color.white, second);
            if(hit.collider.gameObject.GetComponent<Player_Movement>() != null)
            {
                roastPlayer?.Invoke();
            }
            timer+=1*Time.deltaTime;
            yield return null;
        }
        is_firing = false;
        Physics2D.queriesStartInColliders = true;
    }
}
