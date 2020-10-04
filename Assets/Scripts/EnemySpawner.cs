using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Vector2Int> enemyData;
    public List<Vector3> difficultyModifiers;
    public int enemies;
    public int score;

    public int numObjects = 6;
    public int waves = 2;
    public float waveDealy = 1.5f;
    public GameObject prefab;
    public float radius;

    public Cannon cannon;

    Vector3 center = new Vector3(0, 0.85f, 5);

    public float spawnTime;

    public bool pause;

    void Start()
    {
        pause = true;
        spawnTime = 0;
        enemies = 0;
    }

    private void Update()
    {
        if (!pause)
        {
            if (enemies <= score)
            {
                cannon.LevelUp();

                waves = getWaves(cannon.level);
                numObjects = getEnemies(cannon.level);
                enemies = numObjects;
                score = 0;

                StartCoroutine(SpawnEnemies(numObjects));
            }
            else if (spawnTime != 0f && Time.time > spawnTime + 15f)
            {
                enemies = 0;
                score = 0;
                spawnTime = 0f;
            }
        }

    }

    private int getWaves(int level)
    {
        int maxEnemies = 4 + level / 4;
        int enemies = getEnemies(level);
        if(enemies < maxEnemies)
        {
            return 1;
        }
        else return getEnemies(level) / maxEnemies;
    }

    private int getEnemies(int level)
    {

        return 3 + (level / 3) * 3 + ((level / 5) * 2);

    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    IEnumerator SpawnEnemies(int amount)
    {
        int maxEnemies = 4 + cannon.level / 4;
        int spawned = 0;
        while(spawned < amount)
        {
            yield return new WaitForSeconds(waveDealy);
            for (int j = 0; j < maxEnemies; j++)
            {
                Vector3 pos = RandomCircle(center, radius);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                EnemyShip es = Instantiate(prefab, pos, rot).GetComponent<EnemyShip>();
                es.es = this;
                es.HP = 1;
                es.speed += (0.25f * cannon.level) / 2;

                spawned += 1;
                if (spawned >= amount)
                {
                    break;
                }
            }
        }

        spawnTime = Time.time;

    }
}
