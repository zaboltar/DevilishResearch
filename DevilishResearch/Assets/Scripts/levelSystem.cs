using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSystem : MonoBehaviour {



    public int level;
    public int exp;
    public fighter player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        LevelUp();

	}

    void LevelUp()
    {
        if (exp >= Mathf.Pow(level, 2) + 100)
        {
            exp = exp - (int)(Mathf.Pow(level, 2) + 100);
            level = level + 1;
            LevelEffect();
        }
    }

    void LevelEffect()
    {
        player.maxHealth = player.maxHealth + 100;
        //player.damage = player.damage + (int)Mathf.Pow(level, 2);
        player.damage = player.damage + 50;
    }
}
