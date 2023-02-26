using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;
    public Button quit;


    // Start is called before the first frame update
    void Start()
    {
        HealthManager.health = 3;
        PlayerManager.numWhisp = 0;
        AudioManager.instance.Play("BGM");
        var root = GetComponent<UIDocument>().rootVisualElement;
        level1 = root.Q<Button>("level1");
        level2 = root.Q<Button>("level2");
        level3 = root.Q<Button>("level3");
        quit = root.Q<Button>("quit");

        level1.clicked += Level1ButtonPressed;
        level2.clicked += Level2ButtonPressed;
        level3.clicked += Level3ButtonPressed;
        quit.clicked += QuitButtonPressed;
    }

    void Level1ButtonPressed()
    {
        AudioManager.instance.Play("PressButton");
        SceneManager.LoadScene("Level 1");
    }
    
    void Level2ButtonPressed()
    {
        AudioManager.instance.Play("PressButton");
        SceneManager.LoadScene("Level 2");
    }

    void Level3ButtonPressed()
    {
        AudioManager.instance.Play("PressButton");
        SceneManager.LoadScene("Level 3");
    }

    void QuitButtonPressed()
    {
        AudioManager.instance.Play("PressButton");
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
