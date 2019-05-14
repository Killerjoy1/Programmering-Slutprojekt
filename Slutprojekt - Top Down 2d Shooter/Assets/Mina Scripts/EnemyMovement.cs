using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	Transform player;
	UnityEngine.AI.NavMeshAgent nav;

	void Awake ()
	{
		  player = GameObject.Find ("Player").transform;


			nav = GetComponent <UnityEngine.AI.NavMeshAgent>();
	}


	void Update ()
	{
			nav.SetDestination (player.position);
	}
}
