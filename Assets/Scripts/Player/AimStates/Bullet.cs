using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float DestroyTime;
    private float timer;

    public int damage;
   
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>DestroyTime)Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Health>().Damage(damage);
        }
    }

}
