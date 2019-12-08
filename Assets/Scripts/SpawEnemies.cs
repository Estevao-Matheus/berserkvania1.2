using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemies : MonoBehaviour
 
{
    private float nextspawn;
    private float spawnrate;
    public GameObject enemy; // Start is called before the first frame update
    void Start()
    {
        spawnrate = 3f;   
    }

    // Update is called once per frame
    void Update()
    {
        if ( Time.time > nextspawn)
        {
           
            Instantiate(enemy, transform.position, transform.rotation);
          
            nextspawn = Time.time + spawnrate;
        }
    }

   
    
}
