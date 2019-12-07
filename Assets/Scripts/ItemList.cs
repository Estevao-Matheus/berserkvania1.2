using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemList : MonoBehaviour
{
    public Consumable_itens itemConsumivel;
    public Text text;
    public Image image;
    public Weapon arma;
    public Armor armadura;
    // Start is called before the first frame update

    public void setUpItem (Consumable_itens item)
    {
        itemConsumivel = item;
        image.sprite = itemConsumivel.imagem;
        text.text = itemConsumivel.descrição;
    }

   public void setUpWeapon(Weapon arma1)
    {
        arma = arma1;
        image.sprite = arma.image;
        text.text = arma.descrição;
    }
    public void setUpArmor(Armor armadura1)
    {
        armadura = armadura1;
        image.sprite = armadura.image;
        text.text = armadura.descricao;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
