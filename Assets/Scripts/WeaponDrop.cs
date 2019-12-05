using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Weapon arma;

    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = arma.image;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player_Controller player = collision.GetComponent<Player_Controller>();
        Debug.Log("teste2");
        if (player != null)
        {
            player.AddArma(arma);
            Inventory.inventory.AddWeapon(arma);
            Destroy(gameObject);
        }
    }
}