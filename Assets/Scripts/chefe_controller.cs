using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chefe_controller : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float spawnrate = 0.5f;
    private float nextspawn;
    private Animator animator;
    public GameObject prefabpena;
    private int hp = 1000;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        latestDirectionChangeTime = 0f;
        animator = GetComponent<Animator>();
        calcuateNewMovementVector();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
        if (Time.time > nextspawn)
        {
            animator.SetTrigger("Attack");
            Instantiate(prefabpena, transform.position,Quaternion.identity);
            nextspawn = Time.time + spawnrate;
        }
    }
    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        calcuateNewMovementVector();
        
    }
    public void TakeDamage(int dano)
    {

        hp -= dano;

        if (hp < 0)
        {
           
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