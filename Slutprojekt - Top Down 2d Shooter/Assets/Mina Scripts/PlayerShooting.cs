using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;                  // Skada från varje kula
        public float timeBetweenBullets = 0.15f;        // Tid mellan skott
        public float range = 100f;                      // Distansen kulan kan fara


        float timer;                                    // Timer som bestämmer när man kan skjuta
        Ray shootRay = new Ray();                       // En ray från vapnets front
        RaycastHit shootHit;                            // En raycast hit för att få information av vad som träffats
        int shootableMask;                              // En layer mask så att raycasten endast träffar sånt som är på shootable layern

        LineRenderer gunLine;                           // Ljus rendern
        Light gunLight;                                 // Ljust komponenten
		public Light faceLight;
        float effectsDisplayTime = 0.2f;                // Hur länge effekterna från kulorna ska visas tills nästa kula.


        void Awake ()
        {
            // Skapar en layer mask för den shootable layer masken
            shootableMask = LayerMask.GetMask ("Shootable");
            gunLine = GetComponent <LineRenderer> ();
            gunLight = GetComponent<Light> ();
			//faceLight = GetComponentInChildren<Light> ();
        }


        void Update ()
        {

            timer += Time.deltaTime;

#if !MOBILE_INPUT
            // Om skjut knappen på musen trycks ner
			if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // Skjuter vapnet
                Shoot ();
            }
#else
            // När det finns input åt vilket håll man vill skjuta
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // skjuter vapnet.
                Shoot();
            }
#endif
            // Om timern för effekterna slutar och man inte längre skjuter med musen
            if(timer >= timeBetweenBullets * effectsDisplayTime)
            {
                // Ska effekterna stängas av
                DisableEffects ();
            }
        }

        public void DisableEffects ()
        {
            // Stäng av effekter
            gunLine.enabled = false;
			faceLight.enabled = false;
            gunLight.enabled = false;
        }

        void Shoot ()
        {
            // Starta om timern
            timer = 0f;

            // Starta igång ljusen (effekterna)
            gunLight.enabled = true;
			faceLight.enabled = true;

            // Ljus rendern ska vara framför vapnet
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // rayen ska starta från framdelen av vapnet
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // starta raycasten ifall något träffar ett objekt som hat shootable
            if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
            {
                // Försök att hitta fienends health script när fienend blir träffad
                EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

                // Om scriptet finns
                if(enemyHealth != null)
                {
                    // så ska fienden ta skada
                    enemyHealth.TakeDamage (damagePerShot, shootHit.point);
                }
                gunLine.SetPosition (1, shootHit.point);
            }

            else
            {
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
