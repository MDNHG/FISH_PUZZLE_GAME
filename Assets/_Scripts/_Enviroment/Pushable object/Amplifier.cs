using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Amplifier : MonoBehaviour, IPushable
{
    private HashSet<Bullet> charge_bullet;
    public event Action<bool> charge;
    public event Action burst;
    private float center_threshold = 0.1f;
    void Start()
    {
        charge_bullet = new HashSet<Bullet>();
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Bullet>() is Bullet temp && !charge_bullet.Contains(temp))
        {
            charge?.Invoke(true);
            if(Vector2.Distance(temp.gameObject.transform.position, gameObject.transform.position) <= center_threshold)
            {
                temp.IncreaseEValue(1);
                charge_bullet.Add(temp);
                burst?.Invoke();
                charge?.Invoke(false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Bullet>() is Bullet temp && charge_bullet.Contains(temp))
        {
            charge_bullet.Remove(temp);
        }
    }
}
