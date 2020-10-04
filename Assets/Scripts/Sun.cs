using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{

    public int HP;
    public Slider s;
    public Cannon c;
    public GameObject o;
    public TextMeshProUGUI t;

    bool over = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (over)
        {
            Vector3 c = transform.localScale * 1.1f;
            c.z = 1;
            transform.localScale = c;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!over)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                HP -= 5;
            }
            if (collision.gameObject.CompareTag("Bullet"))
            {
                collision.gameObject.GetComponent<Bullet>().Hit();
                HP -= 1;
            }

            s.value = HP;

            if (HP <= 0)
            {
                over = true;
                StartCoroutine(GameOver());
            }
        }

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        o.SetActive(true);
        t.text = (c.level-1).ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
