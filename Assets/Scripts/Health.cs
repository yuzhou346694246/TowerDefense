using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health:MonoBehaviour  {

    public float maxHealth;
    public float currentHealth;
    public bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void Demage(float value)
    {
        currentHealth -= value;
        if(currentHealth <= 0f)
        {
            isDead = true;
        }
    }
}
