using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    // components
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Vector2 initialPos;
    private Transform goblinTransform;
    private Animator anim;

    public ParticleSystem dieParticles;

    // movement parameters
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float wanderLimit = 5f;
    private int randomStart;
    private bool facingRight = true;
    public float detectionDistance = 10f;

    // shooting parameters
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;


    // Finite State Machine declaration
    private enum State { wander, attack, death };
    private State state;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        goblinTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        initialPos = new Vector2(goblinTransform.position.x, goblinTransform.position.y);

        // set a random start for goblins, this way they dont look like twins
        if (randomStart >= 5)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // while goblin is alive
        if (!isDead)
        {
            // check if player is detected
            if (IsPlayerDetected()) // L177
            {
                Attack(); // L117
            }
            else
            {
                Wander(); // L84
            }
            
        }
        else
        {
            Invoke(nameof(Die), 0.5f);
            dieParticles.Play();
            bc.enabled = false;
        }

        StateSwitch(); // L177
    }

    // wandering attitude
    private void Wander()
    {
        rb.velocity = new Vector2(speed, 0);

        // if is going right, euler angle is 0
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                               0f,
                                               transform.eulerAngles.z);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            // if it gets to the limit space, rotate
            if (rb.transform.position.x - initialPos.x >= wanderLimit)
            {
                facingRight = false;
            }
        }
        // if is going right, euler angle is 180
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                               180f,
                                               transform.eulerAngles.z);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            // if it gets to the limit space, rotate
            if (rb.transform.position.x - initialPos.x <= -wanderLimit)
            {
                facingRight = true;
            }
        }
    }

    //only looks at the direction of player
    private void Attack()
    {
        // if player is on left of goblin
        if ((player.transform.position.x - transform.position.x) < 0)
        {
            // if the goblin is facing right, rotate to 180
            if (transform.rotation.y == 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                    180f,
                                                    transform.eulerAngles.z);
            }
        }
        // if the player is on right of goblin
        else
        {
            // if the gaboling is facing left, rotate to 0
            if (transform.rotation.y == -1)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                                                    0f,
                                                    transform.eulerAngles.z);
            }
        }
    }

    // collisions method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the enmy is walking and hits a wall or antoher enemy, rotate
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            if (facingRight)
            {
                facingRight = false;
            }
            else
            {
                facingRight = true;
            }
        }

        // if it gets hit by a player bullet, it dies
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            isDead = true;
        }
    }

    // detecting the player method
    private bool IsPlayerDetected()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= detectionDistance)
        {
            return true;
        }
        return false;
    }

    // FSM changing parameters
    private void StateSwitch()
    {
        if (isDead == true)
        {
            state = State.death;
            anim.SetInteger("State", 2);
        }
        else
        { 
            if (!IsPlayerDetected())
            {
                state = State.wander;
                anim.SetInteger("State", 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
                state = State.attack;
                anim.SetInteger("State", 1);
            }
            
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
