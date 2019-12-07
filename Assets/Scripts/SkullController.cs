using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour {

    public Transform originPoint;
    private Vector2 dir = new Vector2(-1,0);
    private Vector2 targetPosition = new Vector2(-1, 0);
    public float distance = 0.2f;
    BoxCollider2D colisor;
    Animator animator;
    Rigidbody2D rb;
    private int hp;
    private Transform target;
    public float speed = 0.5f;
    private bool facingright = false;
    public float dano = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        colisor = GetComponent<BoxCollider2D>();
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
                //Esta comentado pois para pegar o component da vida do player, só funciona na função de trigger enter, e não do Collision;
                //colisor.isTrigger = false;
            }
        }
        else
        {
            animator.SetBool("walking", true);
            animator.SetBool("attacking", false);
            targetPosition.x = target.position.x;
            targetPosition.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
    private void OnCollisionEnter2D(Collision2D col)
    {

        Debug.Log("Pegou a colisao");
        HealthSystem playerLife = col.gameObject.GetComponent<HealthSystem>();
        if (playerLife != null)
        {
            playerLife.Dano(dano);
        }

    }

  
    public void Flip()
    {
        facingright = !facingright;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        dir *= -1;
    }
}