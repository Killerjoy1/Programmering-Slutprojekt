using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed = 6f;

	Vector3 movement; //Kallar på alla olika delar i Playern
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
			floorMask = LayerMask.GetMask ("Floor");
			anim = GetComponent <Animator> ();
			playerRigidbody = GetComponent <Rigidbody> ();
	}

	void FixedUpdate () //Alla funktioner
	{
			float h = Input.GetAxisRaw ("Horizontal"); //Få gubben att röra sig med a och d
			float v = Input.GetAxisRaw ("Vertical");	// Tvärtom
			Move (h, v);
			Turning();
			Animating(h, v);
	}

	void Move (float h, float v) //Playern går längs marken, Höjd.
	{
			movement.Set (h, 0f, v);
			movement = movement.normalized * speed * Time.deltaTime; //Playern går med samma hastighet även ifall du går åt sidan med w och d samtidigt osv.
			playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);//cameran kollar alltid mot ens mus "hitpoint"

		RaycastHit floorHit;

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))//Raycasten ska endast "hit'a" golvet.
		{
				Vector3 playerToMouse = floorHit.point - transform.position;//Punkten där Raycasten träffar, och playerns position

				playerToMouse.y = 0f;

				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);//Rotation för att göra musens punkt till playerns forward vector
				playerRigidbody.MoveRotation (newRotation);
		}
}
	void Animating(float h, float v) //Animationerna, de ska känna av när man går osv.
	{
			bool walking = h != 0f || v != 0f;
			anim.SetBool ("IsWalking", walking); //kallar på gå animationen
	}

}
