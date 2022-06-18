using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem : MonoBehaviour
{

    [System.Serializable]
    public struct upgradeRequirement
    {
        public string material;
        public int amount;
    };

    public Map map;
    public GameObject playerKingdom;
    public GameObject[] NPCKingdom;
    public GameObject[] buildings;
    public GameObject NPCManager;
    public GameObject gameOver;

    bool start = true;

    public void SaveGame()
    {
        // Funkcija FindData() poišče vse objekte, potrebne za shranjevanje
        FindData();

        // Ustvari se nova spremenljivka tipa BinaryFormatter
        BinaryFormatter formatter = new BinaryFormatter();

        // Določi se pot do datoteke, kamor se shranjujejo podatki
        string path = Application.persistentDataPath + "save.tnkngdms";

        /* Ustvari se nova spremenljivka tipa FileStream. Argumenta konstruktorja
          sta pot do datoteke in način, na katerega sistem odpre datoteko. V
          mojem primeru ustvari novo.
         */
        FileStream stream = new FileStream(path, FileMode.Create);

        /* Ustvari se nov objekt podatkov, ki sem ga prej opisal. Argumenti
           so objekti, ki sem jih poiskal s funkcijo FindData().
         */
        SaveData saveData = new SaveData(map, playerKingdom, NPCKingdom, buildings, gameOver);

        // Podatki se zapišejo v ciljno datoteko
        formatter.Serialize(stream, saveData);

        // Tok podatkov se zapre
        stream.Close();
    }

    public SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "save.tnkngdms";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return saveData;
        } else
        {
            return null;
        }
    }

    void Update()
    {
        if (start == true)
        {
            start = false;
            LoadData();
        }
    }

    void FindData()
    {
        gameOver = GameObject.Find("GameOver");

        NPCManager = GameObject.Find("NPCManager");
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();

        int ind = 0;

        playerKingdom = GameObject.FindGameObjectWithTag("0");

        // Poišče vse objekte tipa GameObjects
        NPCKingdom = FindObjectsOfType<GameObject>();
        for (int i = 0; i < NPCKingdom.Length; i++)
        {
            /* Preveri, če ime objekta vsebuje niz "NPCKingdoms", ki označuje
               neigralska kraljestva */
            if (!NPCKingdom[i].gameObject.name.Contains("NPCKingdom"))
            {
                // Iz tabele izbriše vse objekte, ki niso neigralska kraljestva
                NPCKingdom[i] = null;
            }
            else
            {
                // Neigralska kraljestva pomakne na začetek tabele
                NPCKingdom[ind] = NPCKingdom[i];
                ind++;
            }
        }

        // Prilagodi velikost tabele glede na število preostalih elementov
        System.Array.Resize<GameObject>(ref NPCKingdom, ind);

        // Sortira tabelo glede na imena objektov
        for (int i = 0; i < NPCKingdom.Length - 1; i++)
        {
            for (int j = 0; j < NPCKingdom.Length - i - 1; j++)
            {
                if (String.Compare(NPCKingdom[j].gameObject.name, NPCKingdom[j + 1].gameObject.name) > 0)
                {
                    GameObject tmp = NPCKingdom[j];
                    NPCKingdom[j] = NPCKingdom[j + 1];
                    NPCKingdom[j + 1] = tmp;
                }
            }
        }


        buildings = FindObjectsOfType<GameObject>();

        ind = 0;

        for (int i = 0; i < buildings.Length; i++)
        {
            if (!buildings[i].gameObject.name.Contains("Barracks(Clone)") && !buildings[i].gameObject.name.Contains("Cottage(Clone)")
                && !buildings[i].gameObject.name.Contains("Farm(Clone)") && !buildings[i].gameObject.name.Contains("Mine(Clone)")
                && !buildings[i].gameObject.name.Contains("Dock(Clone)"))
            {
                buildings[i] = null;
            }
            else
            {
                buildings[ind] = buildings[i];
                ind++;
            }
        }
        System.Array.Resize<GameObject>(ref buildings, ind);
    }

    void LoadData()
    {

        FindData();

        if (Global.Instance.newGame == false)
        {

            SaveData saveData = LoadGame();

            map.xSize = saveData.mapXSize;
            map.ySize = saveData.mapYSize;
            map.mapArray = saveData.mapArray;

            map.reloadMap();

            playerKingdom.GetComponent<PlayerKingdomController>().isPlaced = true;
            playerKingdom.transform.position = new Vector3(saveData.playerKingdom.x, saveData.playerKingdom.y, saveData.playerKingdom.z);
            playerKingdom.GetComponent<KingdomController>().wheat = saveData.playerKingdom.wheat;
            playerKingdom.GetComponent<KingdomController>().wood = saveData.playerKingdom.wood;
            playerKingdom.GetComponent<KingdomController>().stone = saveData.playerKingdom.stone;
            playerKingdom.GetComponent<KingdomController>().coal = saveData.playerKingdom.coal;
            playerKingdom.GetComponent<KingdomController>().iron = saveData.playerKingdom.iron;
            playerKingdom.GetComponent<KingdomController>().gold = saveData.playerKingdom.gold;
            playerKingdom.GetComponent<KingdomController>().citizens = saveData.playerKingdom.citizens;
            playerKingdom.GetComponent<KingdomController>().attackKnights = saveData.playerKingdom.attackKnights;
            playerKingdom.GetComponent<KingdomController>().defenceKnights = saveData.playerKingdom.defenceKnights;
            playerKingdom.GetComponent<KingdomController>().health = saveData.playerKingdom.health;
            playerKingdom.GetComponent<KingdomController>().ids = saveData.playerKingdom.ids;
            playerKingdom.GetComponent<PlayerKingdomController>().prevHit = new Vector3(saveData.playerKingdom.x, saveData.playerKingdom.y, saveData.playerKingdom.z);
            playerKingdom.GetComponent<PlayerKingdomController>().targetPos = new Vector3(saveData.playerKingdom.x, saveData.playerKingdom.y, saveData.playerKingdom.z);

            for (int i = 0; i < NPCKingdom.Length; i++)
            {
                NPCKingdom[i].transform.position = new Vector3(saveData.NPCKingdom[i].x, saveData.NPCKingdom[i].y, saveData.NPCKingdom[i].z);
               // Debug.Log(NPCKingdom[i].transform.position.x + " " + NPCKingdom[i].transform.position.z);
                NPCKingdom[i].GetComponent<KingdomController>().wheat = saveData.NPCKingdom[i].wheat;
                NPCKingdom[i].GetComponent<KingdomController>().wood = saveData.NPCKingdom[i].wood;
                NPCKingdom[i].GetComponent<KingdomController>().stone = saveData.NPCKingdom[i].stone;
                NPCKingdom[i].GetComponent<KingdomController>().coal = saveData.NPCKingdom[i].coal;
                NPCKingdom[i].GetComponent<KingdomController>().iron = saveData.NPCKingdom[i].iron;
                NPCKingdom[i].GetComponent<KingdomController>().gold = saveData.NPCKingdom[i].gold;
                NPCKingdom[i].GetComponent<KingdomController>().citizens = saveData.NPCKingdom[i].citizens;
                NPCKingdom[i].GetComponent<KingdomController>().attackKnights = saveData.NPCKingdom[i].attackKnights;
                NPCKingdom[i].GetComponent<KingdomController>().defenceKnights = saveData.NPCKingdom[i].defenceKnights;
                NPCKingdom[i].GetComponent<KingdomController>().health = saveData.NPCKingdom[i].health;
                NPCKingdom[i].GetComponent<KingdomController>().ids = saveData.NPCKingdom[i].ids;
                NPCKingdom[i].GetComponent<NPCKingdomController>().id = saveData.NPCKingdom[i].id;
                NPCKingdom[i].GetComponent<NPCKingdomController>().relationship = saveData.NPCKingdom[i].relationship;
            }


            for (int i = 0; i < buildings.Length; i++)
            {
                Destroy(buildings[i]);
            }

            for (int i = 0; i < saveData.buildings.Length; i++)
            {

                if (saveData.buildings[i].tag != "0")
                    NPCManager.GetComponent<NPCManager>().PlaceBuildingLoad(new Vector3(saveData.buildings[i].x, saveData.buildings[i].y, saveData.buildings[i].z),
                        saveData.buildings[i].id, saveData.buildings[i].buildingClass, saveData.buildings[i].tag, saveData.buildings[i].level);
                else
                {
                    playerKingdom.GetComponent<KingdomController>().PlaceBuildingLoad(new Vector3(saveData.buildings[i].x, saveData.buildings[i].y, saveData.buildings[i].z),
                                saveData.buildings[i].id, saveData.buildings[i].buildingClass, saveData.buildings[i].level);
                }
            }

            gameOver.GetComponent<GameOver>().timer = saveData.days;
        }
            
        }

    public void SaveHighScore(string playerName, int days, string status)
    {
        HighScore highScore = new HighScore(playerName, days, status);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "scores.tnkngdms";
        FileStream stream = new FileStream(path, FileMode.Append);

        formatter.Serialize(stream, highScore);
        stream.Close();
    }
}
