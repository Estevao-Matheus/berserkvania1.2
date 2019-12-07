using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider barraDeVida;

    public float maxLife = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        barraDeVida.minValue = 0;
        barraDeVida.maxValue = maxLife;
        barraDeVida.value = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        ControleDeVida();
    }

    void ControleDeVida()
    {
        if (barraDeVida.value >= maxLife)
        {
            barraDeVida.value = maxLife;
        }
        else if (barraDeVida.value <= barraDeVida.minValue)
        {
            barraDeVida.value = barraDeVida.minValue;
        }
    }

    public void Dano(float dano)
    {
        barraDeVida.value -= dano;

        if(barraDeVida.value <= barraDeVida.minValue)
        {
            Debug.Log("Voce morreu");
        }
    }
}
