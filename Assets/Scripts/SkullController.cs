using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour {

    public Transform originPoint;
    private Vector2 dir = new Vector2(-1,0);
    private Vector2 targetPos = new Vector2(-1, 0);
    private bool morto = false;
    public float distance = 0.07f;
    Animator animator;
    private SpriteRenderer sprite;
    Rigidbody2D rb;
    private int hp;
    private Transform target;
    public float speed = 0.5f;
    private bool facingright = false;
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
        Debug.DrawRay(originPoint.position, dir * distance);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, dir, distance);
        //Caso queira verificar colisão
        if (hit == true)
        {
            if (hit.collider.CompareTag("Player"))
            {
                animator.SetBool("walking", false);
                animator.SetBool("attacking", true);
            }
        }
        else
        {
            animator.SetBool("walking", true);
            animator.SetBool("attacking", false);
            targetPos.x = target.position.x;
            targetPos.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (transform.position.x > 0 && !facingright)
            {

                Flip();

            }
            else if (transform.position.x < 0 && facingright)
            {

                Flip();
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Player_Controller player = collision.gameObject.GetComponent<Player_Controller>();
            player.takeDamage(attack);
        }


    }

    public void TakeDamage(int dano)
    {

        hp -= dano;

        if (hp < 0)
        {
            animator.SetBool("dying", true);

        }
        else
        {
            StartCoroutine(DamageCoroutine());
        }


    }
    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
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
        dir *= -1;
    }
}