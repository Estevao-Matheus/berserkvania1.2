using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    // Start is called before the first frame update
     

        public int itemID;
        public string weaponName;
        public string descrição;
        public int dano;
        public Sprite image;
        public AnimationClip animação;
        public string mensagem;



    }
