﻿using UnityEngine;
using System.Collections;

public class Deletion : MonoBehaviour {

	// Use this for initialization
    public float bulletLife = 10.0f;
    private GameObject player;
    PlayerHealth p;
    EnemyHealth e;
    
	
	// Update is called once per frame
	void Update () {
        
        Destroy(gameObject, bulletLife);
        
	}

    void OnCollisionEnter(Collider col)
    {
        player = col.gameObject;

		if ((col.gameObject.name == "whitinola") && col.gameObject.name == "Blackinola"))
        {
            p = player.GetComponent<PlayerHealth>();
            p.TakeDamage(20);
        }
        if (col.gameObject.name == "Enemy")
        {
            e = player.GetComponent<EnemyHealth>();
            e.TakeDamage(20);
        }
       
    }
}
