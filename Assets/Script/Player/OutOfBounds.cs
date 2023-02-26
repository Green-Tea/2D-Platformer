using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public float minY = -10;
    public float maxY = 10f;
    public Transform playerTransform;
    public Vector3 resetPosition;

    private void LateUpdate()
    {
        if (playerTransform.position.y < minY || playerTransform.position.y > maxY)
        {
            HealthManager.health--;
            playerTransform.position = resetPosition;

            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
        }
    }
}
