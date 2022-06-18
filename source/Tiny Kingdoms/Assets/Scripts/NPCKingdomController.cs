using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCKingdomController : MonoBehaviour
{

    public int id = -1;

    [HideInInspector] public int wheat = 0;
    [HideInInspector] public int wood = 0;
    [HideInInspector] public int stone = 0;
    [HideInInspector] public int coal = 0;
    [HideInInspector] public int iron = 0;
    [HideInInspector] public int gold = 0;
    [HideInInspector] public int citizens = 20;
    [HideInInspector] public int attackKnights = 0;
    [HideInInspector] public int defenceKnights = 0;

    public int relationship;

    public float refreshTime = 4f;
    float timer;

    public GameObject map;

    public GameObject NPCMngr;

    public GameObject brc = null;

    /*
    public GameObject tradeMenu;
    public GameObject tradeMenuButton;
    */

    public GameObject attackButton;

    public GameObject relDisplay;

    public GameObject playerKingdom;

    public GameObject globalManager;

    bool underAttack;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        relationship = (int)Mathf.Floor(Random.Range(-50, 50));
        timer = refreshTime;
        map = GameObject.Find("Map");
        NPCMngr = GameObject.Find("NPCManager");

        playerKingdom = GameObject.Find("Kingdom");

        this.GetComponent<KingdomController>().menu.SetActive(true);
        /*
        tradeMenu = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu");
        tradeMenu.SetActive(false);
        tradeMenuButton = GameObject.Find(this.name + "/Menu/Canvas/TradeButton");
        tradeMenuButton.GetComponent<Button>().onClick.AddListener(TradeMenu);
        */

        attackButton = GameObject.Find(this.name + "/Menu/Canvas/AttackButton");
        attackButton.GetComponent<Button>().onClick.AddListener(AttackButtonPress);

        relDisplay = GameObject.Find(this.name + "/Menu/Canvas/RelationshipDisplay");
        relDisplay.GetComponent<Text>().text = "" + relationship;

        this.GetComponent<KingdomController>().menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerKingdom.GetComponent<KingdomController>().attackKnights <= 0)
        {
            attackButton.GetComponent<Button>().interactable = false;
        } else
        {
            attackButton.GetComponent<Button>().interactable = true;
        }

        // Vse funkcije se kličejo le v primeru, da kraljestva ne napada igralec
        if (playerKingdom.GetComponent<KingdomController>().attackArmy.GetComponent<AttackArmy>().isAttacking == false)
        {
            timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;

            if (timer <= 0)
            {
                /* Funkcije se kličejo v rednih časovnih intervalih, ki jih
                   določa spremenljivka refreshTime */
                timer = refreshTime;

                for (int i = 0; i < 4; i++)
                {
                    /* Preveri se količina vsakega materiala. Če ga primankuje,
                       kraljestvo postavi ustrezno zgradbo */
                    if (CheckMaterial(i))
                        break;
                }

                // Kraljestvo nahrani meščane
                this.GetComponent<KingdomController>().FeedCitizens();

                // Naknadno se preveri še količina napadalnih in obrambnih vitezov
                CheckMaterial(4);

                /* Treniranje vitezov se izvaja takrat, ko barake obstajajo
                   (objekt je shranjen v spremenljivki brc) */
                if (brc != null)
                {
                    // Če ima kraljestvo premalo obrambnih vitezov, prične z njihovim treningom
                    if (this.GetComponent<KingdomController>().defenceKnights <= 20 && this.GetComponent<KingdomController>().citizens >= 5)
                    {
                        brc.GetComponent<BarracksController>().isTraining = true;
                        brc.GetComponent<BarracksController>().TrainDefence();
                    }

                    // Če ima kraljestvo premalo napadalnih vitezov, prične z njihovim treningom
                    else if (this.GetComponent<KingdomController>().attackKnights <= 20 && this.GetComponent<KingdomController>().citizens >= 5)
                    {
                        brc.GetComponent<BarracksController>().isTraining = true;
                        brc.GetComponent<BarracksController>().TrainAttack();
                    }
                    else
                    {
                        // Če je vseh vitezov dovolj, preneha s treningom
                        brc.GetComponent<BarracksController>().isTraining = false;
                    }
                }

                /* Če je status odnosa z igralcem manjši od 0, če je igralec postavil svoje
                  kraljestvo, če je naključno generirano število glede na odnos enako 1, če
                  vojska že ne napada in če je napadalnih vitezov več od 0,
                  kraljestvo napade igralca
                 */
                if (relationship < 0 && playerKingdom.GetComponent<PlayerKingdomController>().isPlaced == true &&
                   (int)Mathf.Floor(Random.Range(0, 100+relationship)) == 1 &&
                   this.GetComponent<KingdomController>().attackArmy.GetComponent<AttackArmy>().isAttacking == false &&
                   this.GetComponent<KingdomController>().attackKnights > 0)
                {
                    AttackPlayer();
                }
            }
        } else
        {
            // Če je kraljestvo napadeno in je naključno generirano število enako 1, se brani 
            if ((int)Mathf.Floor(Random.Range(0,200)) == 1)
            {
                Defend();
            }
        }
    }

    bool CheckMaterial(int m)
    {
        switch (m)
        {
            case 0:
                if (this.GetComponent<KingdomController>().citizens <= 5)
                {
                    this.GetComponent<KingdomController>().Upgrade();
                   // Debug.Log("KINGDOM");
                    return true;
                }
                break;
            case 1:
                if (this.GetComponent<KingdomController>().wheat <= 10)
                {
                    NPCMngr.GetComponent<NPCManager>().PlaceBuilding(this.GetComponent<KingdomController>().ids,
                        this.GetComponent<Transform>().position.x / 10, this.GetComponent<Transform>().position.z / 10, id,
                        this.gameObject, 1);
                  //  Debug.Log("KID: " + id);
                  //  Debug.Log("FARM");
                    return true;
                }
                break;
            case 2:
                if (this.GetComponent<KingdomController>().stone <= 10 || this.GetComponent<KingdomController>().coal <= 10 ||
                    this.GetComponent<KingdomController>().iron <= 10 || this.GetComponent<KingdomController>().gold <= 5)
                {
                    NPCMngr.GetComponent<NPCManager>().PlaceBuilding(this.GetComponent<KingdomController>().ids,
                        this.GetComponent<Transform>().position.x / 10, this.GetComponent<Transform>().position.z / 10, id,
                        this.gameObject, 2);
                  //  Debug.Log("MINE");
                    return true;
                }
                break;
            case 3:
                if (this.GetComponent<KingdomController>().wood <= 10)
                {
                    NPCMngr.GetComponent<NPCManager>().PlaceBuilding(this.GetComponent<KingdomController>().ids,
                        this.GetComponent<Transform>().position.x / 10, this.GetComponent<Transform>().position.z / 10, id,
                        this.gameObject, 0);
                 //   Debug.Log("COTTAGE");
                    return true;
                }
                break;
            case 4:
                if (this.GetComponent<KingdomController>().defenceKnights == 0 &&
                    this.GetComponent<KingdomController>().attackKnights == 0 && brc == null)
                {
                  //  Debug.Log("BARRACKS");
                    float x = 0, z = 0;
                    float kx = this.GetComponent<Transform>().position.x / 10;
                    float kz = this.GetComponent<Transform>().position.z / 10;
                    int cnt = 0;
                    float rng = this.GetComponent<KingdomController>().range / 10f;
                    do
                    {
                        if (kx - rng < 0)
                            kx = 0;
                        else if (kx + rng > map.GetComponent<Map>().xSize)
                            kx = map.GetComponent<Map>().xSize - 1;
                        else
                            x = Mathf.Floor(Random.Range(kx - rng, kx + rng));

                        if (kz - rng < 0)
                            kz = 0;
                        else if (kz + rng > map.GetComponent<Map>().ySize)
                            kz = map.GetComponent<Map>().xSize - 1;
                        else
                            z = Mathf.Floor(Random.Range(kz - rng, kz + rng));

                        cnt++;
                        if (cnt >= 500)
                            break;
                    } while (map.GetComponent<Map>().getTile((int)(x), (int)(z)) != 1);

                    if (cnt < 500)
                    {
                       // GameObject tmp;
                        map.GetComponent<Map>().buildingPlaced((int)(x), (int)(z), -1);

                        brc = Instantiate<GameObject>(this.GetComponent<KingdomController>().barracks, new Vector3(x * 10, 2.5f, z * 10), Quaternion.identity);

                        brc.GetComponent<BuildingController>().id = id;
                        //   tmp.GetComponent<NPCController>().kingdomId = kid;
                        brc.GetComponent<BuildingController>().kingdom = this.gameObject;

                        brc.GetComponent<BuildingController>().tag = ""+id;

                        brc.name = brc.name + 0 + this.GetComponent<KingdomController>().ids;
                        this.GetComponent<KingdomController>().ids++;
                        //brc = tmp;
                        brc.GetComponent<BuildingController>().placed = true;
                        brc.GetComponent<BarracksController>().kingdom = this.gameObject;
                        brc.GetComponent<BarracksController>().isTraining = true;

                        brc.GetComponent<BarracksController>().menu = GameObject.Find(brc.name + "/Menu");

                     //   brc.GetComponent<BarracksController>().menu.SetActive(false);
                        return true;
                    }
          
                }
          
                break;
        }

        switch (m)
        {
            case 0:
                if ((this.GetComponent<KingdomController>().stone >= 8 &&
                    this.GetComponent<KingdomController>().wood >= 8 &&
                    this.GetComponent<KingdomController>().iron >= 8 &&
                    this.GetComponent<KingdomController>().gold >= 4))
                {
                    this.GetComponent<KingdomController>().Upgrade();
                    return true;
                }
                break;
            case 1:
                if ((this.GetComponent<KingdomController>().wood >= 16 &&
                    this.GetComponent<KingdomController>().stone >= 4))
                {
                    NPCMngr.GetComponent<NPCManager>().PlaceBuilding(this.GetComponent<KingdomController>().ids,
                        this.GetComponent<Transform>().position.x / 10, this.GetComponent<Transform>().position.z / 10, id,
                        this.gameObject, 1);
                    return true;
                }
                break;
            case 2:
                if ((this.GetComponent<KingdomController>().wood >= 20))
                {
                    NPCMngr.GetComponent<NPCManager>().PlaceBuilding(this.GetComponent<KingdomController>().ids,
                        this.GetComponent<Transform>().position.x / 10, this.GetComponent<Transform>().position.z / 10, id,
                        this.gameObject, 2);
                    return true;
                }
                break;
            case 3:
                if ((this.GetComponent<KingdomController>().wood >= 12 &&
                    this.GetComponent<KingdomController>().stone >= 12))
                {
                    NPCMngr.GetComponent<NPCManager>().PlaceBuilding(this.GetComponent<KingdomController>().ids,
                        this.GetComponent<Transform>().position.x / 10, this.GetComponent<Transform>().position.z / 10, id,
                        this.gameObject, 1);
                    return true;
                }
                break;
        }
        return false;
        }

    /*
    public void TradeMenu()
    {
        if (tradeMenu.activeSelf == false)
            tradeMenu.SetActive(true);
        else
            tradeMenu.SetActive(false);
    }
    */

    public void AttackButtonPress()
    {
        playerKingdom.GetComponent<KingdomController>().attackArmy.SetActive(true);
        playerKingdom.GetComponent<KingdomController>().attackArmy.GetComponent<AttackArmy>().Attack(this.gameObject);
    }

    void AttackPlayer()
    {
        this.GetComponent<KingdomController>().attackArmy.SetActive(true);
        this.GetComponent<KingdomController>().attackArmy.GetComponent<AttackArmy>().Attack(playerKingdom);
  
    }

    void Defend()
    {
      //  playerKingdom.GetComponent<KingdomController>().attackArmy.SetActive(true);
        playerKingdom.GetComponent<KingdomController>().attackArmy.GetComponent<AttackArmy>().Defend();
        this.GetComponent<KingdomController>().defenceArmy.GetComponent<DefenceArmy>().target = playerKingdom.GetComponent<KingdomController>().attackArmy;
    }
}
