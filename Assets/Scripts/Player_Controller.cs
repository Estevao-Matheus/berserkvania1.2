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
    private static Weapon ArmaEquipada;
    private Animator animator;
    private Attack attack;
    public Consumable_itens item;
    private static Armor armaduraEquipada;
    private float nextattack;
    private float fireRate;
    private bool shockwave = true;
    private bool fireball = true;
    public  GameObject wavePrefab;
    public  Transform ataquek;
    public GameObject FireballPrefab;
    public  Transform fireballpos;

    public static int forca=30;
    public int defesa;
    public int vidaMax;
    public int manaMax;
    public static int vidaAtual= 100;
    public static int manaAtual= 100;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
        extraJumps = extraJumpsvalue;
        sr = GetComponent<SpriteRenderer>();
        attack = GetComponentInChildren<Attack>();
        animator = GetComponent<Animator>();
        fireRate = 0.377f;
        
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
            if(Input.GetKeyDown(KeyCode.Z) && ArmaEquipada != null && Time.time > nextattack)
            {
                animator.SetTrigger("JumpAttack");
                attack.PlayAnimation(ArmaEquipada.animação);
                nextattack = Time.time + fireRate;
            }

        }
        if(Input.GetKeyDown(KeyCode.Z)&& ArmaEquipada!=null&& Time.time> nextattack)
        {
            
            animator.SetTrigger("Attack");
            attack.PlayAnimation(ArmaEquipada.animação);
            nextattack = Time.time + fireRate;
        }
        if(Input.GetKeyDown(KeyCode.X)&&manaAtual>30&&shockwave && ArmaEquipada != null && Time.time > nextattack)
        {
            animator.SetTrigger("Attack");
            attack.PlayAnimation(ArmaEquipada.animação);
            Instantiate(wavePrefab, ataquek.position, ataquek.rotation);
            manaAtual -= 30;
            nextattack = Time.time + fireRate;
        }
        if (Input.GetKeyDown(KeyCode.C) && manaAtual > 15 && fireball && Time.time > nextattack)
        {
            animator.SetTrigger("Fire");
            Instantiate(FireballPrefab, fireballpos.position, fireballpos.rotation);
            manaAtual -= 15;
            nextattack = Time.time + fireRate;
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
    public void AddArmadura(Armor armadura)
    {
        armaduraEquipada = armadura;
        defesa = armaduraEquipada.defesa;
        
    }

    public void UseIten (Consumable_itens item)
    {
        vidaAtual += item.qtdeCura;
        if(vidaAtual>=vidaMax)
        {
            vidaAtual = vidaMax;
        }
        manaAtual += item.qtdeMana;
        if (manaAtual >= manaMax)
        {
            manaAtual = manaMax;
        }
    }

    public void Flip()
    {
        facingright = !facingright;
        transform.Rotate(0f, 180f, 0f);

    }
    public void takeDamage(int dano)
    {
        vidaAtual -= (dano-defesa);
        animator.SetTrigger("Dano");
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        for (float i = 0; i < 0.2f; i += 0.2f)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
