using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    public int HP;
    public GameObject explosion;
    public bool dead;

    public EnemySpawner es;

    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        Vector3 center = new Vector3(0, 1, 0);
        Vector2 direction = new Vector2(
            center.x - transform.position.x,
            center.y - transform.position.y
            );

        transform.up = direction;

        rb.AddForce(transform.up * speed);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > spawnTime + 10f)
        {
            es.score += 1;
            Destroy(gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet b = collision.gameObject.GetComponent<Bullet>();
            HP -= b.dmg;
            b.Hit();
        }
        if(collision.gameObject.CompareTag("Shield"))
        {
            HP = 0;
        }
        if (HP <= 0)
        {
            es.score += 1;
            dead = true;
            Destroy(gameObject, 0.05f);
            Destroy(gameObject.GetComponentInChildren<PolygonCollider2D>());
            rb.AddForce((transform.position - collision.transform.position)*50f);
            Vector3 targetDirection = transform.position - collision.gameObject.transform.position;
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            exp.transform.forward = collision.gameObject.transform.up;
            Destroy(exp, 2.5f);
        }
    }
}
