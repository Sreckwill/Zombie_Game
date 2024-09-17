using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private Health playerHealth;

 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
        else
        {
            Debug.LogWarning("Player not found in the scene.");
        }
    }


    void Update()
    {
        if (playerHealth != null && playerHealth.health <= 0)
        {
            Debug.Log("Scene change triggered due to player death.");
        }
    }
}
