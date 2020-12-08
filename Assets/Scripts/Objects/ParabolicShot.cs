using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicShot : MonoBehaviour
{
    // essential components
    private Rigidbody2D rb;
    private Transform shootingPos;
    private SpriteRenderer rend;
    private CircleCollider2D bc;

    public AudioSource throwSound;

    public float speed = 7f;
    public float force = 400f;

    private void Awake()
    {
        // audio plays on awake
        throwSound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        bc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        // After 4 sec, call destroy method
        Invoke(nameof(DestroyBullet), 4f);
        
        // physic forces
        rb.velocity = transform.right * speed;
        rb.AddForce(new Vector2(0f, force));
        
        Physics2D.IgnoreLayerCollision(15, 12);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // on collision with enemies, renderer and boxcollider disables to look like it is gone
        if (collision.gameObject.CompareTag("Enemy"))
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
