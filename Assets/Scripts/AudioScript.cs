using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioScript audioScript;
    private void Awake()
    {
        if (audioScript == null)
        {
            audioScript = this;
        }
        else if (audioScript != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
