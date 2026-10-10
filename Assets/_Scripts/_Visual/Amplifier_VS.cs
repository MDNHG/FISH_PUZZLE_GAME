using System;
using UnityEngine;
[RequireComponent(typeof(Amplifier))]
public class Amplifier_VS : MonoBehaviour
{
    [SerializeField] ParticleSystem idle_particle;
    [SerializeField] ParticleSystem charge_up_particle;
    [SerializeField] ParticleSystem burst_particle;
    private Amplifier amplifier;
    void Awake()
    {
        amplifier = gameObject.GetComponent<Amplifier>();
        amplifier.charge += playChargeParticle;
        amplifier.burst +=playburst_particleParticle;
    }
    void Start()
    {
        idle_particle.Play();
        charge_up_particle.Stop();
        burst_particle.Stop();
        
       
    }
    void Update()
    {
        
    }
    public void playChargeParticle(bool state)
    {
        if(state && !charge_up_particle.isPlaying)
        {
            charge_up_particle.Play();
        }
        else if (!state &&charge_up_particle.isPlaying)
        {
            charge_up_particle.Stop();
        }
    }
    public void playburst_particleParticle()
    {
        burst_particle.Play();
    }
}
