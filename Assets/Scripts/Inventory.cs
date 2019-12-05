using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public static Inventory inventory;
    public List<Weapon> weapons;
    public List<Consumable_itens> itens;

    // Start is called before the first frame update
    private void Awake()
    {
        if (inventory == null)
        {
            inventory = this;
        }
        else if (inventory != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddWeapon(Weapon arma)
    {
        weapons.Add(arma);
    }
    public void AddIten(Consumable_itens item)
    {
        itens.Add(item);
    }
    public void RemoveItem (Consumable_itens item)
    {
        for(int i=0; i< itens.Count;i++)
        {
            if(itens[i]== item)
            {
                itens.RemoveAt(i);
                break;
            }
        }
    }
}
