using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private int damage;
   
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAnimation (AnimationClip clip)
    {
        anim.Play(clip.name);
    }
    public void SetWeapon (int damageValue)
    {
        damage = damageValue;
    }
  
    public int getDano()
    {
        return this.damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Imp")
        {
            Imp_Controller imp = collision.gameObject.GetComponent<Imp_Controller>();
            imp.TakeDamage(damage + Player_Controller.forca); 
        }
        if (collision.gameObject.tag == "Jack")
        {
            JackObone jack = collision.gameObject.GetComponent<JackObone>();
            jack.TakeDamage(damage + Player_Controller.forca);
        }
    }
}
