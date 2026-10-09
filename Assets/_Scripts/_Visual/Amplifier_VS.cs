using System;
using UnityEngine;
[RequireComponent(typeof(Amplifier))]
public class Amplifier_VS : MonoBehaviour
{
    [SerializeField] ParticleSystem idle;
    [SerializeField] ParticleSystem charge_up;
    [SerializeField] ParticleSystem burst;
    private Amplifier amplifier;
    void Awake()
    {
        amplifier = gameObject.GetComponent<Amplifier>();
        amplifier.charge += playChargeParticle;
        amplifier.burst +=playBurstParticle;
    }
    void Start()
    {
        idle.Play();
        charge_up.Stop();
        burst.Stop();
        
       
    }
    void Update()
    {
        
    }
    public void playChargeParticle(bool state)
    {
        if(state && !charge_up.isPlaying)
        {
            charge_up.Play();
        }
        else if (!state &&charge_up.isPlaying)
        {
            charge_up.Stop();
        }
    }
    public void playBurstParticle()
    {
        burst.Play();
    }
}
