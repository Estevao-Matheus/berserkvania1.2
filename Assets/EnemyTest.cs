using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float dano = 25.0f;
    BoxCollider2D colisor;

    private void Start()
    {
        colisor = GetComponent<BoxCollider2D>();
    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.2f);
        colisor.isTrigger = true;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Pegou a colisao");
        colisor.isTrigger = false;
        HealthSystem playerLife = col.GetComponent<HealthSystem>();
        if (playerLife != null)
        {
            playerLife.Dano(dano);
        }
        StartCoroutine(ExampleCoroutine());
        
    }
}
