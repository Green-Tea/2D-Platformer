using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whisp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            AudioManager.instance.Play("PickupItem");
            PlayerManager.numWhisp++;
            Destroy(gameObject);
        }
    }
}
