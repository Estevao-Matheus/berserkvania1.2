using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penaatack : MonoBehaviour
{
    private Transform player;
    public int damage = 5;
    public int speed;
    private Rigidbody2D rd;
    Player_Controller Target;
    Vector2 movedirection;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        Target = GameObject.FindObjectOfType<Player_Controller>();
        movedirection = (Target.transform.position - transform.position).normalized * speed;
        rd.velocity = new Vector2(movedirection.x, movedirection.y);
    }

    // Update is called once per frame
    void Update()
    {
      

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Controller player = collision.gameObject.GetComponent<Player_Controller>();
            player.takeDamage(damage);
            Destroy(gameObject);
        }

    }
}
