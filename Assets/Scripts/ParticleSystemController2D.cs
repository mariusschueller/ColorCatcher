using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystemController2D : MonoBehaviour
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    //private float m_Drift = 0.5f;
    private GameObject player;
    private moveScript ms;
    private float timeSinceLastFire = 0;

    public bool[] color;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    private bool fired;

    private AudioSource healthSound;

    //try to create a burst and see if it still works
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ms = player.GetComponent<moveScript>();

        healthSound = GetComponent<AudioSource>();

        fired = false;
        //shootParticles(30);
        /*
        InitializeIfNeeded();

        var em = r_System.emission;
        //em.enabled = true;
        em.rateOverTime = 0;

        em.SetBursts(
            new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0.0f, 20)//change number as needed with the enemy defeated
            });

        r_System.Play();

        timeSinceLastFire = Time.time;*/
    }
    

    public void shootParticles(int amount)
    {
        InitializeIfNeeded();

        var em = m_System.emission;
        //em.enabled = true;
        em.rateOverTime = 0;

        em.SetBursts(
            new ParticleSystem.Burst[]
            {
                new ParticleSystem.Burst(0.0f, amount)//change number as needed with the enemy defeated
            });

        m_System.Play();

        timeSinceLastFire = Time.time;
        fired = true;
    }

    //I'm thinking of waiting a certain amount of time then just move strait to player (no +=)
    private void LateUpdate()
    {
        if (fired)
        {
            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = m_System.GetParticles(m_Particles);

            // Change only the particles that are alive
            for (int i = 0; i < numParticlesAlive; i++)
            {
                Vector3 direction;
                if (Time.time - timeSinceLastFire > 0.5f)
                    direction = player.transform.position - m_Particles[i].position;
                
                else
                    direction = m_Particles[i].velocity / 5.5f;
                //direction.Normalize(); 

                m_Particles[i].velocity = direction * 5;
            }

            // Apply the particle changes to the Particle System
            m_System.SetParticles(m_Particles, numParticlesAlive);
        }
        
    }

    void InitializeIfNeeded()
    {
        //initialize all particle systems here. Might need to do public to get the components
        if (m_System == null)
        {
            m_System = GetComponent<ParticleSystem>();
        }

        m_System.trigger.AddCollider(player.GetComponent<CircleCollider2D>());

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
        {
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
            
        }
            
    }


    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = m_System.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        // iterate through the particles which entered the trigger
        for (int i = 0; i < numEnter; i++)
        {
            if (color[0])
            {
                ms.addRed();
                healthSound.Play();
                Debug.Log("adding to red");
            }

            if (color[1])
            {
                ms.addGreen();
                healthSound.Play();
                Debug.Log("adding to green");
            }

            if (color[2])
            {
                ms.addBlue();
                healthSound.Play();
                Debug.Log("adding to blue");
            }

        }

        m_System.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

}
