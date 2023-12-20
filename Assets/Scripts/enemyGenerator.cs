using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    private int maxHealth = 1; // Set the maximum health of this object
    private int currentHealth; // The current health of this object
    private float moveSpeed = 6f; // The speed at which this object moves towards a target
    private int damageOnCollision = 1; // The amount of damage to take when colliding with another object

    public GameObject player; 
    public int level = 1; //probably use game manager to set this, otherwise, use a different prefab for each
    private int[] enemyAttributes = new int[3]; // An array to hold the 3 random numbers generated between 0 and level
    private Rigidbody2D rb; 
    private bool canMove;

    private float ringTotal;
    public GameObject ring1;
    public GameObject ring2;
    public GameObject ring3;

    CircleCollider2D myCollider;

    private Renderer rend;


    public GameObject projectilePrefab; // The prefab for the projectile to be fired
    private float fireRate = 2.0f; // The rate at which to fire projectiles (in seconds)
    private float projectileSpeed = 3.0f; // The speed at which the projectile moves
    private float detectionDistance = 4.0f; // The distance at which to start firing projectiles
    private float timeSinceLastFire = 0.0f;
    private bool canFire = false;


    public ParticleSystemController2D rPar;
    public ParticleSystemController2D gPar;
    public ParticleSystemController2D bPar;

    private bool alive;

    private AudioSource deathSound;

    void Start()
    {
        deathSound = GetComponent<AudioSource>();

        alive = true;
        canMove = true;
        while(enemyAttributes[0] == 0 && enemyAttributes[1] == 0 && enemyAttributes[2] == 0)
        {
            // Generate 3 random numbers between 0 and 5
            for (int i = 0; i < 3; i++)
            {
                enemyAttributes[i] = Random.Range(0, level + 1);
            }
        }
        //Debug.Log(enemyAttributes[0]);
        //Debug.Log(enemyAttributes[1]);
        //Debug.Log(enemyAttributes[2]);

        rend = GetComponent<Renderer>();
        Color32 a = new Color32((byte)(enemyAttributes[0] * 50), (byte)(enemyAttributes[1] * 50), (byte)(enemyAttributes[2] * 50), 255);
        //Debug.Log(a);
        rend.material.color = a;


        // ring sizes
        ringTotal = 1;

        if (enemyAttributes[0] != 0)
        {
            ringTotal = ringTotal + .1f + .05f * enemyAttributes[0];
            ring1.transform.localScale = new Vector2(ringTotal, ringTotal);

        }

        if (enemyAttributes[1] != 0)
        {
            ringTotal = ringTotal + .1f + .05f * enemyAttributes[1];
            ring2.transform.localScale = new Vector2(ringTotal, ringTotal);
        }

        if (enemyAttributes[2] != 0)
        {
            ringTotal = ringTotal + .1f + .05f * enemyAttributes[2];
            ring3.transform.localScale = new Vector2(ringTotal, ringTotal);
        }

        //collider stuff
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.radius = ringTotal * 1.25f;


        //red attribute (speed)
        moveSpeed = enemyAttributes[0] + 2f; //* (5f / 6f) + 2f;
        Debug.Log("move speed: " + moveSpeed);

        //green attribute (health)
        maxHealth = enemyAttributes[1] + 1;

        //blue attribute (fire speed I guess)
        if (enemyAttributes[2] != 0)
        {
            canFire = true;
            projectileSpeed = enemyAttributes[2] * 1.1f + 2;
            fireRate = 2f / enemyAttributes[2] * 1.1f; //level 1 should equal 2
            //fine tune with 1.1f
        }

        // Find the position of the target object
        player = GameObject.FindGameObjectWithTag("Player"); //maybe game manager
        

        // Initialize the current health to the maximum health
        currentHealth = maxHealth;

        // Get the Rigidbody2D component attached to this object
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (alive)
        {
            // Move towards the target object using the Rigidbody2D component
            if (canMove)
            {
                Vector2 direction = player.transform.position - transform.position;
                direction.Normalize();
                rb.velocity = direction * moveSpeed;
            }



            //Projectile Stuff
            if (canFire && Vector2.Distance(player.transform.position, transform.position) < detectionDistance && canMove)
            {
                // Fire a projectile if enough time has passed since the last one
                if (Time.time - timeSinceLastFire > fireRate)
                {
                    timeSinceLastFire = Time.time;
                    Invoke("shoot", 0.5f);
                }
                rb.velocity = Vector2.zero;
            }

            if (Vector2.Distance(player.transform.position, transform.position) > 30)
            {
                Destroy(gameObject);
            }
        }
        

        
    }

    private void shoot()
    {
        if (alive)
        {
            // Create the projectile and set its position and velocity
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 projectileDirection = (player.transform.position - transform.position).normalized;
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.velocity = projectileDirection * projectileSpeed;

            // Reset the time since the last projectile was fired

            Destroy(projectile, 3);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("weapon")){
            currentHealth -= damageOnCollision;
            Debug.Log("Collided with object! Current health: " + currentHealth);
            // Destroy the object if its health is zero or less
            if (currentHealth == 0)
            {
                alive = false;
                // particle System stuff where three particle systems & functions are used

                if (enemyAttributes[0] != 0)
                    rPar.shootParticles(enemyAttributes[0] * 5 + 15);
                if (enemyAttributes[1] != 0)
                    gPar.shootParticles(enemyAttributes[1] * 5 + 15);
                if (enemyAttributes[2] != 0)
                    bPar.shootParticles(enemyAttributes[2] * 5 + 15);

                //deactivate sprite renderer and collider
                myCollider.enabled = false;
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                ring1.SetActive(false);
                ring2.SetActive(false);
                ring3.SetActive(false);

                sr.enabled = false;

                deathSound.Play();

                Destroy(gameObject, 5f);
            }
            
        }
        canMove = false;
        Invoke("startMove", 0.5f);
    }

    void startMove()
    {
        canMove = true;
    }

    //Particle system function
    //ParticleSystem.Burst(time 0,count); create burst with 

}
