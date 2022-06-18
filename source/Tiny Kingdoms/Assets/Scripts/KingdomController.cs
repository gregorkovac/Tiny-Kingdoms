using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingdomController : MonoBehaviour
{

     public int wheat = 0;
     public int wood = 0;
     public int stone = 0;
     public int coal = 0;
     public int iron = 0;
     public int gold = 0;
    public int citizens = 20;
     public int attackKnights = 0;
     public int defenceKnights = 0;

    /*
    public GameObject wheatDisplay;
    public GameObject woodDisplay;
    public GameObject stoneDisplay;
    public GameObject coalDisplay;
    public GameObject ironDisplay;
    public GameObject goldDisplay;
    public GameObject citizenDisplay;
    public GameObject attackKnightsDisplay;
    public GameObject defenceKnightsDisplay;

    public GameObject buildingMenu;
    */
    public GameObject genericBuilding;
    public GameObject farm;
    public GameObject mine;
    public GameObject cottage;
    public GameObject barracks;
    public GameObject dock;

    public float range = 20f;
    
    RaycastHit hit;
    Ray ray;

    public GameObject menu;

   // bool isPlaced = false;

    public GameObject map;

    public GameObject cam;

    public GameObject closeMenuButton;

    public GameObject attackArmy;
    public GameObject defenceArmy;

    GameObject damageModel;

    public int ids = 0;

    public float citizenDeathTime = 1f;
    float citizenTimer;

    public float health = 100;
    float prevHealth;
    float dmgTimer;

    GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {

        damageModel = GameObject.Find(this.name + "/DamageModel");
        damageModel.SetActive(false);
        dmgTimer = 0f;

        soundManager = GameObject.Find("SoundManager");

        wheat = Random.Range(10, 20);
        wood = Random.Range(10, 20);
        stone = Random.Range(10, 20);
        coal = Random.Range(10, 20);
        iron = Random.Range(10, 20);
        gold = Random.Range(10, 20);
        citizens = Random.Range(20, 30);
        attackKnights = 0;
        defenceKnights = 0;

        health = 100;

        ids = 0;

        menu = GameObject.Find(this.name + "/Menu");
        menu.GetComponent<Menu>().Set();
        map = GameObject.Find("Map");
        cam = GameObject.Find("Main Camera");

        closeMenuButton = GameObject.Find(this.name + "/Menu/Canvas/CloseMenuButton");
        closeMenuButton.GetComponent<Button>().onClick.AddListener(CloseMenu);


        menu.SetActive(false);
       // buildingMenu.SetActive(false);
       
        citizenTimer = citizenDeathTime;

        attackArmy = GameObject.Find(this.name + "/Army");
        attackArmy.GetComponent<AttackArmy>().isAttacking = false;
        attackArmy.SetActive(false);

        defenceArmy = GameObject.Find(this.name + "/DefenceArmy");
        defenceArmy.GetComponent<DefenceArmy>().isAttacking = false;
        defenceArmy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*    if (isPlaced == false)
            {
                Place();
            } else
            { */

        if (prevHealth > health)
        {
            dmgTimer = 1f;
        }

        if (dmgTimer >= 0f)
        {
            dmgTimer -= 1f * Time.deltaTime;
            damageModel.SetActive(true);
        }
        else
        {
            damageModel.SetActive(false);
        }

        prevHealth = health;

        if (health <= 0)
        {
            soundManager.GetComponent<SoundManager>().PlaySound(6);
            Destroy(this.gameObject);
        }

            Citizens();
      //  }

    }

    /*
    void Place()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            int tile = map.GetComponent<Map>().getTile((int)(Mathf.Floor(hit.point.x / 10)), (int)(Mathf.Floor(hit.point.z / 10)));
            if (tile == 1)
            {
                transform.position = hit.point;
                transform.position = new Vector3(Mathf.Floor(transform.position.x / 10) * 10, 2.5f, Mathf.Floor(transform.position.z / 10) * 10);
            }
            
        }
        if (Input.GetKey(KeyCode.Mouse0) && map.GetComponent<Map>().getTile((int)(Mathf.Floor(transform.position.x / 10)), (int)(Mathf.Floor(transform.position.z / 10))) == 1)
        {
            isPlaced = true;
            map.GetComponent<Map>().buildingPlaced((int)(Mathf.Floor(hit.point.x / 10)), (int)(Mathf.Floor(hit.point.z / 10)), -1);
        }       
    }
    */
    
    public void CloseMenu()
    {
        menu.SetActive(false);
    }
    

    public void Upgrade()
    {
        if (stone >= 2 && wood >= 2 && iron >= 2 && gold >= 1)
        {
            citizens += 20;
            stone -= 2;
            wood -= 2;
            iron -= 2;
            gold -= 1;

            if (this.TryGetComponent(typeof(PlayerKingdomController), out Component component))
            {
                this.GetComponent<PlayerKingdomController>().soundManager.GetComponent<SoundManager>().PlaySound(8);
            }
        }
    }
    /*
    public void PlaceBuilding()
    {
  

        if (buildingMenu.activeSelf == false)
        {
            buildingMenu.SetActive(true);
        } else
        {
            buildingMenu.SetActive(false);
        }
    }
    */

    public void PlaceFarm()
    {
        if (wood >= 4 && stone >= 1 && citizens >= 1)
        {
            range = range + 5f;
            wood -= 4;
            stone -= 1;
            citizens -= 1;
            GameObject tmp = Instantiate<GameObject>(farm, new Vector3(0, 0, 0), Quaternion.identity);
            tmp.name = tmp.name + ids.ToString();
            tmp.tag = this.tag;
            tmp.GetComponent<BuildingController>().kingdom = this.gameObject;
            ids++;
            
        }
    }

    public void PlaceBuildingLoad(Vector3 pos, int id, int bClass, int lvl) {
        GameObject tmp;
        switch (bClass) {
            case -1:
                tmp = Instantiate<GameObject>(farm, pos, Quaternion.identity);
                break;
            case -2:
                tmp = Instantiate<GameObject>(cottage, pos, Quaternion.identity);
                break;
            case -3:
                tmp = Instantiate<GameObject>(mine, pos, Quaternion.identity);
                break;
            case -4:
                tmp = Instantiate<GameObject>(dock, pos, Quaternion.identity);
                break;
            case -5:
                tmp = Instantiate<GameObject>(barracks, pos, Quaternion.identity);
                break;
            default:
                tmp = Instantiate<GameObject>(genericBuilding, pos, Quaternion.identity);
                break;

        }
        
        tmp.name = tmp.name + ids.ToString();
        tmp.GetComponent<BuildingController>().id = id;
        tmp.tag = this.tag;
        tmp.GetComponent<BuildingController>().kingdom = this.gameObject;
        tmp.GetComponent<BuildingController>().placed = true;
        tmp.GetComponent<PlayerBuildingController>().placed = true;
        tmp.GetComponent<BuildingController>().level = lvl;

        tmp.GetComponent<PlayerBuildingController>().prevHit = new Vector3(pos.x, 2.5f, pos.z);
        tmp.GetComponent<PlayerBuildingController>().targetPos = new Vector3(pos.x, 2.5f, pos.z);
        tmp.GetComponent<PlayerBuildingController>().placed = true;
    }

    public void PlaceMine()
    {
        if (wood >= 5 && citizens >= 2)
        {
            range = range + 5f;
            wood -= 5;
            citizens -= 2;
            GameObject tmp = Instantiate<GameObject>(mine, new Vector3(0, 0, 0), Quaternion.identity);
            tmp.name = tmp.name + ids.ToString();
            tmp.tag = this.tag;
            tmp.GetComponent<BuildingController>().kingdom = this.gameObject;
            ids++;
        }
    }

    public void PlaceCottage()
    {
        if (wood >= 5 && citizens >= 1)
        {
            range = range + 5f;
            wood -= 5;
            citizens -= 1;
            GameObject tmp = Instantiate<GameObject>(cottage, new Vector3(0, 0, 0), Quaternion.identity);
            tmp.name = tmp.name + ids.ToString();
            tmp.tag = this.tag;
            tmp.GetComponent<BuildingController>().kingdom = this.gameObject;
            ids++;
        }
    }

    public void PlaceBarracks()
    {
        if (wood >= 6 && stone >= 6 && citizens >= 2)
        {
            range = range + 5f;
            wood -= 6;
            stone -= 6;
            citizens -= 2;
            GameObject tmp = Instantiate<GameObject>(barracks, new Vector3(0, 0, 0), Quaternion.identity);
            tmp.name = tmp.name + ids.ToString();
            tmp.tag = this.tag;
            tmp.GetComponent<BuildingController>().kingdom = this.gameObject;
            tmp.GetComponent<BarracksController>().kingdom = this.gameObject;
            ids++;
        }
    }

    public void PlaceDock()
    {
        if (wood >= 10 && citizens >= 3)
        {
            range = range + 5f;
            wood -= 10;
            citizens -= 3;
            GameObject tmp = Instantiate<GameObject>(dock, new Vector3(0, 0, 0), Quaternion.identity);
            
            tmp.name = tmp.name + ids.ToString();
            tmp.tag = this.tag;
            tmp.GetComponent<BuildingController>().kingdom = this.gameObject;
            ids++;
        }
    }

    private void OnMouseDown()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (menu.activeSelf == true)
            {
                if (menu.GetComponent<Menu>().globalManager == null)
                {
                    menu.GetComponent<Menu>().Set();
                }
                menu.GetComponent<Menu>().globalManager.GetComponent<GlobalManager>().selectedMenu = -1;
                menu.SetActive(false);

            }
            else
            {
                if (menu.GetComponent<Menu>().globalManager == null)
                {
                    menu.GetComponent<Menu>().Set();
                }
                menu.SetActive(true);
                menu.GetComponent<Menu>().globalManager.GetComponent<GlobalManager>().selectedMenu = menu.GetComponent<Menu>().id;
            }
        }
    }

    void Citizens()
    {
        citizenTimer -= 1f * Time.deltaTime;
        if (citizenTimer <= 0)
        {
            citizenTimer = citizenDeathTime;
            citizens = citizens - 2;
            if (citizens < 0)
                citizens = 0;
        }
    }

    public void FeedCitizens()
    {
        if (wheat >= 4 && coal >= 2)
        {
            citizenTimer = citizenDeathTime;
            wheat = wheat - 4;
            coal = coal - 2;
            if (this.TryGetComponent(typeof(PlayerKingdomController), out Component component))
            {
                this.GetComponent<PlayerKingdomController>().soundManager.GetComponent<SoundManager>().PlaySound(7);
            }
        }
    }

    

}
