using UnityEngine;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;            // Hur mycket hp fienden startar med
        public int currentHealth;                   // Hp fienden har just nu
        public float sinkSpeed = 2.5f;              // Hastigheten fienden sjunker ner i marken efter döden
        public int scoreValue = 10;                 // Hur mycket poäng spelaren får när fienden dör


        Animator anim;                              // Animatorn
        CapsuleCollider capsuleCollider;            // Capsulecollidern fienden har
        bool isDead;                                // Om fienden är död
        bool isSinking;                             // När fienden sjunker genom marken.

        void Awake ()
        {
            // Referenser
            anim = GetComponent <Animator> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();

            // Sätter fiendens hp just nu när den spawnar.
            currentHealth = startingHealth;
        }


        void Update ()
        {
            // När fienden skjunker
            if(isSinking)
            {
                // Iprincip fiendens "skjunk" hastighet
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            // Om spelaren dör
            if(isDead)
                // stänger funktionen av, eller "startar" om
                return;

            // Ta bort hp beroende på hur mycket skada som gjorts från spelaren.
            currentHealth -= amount;

            // Om hp är mindre än 0
            if(currentHealth <= 0)
            {
                // Är fienden död
                Death ();
            }
        }


        void Death ()
        {
            // Fienden är död
            isDead = true;

            // Ändra collider till en trigger så skott kan gå igenom den döda fienden-
            capsuleCollider.isTrigger = true;

            // Berätta för animatorn att fienden är död
            anim.SetTrigger ("Dead");
        }


        public void StartSinking ()
        {
            // Hitta och stäng av navmesh
            GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;

            // Hitta rigidbody och gör den kinematisk
            GetComponent <Rigidbody> ().isKinematic = true;

            // Fiendens ska inte sjunka
            isSinking = true;

            // Öka score beroende på fiendens score value
            ScoreManager.score += scoreValue;

            // Efter 2 sec raderas den döda fienden
            Destroy (gameObject, 2f);
        }
    }
}
