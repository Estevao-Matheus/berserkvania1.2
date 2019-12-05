using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Consumable_itens : ScriptableObject
{
    public int itemID;
    public string itemNome;
    public string descrição;
    public Sprite imagem;
    public int qtdeCura;
    public int qtdeMana;
    public string menssagem;
}
