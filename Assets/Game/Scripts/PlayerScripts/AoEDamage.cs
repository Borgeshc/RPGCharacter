using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEDamage : MonoBehaviour
{
    int damage;

    public void SetVariables(int _damage)
    {
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TookDamage(damage);
        }
    }
}
