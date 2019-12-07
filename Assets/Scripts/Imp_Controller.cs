using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp_Controller : MonoBehaviour
{


    private int hp;
    private Transform target;
    public int speed;
    private bool facingright = true;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (transform.position.x > 0 && !facingright)
            {
                //sr.flipX = false;
                Flip();

            }
            else if (transform.position.x < 0 && facingright)
            {

                //sr.flipX = true;
                Flip();
            }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if()


    }
    public void Flip()
    {
        facingright = !facingright;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }
}
