using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile_Script : MonoBehaviour
{
    [SerializeField]
    public float speed = 1;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.tag == "Ally")
        {
            AllyMovement allyController = (AllyMovement) collision.gameObject.GetComponent(typeof(AllyMovement));
            allyController.DestroySelf();
        }
        Destroy(gameObject);
    }
}
