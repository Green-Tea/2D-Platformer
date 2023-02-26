using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 1.3f;
    private Vector3 spawnPosition;

    void Start()
    {
        AudioManager.instance.Play("Shoot");
        spawnPosition = transform.position;
    }

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, spawnPosition);

        
        if (distance >= maxDistance) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
