using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float maxSpeed;
    private float speed;
    private bool onGround;
    private bool facingright = true;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpsvalue;
    public float jumpForce;
    private SpriteRenderer sr;
    private Weapon ArmaEquipada;
    private Animator animator;
    private Attack attack;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        extraJumps = extraJumpsvalue;
        sr = GetComponent<SpriteRenderer>();
        attack = GetComponentInChildren<Attack>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (onGround == true)
        {
            extraJumps = extraJumpsvalue;
        }
        if (Input.GetKeyDown(KeyCode.Space)&& extraJumps>0)
        {
            rb.velocity=Vector2.up*jumpForce;
            extraJumps--;
            animator.SetTrigger("Jump");
        }
        else if(Input.GetKeyDown(KeyCode.Space)&& extraJumps==0&& onGround ==true)
        {
            rb.velocity = Vector2.up * jumpForce;
           
            animator.SetTrigger("Jump");

        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            
            animator.SetTrigger("Attack");
            attack.PlayAnimation(ArmaEquipada.animação);
        }
        
    }
    private void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, whatIsGround);
        float h = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h));
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
        
        if(h>0 && !facingright)
        {
            //sr.flipX = false;
            Flip();
           
        }
        else if(h<0 && facingright)
        {

            //sr.flipX = true;
            Flip();
        }
    }
    public void AddArma(Weapon arma)
    {
        ArmaEquipada = arma;
        attack.SetWeapon(ArmaEquipada.dano);
    }

    public void Flip()
    {
        facingright = !facingright;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

}
