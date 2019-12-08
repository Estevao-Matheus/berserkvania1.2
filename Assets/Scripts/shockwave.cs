using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwave : MonoBehaviour
{
    float speed = 15f;
    private Rigidbody2D rd;
    private int damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rd.velocity = transform.right * speed;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Imp")
        {
            Imp_Controller imp = collision.gameObject.GetComponent<Imp_Controller>();
            imp.TakeDamage(damage + Player_Controller.forca);
        }
       
    }
}
