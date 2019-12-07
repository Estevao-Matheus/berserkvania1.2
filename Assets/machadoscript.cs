using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machadoscript : MonoBehaviour
{
    float speed = 5f;
    private Rigidbody2D rd;
    private int damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rd.velocity = transform.right * speed;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Controller player = collision.gameObject.GetComponent<Player_Controller>();
            player.takeDamage(damage);
        }

    }
}
