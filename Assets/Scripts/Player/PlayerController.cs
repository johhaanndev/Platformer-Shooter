using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private AudioSource jumpClip;
    [SerializeField] private AudioSource coinClip;

    // particle systems
    [SerializeField] private ParticleSystem coinsParticles;
    [SerializeField] private ParticleSystem shootingParticles;
    [SerializeField] private ParticleSystem deathParticles;
    
    // position where bullets are created
    [SerializeField] private Transform firePoint;

    // collider variables
    private Collider2D coll;
    [SerializeField] private LayerMask ground;

    // movement parameters
    public float speed = 5f;
    public float playerJumpForce = 10f;
    public float killJumpForce = 5f;
    private bool facingRight = true;

    // gameplay parameters
    private bool isWin = false;
    private bool isDead = false;
    private int points = 0;
    private int coinPoints = 50;


    // Finite State Machine declaration
    private enum State { idle, run, jump, fall, death};
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        anim.SetBool("isShooting", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement only when player is alive
        if (state != State.death && !isWin)
        {
            Movement();
        }

        PlayerShoot(); // L155

        StateSwitch();
        anim.SetInteger("state", (int)state);
        
        // got a bug that kept shooting animation up, with this invoke() we assure it is always false and let the animation flow once (L169)
        if (anim.GetBool("isShooting"))
        {
            Invoke(nameof(StopShootAnim), 0.25f); // 0.25 s is the duration of the animation
        }
    }

    // method to create the basic movement by key inputs
    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                0f,
                                                transform.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                180f,
                                                transform.eulerAngles.z);
        }

        // jump - GetKeyDown to just play on the moment the key is pressed
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && coll.IsTouchingLayers(ground)) // added spacebar because some players may feel more comfortable
        {
            Jump(playerJumpForce);
        }
        
    }

    // created jump in a method this way we can use it when enemy is killed
    private void Jump(float jumpForce)
    {
        jumpSound();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jump; // jump state activate when jump button is pressed
    }

    // method that manages the FSM for a well animator use
    private void StateSwitch()
    {
        if (isDead == true)
        {
            state = State.death;
        }
        else
        {
            // when jump, check y velocity for falling animation
            if (state == State.jump)
            {
                if (rb.velocity.y < 0f)
                {
                    state = State.fall;
                }
            }
            // player can fall off of the platform, fall animation activate
            else if (!coll.IsTouchingLayers(ground) && rb.velocity.y < 0f)
            {
                state = State.fall;
            }
            // when hits the floor, switch to idle animation
            else if (state == State.fall)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    state = State.idle;
                }
            }
            // if velocity x is greater than 0.2, switch to run animation
            else if (Mathf.Abs(rb.velocity.x) > 0.5f)
            {
                state = State.run;
            }
            // when stopped, switch to idle animation
            else if (Mathf.Abs(rb.velocity.x) < 0.5f)
            {
                state = State.idle;
            }
        }
    }

    // method for attacking controls
    private void PlayerShoot()
    {
        // both F & E are used for shooting, in ShootingScript are the differences between shooting types
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E))
        {
            if (!anim.GetBool("isShooting"))
            {
                anim.SetBool("isShooting", true);
                Instantiate(shootingParticles, firePoint);
            }
        }
    }

    // method called to avoid animation loop bug
    private void StopShootAnim()
    {
        if (anim.GetBool("isShooting"))
        {
            anim.SetBool("isShooting", false);
        }
    }

    // collision method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // player dies if it gets hit by enemy bullet
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            isDead = true;
            Invoke(nameof(PlayDeathParticles), 1f); // PlayDeathParticles() L242
        }
    }

    // triggers method
    private void OnTriggerEnter2D(Collider2D other)
    {
        // coins
        if (other.gameObject.CompareTag("Collectable"))
        {
            coinClip.Play();
            points += 50;

            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Instantiate(coinsParticles, other.transform);
            StartCoroutine(destroyGO(other.gameObject, 2f));
            
        }
        // player win
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            isWin = true;
        }
    }

    // ********* Getters and Setters for Gameplay manager *********

    public bool getWin()
    {
        return isWin;
    }

    public bool getDead()
    {
        return isDead;
    }

    public int getPoints()
    {
        return points;
    }

    public void SetPoints(int points)
    {
        this.points += points;
    }
    // ************************************************************ end getters & setters


    // method to play jumping effect sound
    private void jumpSound()
    {
        jumpClip.Play();
    }


    // When player dies, menu pops up
    private void Death()
    {
        //gameObject.SetActive(false); if it destroys, spark particles GO loses target and it shows ERROR
        gameplayUI.SetActive(false);
        deathMenu.SetActive(true);

        StartCoroutine(disableGO()); // L264
    }

    private void PlayDeathParticles()
    {
        deathParticles.Play();
    }

    //********************** COROUTUNE **********************

    // plays particles system
    IEnumerator PlayParticles(ParticleSystem particles, float time)
    {
        yield return new WaitForSeconds(time);
        particles.Play();
    }

    // destroys the gameobject
    IEnumerator destroyGO(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj.gameObject);
    }

    // Sets inactive the player game object afetr 1 second, so it can play the death particles
    IEnumerator disableGO()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    //******************************************************* end coroutines
}
