﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
public class EnemyMovement : MonoBehaviour {

	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	UnityEngine.AI.NavMeshAgent nav;

	void Awake ()
	{
		  player = GameObject.Find ("Player").transform;
			playerHealth = player.GetComponent <PlayerHealth> ();
			enemyHealth = GetComponent <EnemyHealth> ();
			nav = GetComponent <UnityEngine.AI.NavMeshAgent>();
	}


	void Update ()
		{
			if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
			{
					nav.SetDestination (player.position);
			}
			else
			{
					nav.enabled = false;
			}
		}
	}
}
