using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public GameObject playerKingdom;
    public GameObject[] NPCKingdoms;

    public GameObject gameOverMenu;
    //    public GameObject gameOverCitizensMenu;
    //    public GameObject victoryMenu;

    public GameObject gameOverText;
    public GameObject gameOverCitizensText;
    public GameObject victoryText;

    public GameObject nameInput;

    public float timer;
    public int days;


    bool deleted = false;
    bool start = true;

    int npclen;

    int playerCitizens;

    string status;

    private void Awake()
    {
        timer = 0f;
        days = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerKingdom = GameObject.Find("Kingdom");

        gameOverMenu.SetActive(false);
        //   gameOverCitizensMenu.SetActive(false);
        //   victoryMenu.SetActive(false);
        gameOverText.SetActive(false);
        gameOverCitizensText.SetActive(false);
        victoryText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        timer += 0.016f * Time.deltaTime;
        days = (int)(timer);

        if (start == true)
        {
            start = false;
            NPCKingdoms = FindObjectsOfType<GameObject>();

            int ind = 0;

            for (int i = 0; i < NPCKingdoms.Length; i++)
            {
                if (!NPCKingdoms[i].gameObject.name.Contains("NPCKingdom"))
                {
                    NPCKingdoms[i] = null;
                }
                else
                {
                    NPCKingdoms[ind] = NPCKingdoms[i];
                    ind++;
                }
            }
            System.Array.Resize<GameObject>(ref NPCKingdoms, ind);
            npclen = NPCKingdoms.Length;
        }
        if (playerKingdom == null)
        {
            if (playerCitizens <= 0)
            {
                status = "(Defeat)";
                gameOverMenu.SetActive(true);
                gameOverCitizensText.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                status = "(Defeat)";
                gameOverMenu.SetActive(true);
                gameOverText.SetActive(true);
                Time.timeScale = 0f;
            }

            if (deleted == false)
            {
                deleted = true;
                string path = Application.persistentDataPath + "save.tnkngdms";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        } else 
        {
            playerCitizens = playerKingdom.GetComponent<KingdomController>().citizens +
           playerKingdom.GetComponent<KingdomController>().attackKnights +
           playerKingdom.GetComponent<KingdomController>().defenceKnights;
        }

        npclen = 0;
        for (int i = 0; i < NPCKingdoms.Length; i++)
        {
            if (NPCKingdoms[i] != null)
                npclen++;
        }

        if (npclen == 0)
        {
            status = "(Victory)";
            gameOverMenu.SetActive(true);
            victoryText.SetActive(true);
            Time.timeScale = 0f;
        }

        if (gameOverMenu.activeSelf)
        {
            if (GameObject.Find(nameInput.name + "/Text").GetComponent<Text>().text == null ||
                GameObject.Find(nameInput.name + "/Text").GetComponent<Text>().text == "")
            {
                GameObject.Find(this.name + "/Canvas/MainMenu").GetComponent<Button>().interactable = false;
            } else if (GameObject.Find(nameInput.name + "/Text").GetComponent<Text>().text != null &&
                GameObject.Find(nameInput.name + "/Text").GetComponent<Text>().text != "")
            {
                GameObject.Find(this.name + "/Canvas/MainMenu").GetComponent<Button>().interactable = true;
            }
        }
    }

    public void MainMenu()
    {
        string nm = GameObject.Find(nameInput.name + "/Text").GetComponent<Text>().text;
        GameObject.Find("SaveManager").GetComponent<SaveSystem>().SaveHighScore(nm, days, status);
        SceneManager.LoadScene("Main Menu");
    }
}
