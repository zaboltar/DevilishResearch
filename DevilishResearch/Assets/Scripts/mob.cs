using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob : MonoBehaviour {

    public float speed;
    public float range;
    public CharacterController controller;

    public Transform player;
    public Animation anim;
    public int health;

    public levelSystem playerLevel;

    private fighter opponent;

    public double impactTime = 0.36;
    private bool impacted;
    public int damage;

    public int maxHealth;

    private int stunTime;

  
    // Use this for initialization
    void Start () {

        health = maxHealth;
        opponent = player.GetComponent<fighter>();
	}
	
	// Update is called once per frame
	void Update () {
                
        if (!isDead())
        {
            if (stunTime <= 0)
            {
                if (!inRange())
            {
                Chase();
            }
            else
            {
                anim.CrossFade("attack");
                Attack();

                if (anim["attack"].time > 0.9 * anim["attack"].length)
                {
                    impacted = false;
                }

            }
    
        }
        else
            {
                
            }

        } 
        else
        {
            DieMethod();
        }

   	}

    void Attack()
    { 
        opponent.getHit(damage);
        impacted = true;
    

        if (anim["attack"].time > anim["attack"].length * impactTime && !impacted && anim["attack"].time < 0.9 * anim["attack"].length)
        {
            opponent.getHit(damage);
            impacted = true;
        }
    }


    bool inRange()
    {
        if (Vector3.Distance(transform.position, player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void GetHit(double damage)
    {
        health = health - (int)damage;
        if (health < 0)
        {
            health = 0;
        }
    }


    public void getStun(int seconds)
    {
        CancelInvoke("stunCountDown");
        stunTime = seconds;
        InvokeRepeating("stunCountDown", 0f, 1f);
    }

    void stunCountDown()
    {
        stunTime = stunTime - 1;
        if (stunTime == 0)
        {
            CancelInvoke("stunCountDown");
        }
    }

 
    void Chase ()
    {
        transform.LookAt(player.position);
        controller.SimpleMove(transform.forward * speed );
        anim.CrossFade("run");
    }



    void DieMethod()
    {
        anim.Play("die");
        if (anim["die"].time > anim["die"].length * 0.9)
        {
            playerLevel.exp = playerLevel.exp + 100;
            Destroy(gameObject);
        }
    }

    bool isDead()
    {
        if (health <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void OnMouseOver ()
    {
        player.GetComponent<fighter>().opponent = gameObject;
    }
}
