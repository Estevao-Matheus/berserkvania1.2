using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp_Controller : MonoBehaviour
{


    private int hp;
    private Transform target;
    public int speed;
    private bool facingright = true;
    private bool morto = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private int attack = 20;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!morto)
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag=="Player")
        {
            Player_Controller player = collision.gameObject.GetComponent<Player_Controller>();
            player.takeDamage(attack);
        }


    }

    public void TakeDamage(int dano)
    {

        hp -= dano;

        if(hp<0)
        {
            morto = true;
            animator.SetTrigger("Dead");
           
        }else
        {
            StartCoroutine(DamageCoroutine());
        }


    }
    IEnumerator DamageCoroutine()
    {
        for(float i=0;i<0.2f;i+=0.2f)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void Destroythis()
    {
        Destroy(gameObject);
    }
    
    public void Flip()
    {
        facingright = !facingright;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }
}
