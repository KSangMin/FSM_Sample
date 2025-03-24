using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider myCol;

    private int damage;
    private float knockback;

    private List<Collider> alreadyCols = new List<Collider>();

    private void OnEnable()
    {
        alreadyCols.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == myCol) return;
        if(alreadyCols.Contains(other)) return;

        alreadyCols.Add(other);

        if(other.TryGetComponent(out Health health)) health.TakeDamage(damage);
        if (other.TryGetComponent(out ForceReceiver forceReceiver)) 
        { 
            Vector3 dir = (other.transform.position - myCol.transform.position).normalized;
            forceReceiver.AddForce(dir * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
