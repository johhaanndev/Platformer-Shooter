using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // essential components
    private Rigidbody2D rb;
    private Transform shootingPos;
    private SpriteRenderer rend;
    private BoxCollider2D bc;

    // all particle systems tht interacts with bullet
    public ParticleSystem wallImpactParticlesRight;
    public ParticleSystem wallImpactParticlesLeft;
    public ParticleSystem enemyImpactParticlesRight;
    public ParticleSystem enemyImpactParticlesLeft;

    public AudioSource shotAudio;

    public float speed = 20f;

    private void Awake()
    {
        // audio plays on awake
        shotAudio.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        // bullet always forward the character
        rb.velocity = transform.right * speed;

        // after 1 sec, call destroy method
        Invoke(nameof(DestroyBullet), 1f);

        // ignore enemy collision (avoids killing other goblin)
        Physics2D.IgnoreLayerCollision(13, 14);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // on collision, renderer and boxcollider disables to look like it is gone
        // if hits walls, play wall particles
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (rb.velocity.x > 0)
            {
                wallImpactParticlesRight.Play();
            }
            else
            {
                wallImpactParticlesLeft.Play();
            }
            rend.enabled = false;
            bc.enabled = false;
        }
        // if it hits enemies, play enemy particles
        if (collision.gameObject.CompareTag("Player"))
        {
            rend.enabled = false;
            bc.enabled = false;
        }
    }

    // Destroys the game object
    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

}
