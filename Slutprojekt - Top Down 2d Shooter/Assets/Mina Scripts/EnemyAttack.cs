using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // Tiden mellan varje attack
        public int attackDamage = 10;               // Skada

        //Mina referenser
        Animator anim;                              // Referens till animator
        GameObject player;                          // Referens till spelaren
        PlayerHealth playerHealth;                  // Referens till hp
        EnemyHealth enemyHealth;                    // Referens till fiendens hp
        bool playerInRange;                         // Om spelaren kan bli attackerad inom en radie
        float timer;                                // Tid tills nästa attack


        void Awake ()
        {
            // Sätter upp alla referenser
            player = GameObject.Find ("Player");
            playerHealth = player.GetComponent <PlayerHealth> ();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent <Animator> ();
        }


        void OnTriggerEnter (Collider other)
        {
            // Om spelaren rör sig in i fiendens collider
            if(other.gameObject == player)
            {
                // Spelaren är inom collidern
                playerInRange = true;
            }
        }


        void OnTriggerExit (Collider other)
        {
            // Om spelaren rör sig ut ur fiendens collider
            if(other.gameObject == player)
            {
                // Spelaren är utanför collidern
                playerInRange = false;
            }
        }


        void Update ()
        {
            // Lägg till tid tills update var senast kallad av timern
            timer += Time.deltaTime;

            if(timer >= timeBetweenAttacks && playerInRange  && enemyHealth.currentHealth > 0)
            {
                // ... attack.
                Attack ();
            }

            // Om spelaren har mindre än 0 hp
            if(playerHealth.currentHealth <= 0)
            {
                // Animatorn spelar döds animationen
                anim.SetTrigger ("PlayerDead");
            }
        }


        void Attack ()
        {
            // Starta om timern
            timer = 0f;

            // Om spelaren fortfarande har hp att förlora
            if(playerHealth.currentHealth > 0)
            {
                // Spelaren fortsätter skadas
                playerHealth.TakeDamage (attackDamage);
            }
        }
    }
}
