using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            HealthManager.health--;
            AudioManager.instance.Play("TakeDamage");

            if (HealthManager.health <= 0){
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }else{
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(7, 9);
        GetComponent<Animator>().SetLayerWeight(1,1);
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetLayerWeight(1,0);
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }
}
