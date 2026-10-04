using System;
using UnityEngine;

public class WarpReciever : MonoBehaviour
{
    [SerializeField] private int cur_e = 0;
    [SerializeField] private int require_e;
    [SerializeField] private WarpGate warp_gate;
    void Start()
    {   
    }
    void Update()
    {
        if(cur_e >= require_e)
        {
            warp_gate.Activate();
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Bullet>() is Bullet bullet)
        {
            cur_e += bullet.GetEValue();
            Destroy(collider.gameObject);
        }
    }
    public void SetRequiredEnergy(int e)
    {
        require_e = e;
    }
}
