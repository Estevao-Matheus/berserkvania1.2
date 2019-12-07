using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Armor : ScriptableObject
{
    // Start is called before the first frame update
    public int ItemID;
    public string armaNome;
    public string descricao;
    public int defesa;
    public Sprite image;
    public string message;
}
