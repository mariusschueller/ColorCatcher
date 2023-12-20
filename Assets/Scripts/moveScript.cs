using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveScript : MonoBehaviour
{
    private Rigidbody2D rb;
    bool canBoost;
    public Transform weapon;

    private float dashSpeed = 5f;
    private float dashDuration = 0.15f;

    private float rHealth;
    private float gHealth;
    private float bHealth;

    private bool noR;
    private bool noG;
    private bool noB;

    public Image rHealthBar;
    public Image gHealthBar;
    public Image bHealthBar;

    //Deacta
    public GameObject rHealthBarOutline;
    public GameObject gHealthBarOutline;
    public GameObject bHealthBarOutline;

    private bool winR;
    private bool winG;
    private bool winB;

    private SpriteRenderer sr;

    public bool tutorial = false;

    public AudioSource hitSound;
    public AudioSource dashSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canBoost = true;

        rHealth = 0;
        gHealth = 0;
        bHealth = 0;

        rHealthBar.fillAmount = rHealth/250;
        gHealthBar.fillAmount = gHealth/250;
        bHealthBar.fillAmount = bHealth/250;

        winR = false;
        winG = false;
        winB = false;

        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canBoost)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        dashSound.Play();
        canBoost = false;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = weapon.position;
        Vector3 direction = (targetPosition - startPosition).normalized;

        // Calculate the velocity needed to reach the target position in dashDuration seconds
        Vector2 velocity = direction * (Vector2.Distance(targetPosition, startPosition) / dashDuration);

        rb.velocity = velocity * dashSpeed;
      
        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;

        canBoost = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("enemy"))
        {
            getHit(10);
        }
    }

    // could replace when collide with a red particle 
    public void addRed()
    {
        if (!winR)
        {
            rHealthBarOutline.SetActive(true);

            rHealth += 1;
            rHealthBar.fillAmount = rHealth / 250;

            sr.color = new Color32((byte)rHealth, (byte)gHealth, (byte)bHealth, 255);

            noR = false;
        }
        

        winR = rHealth >= 250;

        checkWin();
    }

    public void addGreen()
    {
        if (!winG)
        {
            gHealthBarOutline.SetActive(true);

            gHealth += 1;
            gHealthBar.fillAmount = gHealth / 250;

            sr.color = new Color32((byte)rHealth, (byte)gHealth, (byte)bHealth, 255);

            noG = false;
        }
        

        winG = gHealth >= 250;

        checkWin();
    }

    public void addBlue()
    {
        if (!winB)
        {
            bHealthBarOutline.SetActive(true);

            bHealth += 1;
            bHealthBar.fillAmount = bHealth / 250;

            sr.color = new Color32((byte)rHealth, (byte)gHealth, (byte)bHealth, 255);

            noB = false;
        }
        

        winB = bHealth >= 250;

        checkWin();
    }

    public void bulletHit()
    {
        Debug.Log("bullet hit");

        getHit(5);
    }

    private void getHit(int amount)
    {
        hitSound.Play();
        if (rHealth <= 0 && !noR)
        {
            Debug.Log("red gone");
            noR = true;
            //fade the red health bar
            rHealthBarOutline.SetActive(false);
        }

        else if (gHealth <= 0 && !noG)
        {
            Debug.Log("green gone");
            noG = true;
            //fade the green health bar
            gHealthBarOutline.SetActive(false);
        }

        else if (bHealth <= 0 && !noB)
        {
            Debug.Log("blue gone");
            noB = true;
            //fade the blue health bar
            bHealthBarOutline.SetActive(false);
        }

        else if (rHealth <= 0 && gHealth <= 0 && bHealth <= 0 && noR && noG && noB)
        {
            if (!tutorial)
                SceneManager.LoadScene("Lose");
            //Debug.Log("YOU LOSE");
        }



        if (rHealth > 0)
        {
            rHealth -= amount;

            if (rHealth < 0)
                rHealth = 0;

            rHealthBar.fillAmount = rHealth / 250;
        }

        if (gHealth > 0)
        {
            gHealth -= amount;

            if (gHealth < 0)
                gHealth = 0;

            gHealthBar.fillAmount = gHealth / 250;
        }

        if (bHealth > 0)
        {
            bHealth -= amount;

            if (bHealth < 0)
                bHealth = 0;

            bHealthBar.fillAmount = bHealth / 250;
        }


        sr.color = new Color32((byte)rHealth, (byte)gHealth, (byte)bHealth, 255);

        winR = winG = winB = false;
    }

    private void checkWin()
    {
        if (winR && winG && winB && !tutorial)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name + " Win");
        }
    }
}
