using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDrop : MonoBehaviour
{
    public Armor item;

    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = item.image;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.inventory.AddArmadura(item);
        }
        Destroy(gameObject);
    }
}
