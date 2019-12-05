﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Change_scene : MonoBehaviour
{
    [SerializeField] private string newLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(newLevel,LoadSceneMode.Additive);
        }
    }
}
