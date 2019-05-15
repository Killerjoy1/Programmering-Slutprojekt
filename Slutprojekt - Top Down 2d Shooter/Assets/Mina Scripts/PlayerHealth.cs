using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;                                 // Referens för spelarens hp ui slide

        Animator anim;
        PlayerMovement playerMovement;
        PlayerShooting playerShooting;
        bool isDead;
        bool damaged;


        void Awake ()
        {

            anim = GetComponent <Animator> ();
            playerMovement = GetComponent <PlayerMovement> ();
            playerShooting = GetComponentInChildren <PlayerShooting> ();


            currentHealth = startingHealth;
        }

        public void TakeDamage (int amount)
        {

            currentHealth -= amount;

            // Ändrar hp slidern beroende på hur mycket skada man tagit
            healthSlider.value = currentHealth;

            // Om spelaren har förlorat allt hp
            if(currentHealth <= 0 && !isDead)
            {
                // Så dör man
                Death ();
            }
        }


        void Death ()
        {

            isDead = true;

            // Stänger av effekter från ens vapen ifall det fortfarande är igång när man dör.
            playerShooting.DisableEffects ();


            anim.SetTrigger ("Die");

            // Stänger av rörelse och skjuta scripten
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }


        public void RestartLevel ()
        {
            // Starta om nivån
            SceneManager.LoadScene (0);
        }
    }
}
