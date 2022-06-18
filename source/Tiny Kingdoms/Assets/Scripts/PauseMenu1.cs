using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu1 : MonoBehaviour
{

    public bool pause;
    public GameObject menu;

    GameObject saveManager;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pause = false;

        menu.SetActive(false);

        saveManager = GameObject.FindGameObjectWithTag("SaveManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pause = false;
            menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pause = true;
            menu.SetActive(true);
        }
    }

    public void Exit()
    {
        saveManager.GetComponent<SaveSystem>().SaveGame();
        SceneManager.LoadScene("Main Menu");
    }
}
