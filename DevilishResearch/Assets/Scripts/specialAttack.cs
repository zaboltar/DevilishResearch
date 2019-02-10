using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialAttack : MonoBehaviour {

    public double damagePercentage;
    public int stunTime;
    public KeyCode key;
    public fighter player;
    public bool inAction;
    public int projectile;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(key) && !player.specialAttack)
        {
            player.ResetAttackFunction();
            player.specialAttack = true;
            inAction = true;
        }

        if (inAction)
        {
            if (player.AttackFunction(stunTime, damagePercentage, key))
            {

            } else
            {
                inAction = false;
            }
        } 
      
    }
	
}
