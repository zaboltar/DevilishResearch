using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : MonoBehaviour {

    public CharacterController controller;
    public GameObject opponent;

    public Animation anim;

    public int maxHealth;
    public int health;
    public int damage;

    private double impactLength;
    public double impactTime;
    public bool impacted;
    public float range;

    bool started;
    bool ended;

    public float combatEscapeTime;
    public float countDown;

    public bool specialAttack;
    public bool inAction;

    

    // Use this for initialization
    void Start () {
        health = maxHealth;
        impactLength = (anim["attack"].length * impactTime);
    }
	
    

	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Space) && !specialAttack)
            {
                inAction = true;
            }
       
        if(inAction)
            {
                if(AttackFunction(0, 1, KeyCode.Space, null, 0, true))
                {

                } else
                {
                    inAction = false;
                }
            }
            
        
        Die();
    }

    public bool AttackFunction(int stunSeconds, double scaledDamage, KeyCode key, GameObject particleFX, int projectile, bool opponentBased)
    {
      if (opponentBased)
        {
            if (Input.GetKey(key) && inRange())
            {
                anim.Play("attack");
                ClickToMove.attack = true;

                if (opponent != null)
                {
                    transform.LookAt(opponent.transform.position);

                }
            }
        }
        else
        {
            
            if (Input.GetKey(key))
            {
                anim.Play("attack");
                ClickToMove.attack = true;
                transform.LookAt(ClickToMove.cursorPosition);

                
            }
        }

        if (anim["attack"].time > 0.9 * anim["attack"].length)
        {
            ClickToMove.attack = false;
            impacted = false;
            if (specialAttack)
            {
                specialAttack = false;
            }
            return false;
        }

        Impact(stunSeconds, scaledDamage, particleFX, projectile, opponentBased);
        return true;
    }

    
    public void ResetAttackFunction()
    {
        ClickToMove.attack = false;
        impacted = false;
        anim.Stop("attack");

    }

    void Impact(int stunSeconds, double scaledDamage, GameObject particleFX, int projectile, bool opponentBased)
    {
        if (!opponentBased || opponent != null && anim.IsPlaying("attack") && !impacted)
        {
            if (anim["attack"].time > impactLength && anim["attack"].time <  0.9 * anim["attack"].length )
            {

                countDown = combatEscapeTime +2;
                CancelInvoke("combatEscapeCountDown");
                InvokeRepeating("combatEscapeCountDown", 0, 1);

                if (opponentBased)
                {
                    opponent.GetComponent<mob>().GetHit(damage * scaledDamage);
                    opponent.GetComponent<mob>().getStun(stunSeconds);
                }

                

                //Send Spheres
                Quaternion rot = transform.rotation;
                rot.x = 0f;
                rot.z = 0f;

                if (projectile > 0)
                {
                    //shoot
                    GameObject bolt = (GameObject) Instantiate(Resources.Load("Projectile"), new Vector3 (transform.position.x, transform.position.y + 1.5f, transform.position.z) , rot);
                    Destroy(bolt, 5f);
                }
                //Play partFX
                if (particleFX != null)
                {
                    Instantiate(particleFX, new Vector3(opponent.transform.position.x, opponent.transform.position.y + 1.5f, opponent.transform.position.z), Quaternion.identity);
                }
                
                impacted = true;
            }
        }
    }

    void combatEscapeCountDown()
    {
        countDown = countDown - 1;
        if (countDown == 0)
        {
            CancelInvoke("combatEscapeCountDown");
        }
    }
  

    public void getHit(int damage)
    {
        health = health - damage;
        if (health < 0)
        {
            health = 0;
        }
    }

  

    bool inRange()
    {
        if (Vector3.Distance(opponent.transform.position, transform.position) <= range)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool isPlayerDead()
    {
        if (health == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void Die()
    {
        if(isPlayerDead() && !ended)
        {
            if (!started)
            {
                ClickToMove.die = true;
                anim.Play("die");
                started = true;
            } 

            if (started && !anim.IsPlaying("die"))
            {
                //death logics

                Debug.Log("dead!");

                health = 100;

                ended = true;
                started = false;
                ClickToMove.die = false;
            }
            
        }
    }

}
