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
    public void Flip()
    {
        
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }
}
