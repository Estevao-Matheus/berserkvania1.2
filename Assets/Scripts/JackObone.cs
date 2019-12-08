using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackObone : MonoBehaviour
{

    private int hp;
    private Transform target;
    public int speed;
    private bool facingright = true;
    private bool morto = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private int attack = 20;
    public GameObject machadoprefab;
    public Transform ataque;
    private float nextspawn;
    private float spawnrate;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        spawnrate = 2f;
        transform.Rotate(0f, 180f, 0f);

    }

    // Update is called once per frame
    void Update()
    {

        if (!morto)
        {

            if (Vector2.Distance(transform.position, target.position) > 2)
            {
                var targetPos = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                animator.SetBool("andando",true);
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
            }else
            {
                animator.SetBool("andando", false);
                if (Time.time > nextspawn)
                {
                    animator.SetTrigger("Attack");
                    Instantiate(machadoprefab, ataque.position, ataque.rotation);
                    nextspawn = Time.time + spawnrate;
                }
            }
        }
    }
    public void Flip()
    {
        facingright = !facingright;
        transform.Rotate(0f, 180f, 0f);

    }
    public void TakeDamage(int dano)
    {

        hp -= dano;

        if (hp < 0)
        {
            morto = true;
            Destroy(gameObject);

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
}
