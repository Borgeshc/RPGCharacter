using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public GameObject explosion;
    public bool explosive;

    Vector3 targetPosition;
    int damage;

    public void SetVariables(float _damage, Vector3 _targetPosition)
    {
        damage = (int)_damage;
        targetPosition = _targetPosition;
    }

    private void Update()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            if(!explosive)
            {
                other.GetComponent<EnemyHealth>().TookDamage(damage);
            }
            else
            {
                GameObject myExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                myExplosion.GetComponent<AoEDamage>().SetVariables(damage);
                Destroy(gameObject);
            }
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Destroy(gameObject);
        }
    }

}
