using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // Struktura kraljestva
    [System.Serializable]
    public struct kingdom
    {
        // Koordinate
        public float x, y, z;
        // Količina posameznih surovin
        public int wheat;
        public int wood;
        public int stone;
        public int coal;
        public int iron;
        public int gold;
        // Število meščanov
        public int citizens;
        // Število obrambnih in napadalnih vitezov
        public int attackKnights;
        public int defenceKnights;
        // Življenske točke
        public float health;
        // Id številka nazadnje postavljene zgradbe
        public int ids;
        // Id številka
        public int id;
        // Odnos z igralčevim kraljestvom
        public int relationship;
    }

    // Struktura zgradbe
    [System.Serializable]
    public struct building
    {
        // Koordinate
        public float x, y, z;
        // Tip
        public int buildingClass;
        // Id številka
        public int id;
        // Ploščice, na katerih je lahko postavljena
        public int placableOn;
        // Surovine, ki jih proizvaja
        public string[] materials;

        // Struktura zahtev za nadgradnjo
        [System.Serializable]
        public struct upgradeRequirement
        {
            public string material;
            public int amount;
        };
        // Tabela zahtev za nadgradnjo
        public upgradeRequirement[] upgradeRequirements;
        // Čas proizvodnje surovin
        public float productionTime;
        // Življenske točke
        public int health;
        // Stopnja
        public int level;
        // Značka
        public string tag;
    }

    // Širina in dolžina igrale plošče
    public int mapXSize, mapYSize;
    // Tabela s podatki o ploščicah mape
    public int[,] mapArray;

    // Spremenljivka igralčevega kraljestva
    public kingdom playerKingdom;
    // Tabela neigralskih kraljestev
    public kingdom[] NPCKingdom;

    // Tabela zgradb
    public building[] buildings;

    // Pretekli "dnevi" igre
    public float days;

    public SaveData (Map map, GameObject pK, GameObject[] NPCK, GameObject[] b, GameObject go)
    {
        mapXSize = map.xSize;
        mapYSize = map.ySize;
        mapArray = map.mapArray;

        playerKingdom.x = pK.transform.position.x;
        playerKingdom.y = pK.transform.position.y;
        playerKingdom.z = pK.transform.position.z;
        playerKingdom.wheat = pK.GetComponent<KingdomController>().wheat;
        playerKingdom.wood = pK.GetComponent<KingdomController>().wood;
        playerKingdom.stone = pK.GetComponent<KingdomController>().stone;
        playerKingdom.coal = pK.GetComponent<KingdomController>().coal;
        playerKingdom.iron = pK.GetComponent<KingdomController>().iron;
        playerKingdom.gold = pK.GetComponent<KingdomController>().gold;
        playerKingdom.citizens = pK.GetComponent<KingdomController>().citizens;
        playerKingdom.attackKnights = pK.GetComponent<KingdomController>().attackKnights;
        playerKingdom.defenceKnights = pK.GetComponent<KingdomController>().defenceKnights;
        playerKingdom.health = pK.GetComponent<KingdomController>().health;
        playerKingdom.ids = pK.GetComponent<KingdomController>().ids;

        NPCKingdom = new kingdom[NPCK.Length];

        for (int i = 0; i < NPCK.Length; i++)
        {
            NPCKingdom[i].x = NPCK[i].transform.position.x;
            NPCKingdom[i].y = NPCK[i].transform.position.y;
            NPCKingdom[i].z = NPCK[i].transform.position.z;
            NPCKingdom[i].wheat = NPCK[i].GetComponent<KingdomController>().wheat;
            NPCKingdom[i].wood = NPCK[i].GetComponent<KingdomController>().wood;
            NPCKingdom[i].stone = NPCK[i].GetComponent<KingdomController>().stone;
            NPCKingdom[i].coal = NPCK[i].GetComponent<KingdomController>().coal;
            NPCKingdom[i].iron = NPCK[i].GetComponent<KingdomController>().iron;
            NPCKingdom[i].gold = NPCK[i].GetComponent<KingdomController>().gold;
            NPCKingdom[i].citizens = NPCK[i].GetComponent<KingdomController>().citizens;
            NPCKingdom[i].attackKnights = NPCK[i].GetComponent<KingdomController>().attackKnights;
            NPCKingdom[i].defenceKnights = NPCK[i].GetComponent<KingdomController>().defenceKnights;
            NPCKingdom[i].health = NPCK[i].GetComponent<KingdomController>().health;
            NPCKingdom[i].ids = NPCK[i].GetComponent<KingdomController>().ids;
            NPCKingdom[i].id = NPCK[i].GetComponent<NPCKingdomController>().id;
            NPCKingdom[i].relationship = NPCK[i].GetComponent<NPCKingdomController>().relationship;
        }

        buildings = new building[b.Length];

        for (int i = 0; i < b.Length; i++)
        {
            buildings[i].x = b[i].transform.position.x;
            buildings[i].y = b[i].transform.position.y;
            buildings[i].z = b[i].transform.position.z;
            buildings[i].id = b[i].GetComponent<BuildingController>().id;
            buildings[i].buildingClass = b[i].GetComponent<BuildingController>().buildingClass;
            buildings[i].placableOn = b[i].GetComponent<BuildingController>().placableOn;
            buildings[i].materials = b[i].GetComponent<BuildingController>().materials;
            if (buildings[i].materials.Length == 0 && buildings[i].buildingClass == -1)
            {
                buildings[i].buildingClass = -5;
            }

            buildings[i].upgradeRequirements = new building.upgradeRequirement[b[i].GetComponent<BuildingController>().upgradeRequirements.Length];
            for (int j = 0; j < b[i].GetComponent<BuildingController>().upgradeRequirements.Length; j++)
            {
                buildings[i].upgradeRequirements[j].material = b[i].GetComponent<BuildingController>().upgradeRequirements[j].material;
                buildings[i].upgradeRequirements[j].amount = b[i].GetComponent<BuildingController>().upgradeRequirements[j].amount;
            }

            buildings[i].productionTime = b[i].GetComponent<BuildingController>().productionTime;
            buildings[i].health = b[i].GetComponent<BuildingController>().health;
            buildings[i].level = b[i].GetComponent<BuildingController>().level;
            buildings[i].tag = b[i].gameObject.tag;
        }

        days = go.GetComponent<GameOver>().timer;
    }

}

[System.Serializable]
public class HighScore
{
    public string playerName;
    public int days;
    public string status;

    public HighScore(string pn, int d, string s)
    {
        playerName = pn;
        days = d;
        status = s; 
    }
}