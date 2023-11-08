using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealth : MonoBehaviour
{
    private CharacterMovement movement;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    public bool death;

    public void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    public void SoldierTakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    public void Death()
    {
        foreach (var anim in movement.anims)
        {
            anim.Play("Death");
        }
        movement.enabled = false;
        death = true;
    }
}
