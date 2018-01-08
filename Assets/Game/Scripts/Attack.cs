using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int amountOfAttacks = 6;
    public int amountOfKicks = 2;

    Animator anim;

    public static bool attacking;
    
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0) && !attacking)
        {
            attacking = true;
            int randomAttack = Random.Range(0, amountOfAttacks);
            anim.SetTrigger("Attack" + (randomAttack + 1));
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !attacking)
        {
            attacking = true;
            int randomKick = Random.Range(0, amountOfKicks);
            anim.SetTrigger("Kick" + (randomKick + 1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !attacking)
        {
            attacking = true;
            anim.SetTrigger("Spell1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !attacking)
        {
            attacking = true;
            anim.SetTrigger("Spell2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !attacking)
        {
            attacking = true;
            anim.SetTrigger("Spell3");
        }
    }
}
