using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireReached : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Campfire")
        {
            AudioManager.instance.Play("StageClear");
            gameObject.SetActive(false);
            PlayerManager.isClear = true;
        }
    }
}
