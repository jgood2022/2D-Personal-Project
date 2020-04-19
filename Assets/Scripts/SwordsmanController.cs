using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordsmanController : MonoBehaviour
{
    public float moveForce, maxVelo, restartTime;
    public HealthBar healthBar;
    private Rigidbody2D rg;
    private Animator anim;
    private bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            rg.AddForce(Vector2.up * moveForce);
        }
        if (Input.GetKey("a"))
        {
            rg.AddForce(Vector2.left * moveForce);
        }
        if (Input.GetKey("s"))
        {
            rg.AddForce(Vector2.down * moveForce);
        }
        if (Input.GetKey("d"))
        {
            rg.AddForce(Vector2.right * moveForce);
        }
        //caps a max velocity
        rg.velocity = Vector2.ClampMagnitude(rg.velocity, maxVelo);

        //if (rg.velocity.x > 0 && facingLeft)
        //{
        //    foreach(SpriteRenderer sr in transform)
        //    {
        //        sr.flipX = true;
        //    }
        //}

        //if (rg.velocity.x < 0 && !facingLeft)
        //{
        //    foreach (SpriteRenderer sr in transform)
        //    {
        //        sr.flipX = false;
        //    }
        //}


        anim.SetFloat("Velo", rg.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            healthBar.ChangeHealthBar(healthBar.maxHealth);
            Die();
        }

        if (collision.gameObject.GetComponent<Damage>() != null) {
            healthBar.ChangeHealthBar(collision.gameObject.GetComponent<Damage>().damage);
        }

        if (collision.gameObject.GetComponent<Victory>() != null)
        {
            Debug.Log("Victory");
            SceneManager.LoadScene(collision.gameObject.GetComponent<Victory>().nextLevelName);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Damage>() != null)
        {
            healthBar.ChangeHealthBar(collision.gameObject.GetComponent<Damage>().damage);
        }
    }

    public void Die()
    {
        Debug.Log("Dead");
        anim.SetTrigger("Dead");
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
