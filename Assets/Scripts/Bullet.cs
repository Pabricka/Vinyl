using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float decayTimer;
    float spawnTime;
    public int dmg;
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime + decayTimer)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        Destroy(gameObject, 0.05f);
    }
}
