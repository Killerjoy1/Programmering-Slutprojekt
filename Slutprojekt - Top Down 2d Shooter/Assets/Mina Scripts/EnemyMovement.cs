using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
public class EnemyMovement : MonoBehaviour {

	Transform player; //referenser
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	UnityEngine.AI.NavMeshAgent nav; //Ai's navmesh

	void Awake ()
	{
		  player = GameObject.Find ("Player").transform;
			playerHealth = player.GetComponent <PlayerHealth> ();
			enemyHealth = GetComponent <EnemyHealth> ();
			nav = GetComponent <UnityEngine.AI.NavMeshAgent>();
	}


	void Update ()
		{
			if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) //om Spelarens och fiendens hp är mer än 0
			{
					nav.SetDestination (player.position); //så forstätter fienden att gå mot spelaren
			}
			else
			{
					nav.enabled = false; // annars stannar fienden
			}
		}
	}
}
