using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 100; 

    [SerializeField]
    private Slider healthSlider;
    
    [SerializeField]
    private GameObject UIWin;
    [SerializeField]
    private GameObject UIrestart;
    [SerializeField]
    private GameObject UILose;

    public int currentHealth;           
    private Pong pongBall;

    // Reference to the Animator component.
    private Animator anim;                                    
    // Reference to the AudioSource component.          
    private AudioSource playerAudio;                                    
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        currentHealth = startingHealth;

        EventsContainer.PongLandedToTheCup += OnPongLandedToTheCup;
    }

    private void ProcessLose()
    { 
        UILose.SetActive(true);
        UIrestart.SetActive(true);
    }

    public void OnPongLandedToTheCup(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthSlider.value = currentHealth;
        

        if (currentHealth <= 50)
        {
            DrunkIdle();
        }

        if (currentHealth <= 0)
        {
            ProcessDeath();
        }
    }

    private void Drink()
    { 
        anim.SetBool("Drink", true);
        anim.SetBool("Idle", false);
    }

    private void DrunkIdle()
    { 
        anim.SetBool("Idle", false);
        anim.SetBool("DrunkIdle", true);
    }

    void ProcessDeath()
    {
        anim.SetBool("DrunkIdle", false);
        anim.SetBool("Drink", false);
        int randomNumber = Random.Range(1, 3);
        anim.SetTrigger("Die" + " "+ randomNumber);

        UIWin.SetActive(true);
    }

    public void OnDrinkAnimationEnd()
    {
        anim.SetBool("Drink", false);
        anim.SetBool("Idle", true);
    }
}

