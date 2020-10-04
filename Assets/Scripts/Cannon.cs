using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour
{

    public float rotateSpeed;
    public GameObject bullet;
    public GameObject shield;
    public float shieldCD;
    public float shieldDuration;
    public float lastShield;
    float lastShot;
    public float shotCD;

    public int level;

    public Rotate rotate;

    public TextMeshProUGUI levelText;

    public int score;

    public Animator tt;

    public Slider s;

    // Start is called before the first frame update
    void Start()
    {
        lastShield = -shieldCD;
        lastShot = -shotCD;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShield < shieldCD)
        {
            s.value = Time.time - lastShield;
        }


        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.up = direction;

        if (Input.GetMouseButton(0))
        {
            if (Time.time > lastShot + shotCD)
            {
                lastShot = Time.time;
                Rigidbody2D bul = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
                bul.AddForce(transform.up * 750f);
            }


        }

        if (Input.GetMouseButtonDown(1))
        {
            Shield();

        }
    }

    public bool Shield()
    {
        if (Time.time > shieldCD + lastShield)
        {
            lastShield = Time.time;
            shield.SetActive(true);
            StartCoroutine(ExecuteAfterTime(2));
        }

        return true;
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        shield.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void LevelUp()
    {
        level += 1;
        rotate.rotateSpeed += 5f;
        levelText.text = (level-1).ToString();
        if(shotCD >= 0.25f && level != 2)
        {
            tt.gameObject.SetActive(true);
            tt.Play("LevelUp", -1, 0f);
            shotCD -= 0.05f;
        }
    }
}
