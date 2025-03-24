using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action OnDead;

    public bool isDead;

    private void Start()
    {
        health = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0) return;

        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (health <= 0)
        {
            isDead = true;
            OnDead?.Invoke();
        }

        Debug.Log(health);
    }
}
