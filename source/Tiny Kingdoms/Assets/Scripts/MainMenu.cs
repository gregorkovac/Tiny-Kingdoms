using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    HighScore[] highScores;
    int k;
    public GameObject scoresMenu;

    public GameObject titleMenu;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        scoresMenu.SetActive(false);
        string path = Application.persistentDataPath + "save.tnkngdms";
        if (File.Exists(path))
        {
            continueButton.GetComponent<Button>().interactable = true;
        } else
        {
            continueButton.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        Global.Instance.newGame = false;
        SceneManager.LoadScene("Main");
    }

    public void NewGame()
    {
        Global.Instance.newGame = true;
        SceneManager.LoadScene("Main");
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowScores()
    {
        scoresMenu.SetActive(true);
        titleMenu.SetActive(false);
        string displayText = "";

        if (highScores == null) { 
        highScores = new HighScore[20];
        k = 0;
        string path = Application.persistentDataPath + "scores.tnkngdms";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                HighScore hs;

                while (stream.Position != stream.Length)
                {
                    hs = formatter.Deserialize(stream) as HighScore;
                    highScores[k] = new HighScore(hs.playerName, hs.days, hs.status);
                 //   Debug.Log(highScores[k].playerName);
                    k++;
                    //  Debug.Log(highScores.Length);
                }
                stream.Close();
            }
        }
        HighScore tmp;
            
        for (int i = 0; i < k-1; i++)
         {
             for (int j = 0; j < k-i-1; j++)
              {
                if (highScores[j].status == "(Defeat)" && highScores[j+1].status == "(Victory)")
                {
                    tmp = highScores[j];
                    highScores[j] = highScores[j + 1];
                    highScores[j + 1] = tmp;
                }
       
            }
         }

        for (int i = 0; i < k - 1; i++)
        {
            for (int j = 0; j < k - i - 1; j++)
            {
                if ((highScores[j].status == "(Victory)" && highScores[j+1].status == "(Victory)"))
                {
                    if (highScores[j].days > highScores[j + 1].days)
                    {
                        tmp = highScores[j];
                        highScores[j] = highScores[j + 1];
                        highScores[j + 1] = tmp;
                    }
                }
                else if (highScores[j].status == "(Defeat)" && highScores[j+1].status == "(Defeat)")
                {

                    if (highScores[j].days < highScores[j + 1].days)
                    {
                        tmp = highScores[j];
                        highScores[j] = highScores[j + 1];
                        highScores[j + 1] = tmp;
                    }
                }
            }
        }




        for (int i = 0; i < k; i++)
            {
                displayText += highScores[i].playerName + ": " + highScores[i].days + " DAYS "+ highScores[i].status + "\n";
           // Debug.Log(highScores[i].status);
              //  displayText = "hahaaa";
             //   Debug.Log(highScores[i].days);
            }

            GameObject.Find(scoresMenu.name + "/Canvas/ScoreDisplay").GetComponent<Text>().text = displayText;
       

  
    }

    public void HideScores()
    {
        scoresMenu.SetActive(false);
        titleMenu.SetActive(true);
    }

    public void ResetScores()
    {
        string path = Application.persistentDataPath + "scores.tnkngdms";
        GameObject.Find(scoresMenu.name + "/Canvas/ScoreDisplay").GetComponent<Text>().text = "";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        highScores = null;
        k = 0;
    }
}
