using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static bool isClear;
    public GameObject gameOverScreen;
    public GameObject clearScreen;
    public static int numWhisp;
    public TextMeshProUGUI whispText;
    Animator myAnimator;
    public Transform playerTransform;
    private Vector3 playerInitialPosition;


    private void Awake()
    {
        isGameOver = false;
        isClear = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        whispText.text = "x " + numWhisp;
        if(isGameOver)
        {
            gameOverScreen.SetActive(true);
        }

        if(isClear)
        {
            clearScreen.SetActive(true);
        }
    }

    public void ReplayLevel()
    {
        AudioManager.instance.Play("PressButton");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        HealthManager.health = 3;
        numWhisp = 0;
    }

    public void NextLevel()
    {
        AudioManager.instance.Play("PressButton");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        AudioManager.instance.Play("PressButton");
        SceneManager.LoadScene("Main Menu");
    }

}
