using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public EnemySpawner es;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) )
        {
            es.pause = false;
            Destroy(gameObject);
        }

    }

}
