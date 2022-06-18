using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingController : MonoBehaviour
{
    // Unikatna id številka zgradbe
    [HideInInspector] public int id = -1;

    // Tip zgradbe
    public int buildingClass;
    // -1 = kraljestvo, kmetija, barake
    // -2 = koča
    // -3 = rudnik
    // -4 = pristanišče

    /*
      Nekatere zgradbe (naprimer barake) imajo dodatno skripto.
      Takrat spremenljivko expansions nastavim na true
     */
    public bool expansions = false;

    /* Spremenljivka, ki se uporabla za določanje tipa ploščice, na katero
      lahko postavimo zgradbo */
    public int placableOn;

    // Spremenljivka, ki je true, če je zgradba že postavljena
    public bool placed = true;

    // Tabela surovin, ki jih proizvaja zgradba
    public string[] materials;

    // Spremenljivka za naključno generiranje surovine, ki jo bo dobilo kraljestvo
    int randMaterial;

    // Struktura zahtev za nadgradnjo
    [System.Serializable]
    public struct upgradeRequirement
    {
        // Surovina
        public string material;

        // Potrebna količina te surovine
        public int amount;
    };
    
    // Rabela zahtev za nadgradnjo
    public upgradeRequirement[] upgradeRequirements;

    // Čas, v katerem se proizvede nova surovina
    public float productionTime = 1f;


    [HideInInspector] public float timer;

    public int health;
    float prevHealth;
    float dmgTimer;
    public int level = -1;

    RaycastHit hit;
    Ray ray;

    GameObject map;
    public GameObject kingdom;
    GameObject damageModel;
    public Material dmgMaterial;

    public GameObject menu;
    public GameObject closeMenuButton;

    public GameObject globalManager;

    public GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        damageModel = GameObject.Find(this.name + "/DamageModel");
        if (damageModel != null)
            damageModel.SetActive(false);
        dmgTimer = 0f;

        menu = GameObject.Find(this.name + "/Menu");
        menu.GetComponent<Menu>().Set();
        closeMenuButton = GameObject.Find(this.name + "/Menu/Canvas/CloseMenuButton");

        closeMenuButton.GetComponent<Button>().onClick.AddListener(CloseMenu);

        map = GameObject.FindGameObjectWithTag("Map");
    
        health = 100;
        prevHealth = 100;

        placed = true;
       // isPlaced = true;

        soundManager = GameObject.Find("SoundManager");

        if (expansions == false && kingdom.tag != "0")
            menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (damageModel != null)
        {
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
                soundManager.GetComponent<SoundManager>().PlaySound(5);
                Destroy(this.gameObject);
            }
        }

            
            if (materials.Length > 0)
            {
                timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
                if (timer <= 0f)
                {
                    timer = productionTime;
                    randMaterial = Random.Range(0, materials.Length);

                if (kingdom != null)
                {
                    if (materials[randMaterial] == "wood")
                    {
                        kingdom.GetComponent<KingdomController>().wood += 1;
                    }
                    else if (materials[randMaterial] == "wheat")
                    {
                        kingdom.GetComponent<KingdomController>().wheat += 1;
                    }
                    else if (materials[randMaterial] == "stone")
                    {
                        kingdom.GetComponent<KingdomController>().stone += 1;
                    }
                    else if (materials[randMaterial] == "coal")
                    {
                        kingdom.GetComponent<KingdomController>().coal += 1;
                    }
                    else if (materials[randMaterial] == "iron")
                    {
                        kingdom.GetComponent<KingdomController>().iron += 1;
                    }
                    else if (materials[randMaterial] == "gold")
                    {
                        kingdom.GetComponent<KingdomController>().gold += 1;
                    }
                }
                }
            }
    }

    public void Upgrade()
    {
        if (kingdom != null)
        {
            bool ok = true;

            for (int i = 0; i < upgradeRequirements.Length; i++)
            {
                if (upgradeRequirements[i].material == "wood")
                {
                    if (kingdom.GetComponent<KingdomController>().wood <= upgradeRequirements[i].amount + level)
                    {
                        ok = false;
                    }
                }
                else if (upgradeRequirements[i].material == "wheat")
                {
                    if (kingdom.GetComponent<KingdomController>().wheat <= upgradeRequirements[i].amount + level)
                    {
                        ok = false;
                    }
                }
                else if (upgradeRequirements[i].material == "stone")
                {
                    if (kingdom.GetComponent<KingdomController>().stone <= upgradeRequirements[i].amount + level)
                    {
                        ok = false;
                    }
                }
                else if (upgradeRequirements[i].material == "coal")
                {
                    if (kingdom.GetComponent<KingdomController>().coal <= upgradeRequirements[i].amount + level)
                    {
                        ok = false;
                    }
                }
                else if (upgradeRequirements[i].material == "iron")
                {
                    if (kingdom.GetComponent<KingdomController>().iron <= upgradeRequirements[i].amount + level)
                    {
                        ok = false;
                    }
                }
                else if (upgradeRequirements[i].material == "gold")
                {
                    if (kingdom.GetComponent<KingdomController>().gold <= upgradeRequirements[i].amount + level)
                    {
                        ok = false;
                    }
                }
            }

            if (ok == false)
                return;

            for (int i = 0; i < upgradeRequirements.Length; i++)
            {
                if (upgradeRequirements[i].material == "wood")
                {
                    kingdom.GetComponent<KingdomController>().wood -= upgradeRequirements[i].amount + level;
                }
                else if (upgradeRequirements[i].material == "wheat")
                {
                    kingdom.GetComponent<KingdomController>().wheat -= upgradeRequirements[i].amount + level;
                }
                else if (upgradeRequirements[i].material == "stone")
                {
                    kingdom.GetComponent<KingdomController>().stone -= upgradeRequirements[i].amount + level;
                }
                else if (upgradeRequirements[i].material == "coal")
                {
                    kingdom.GetComponent<KingdomController>().coal -= upgradeRequirements[i].amount + level;
                }
                else if (upgradeRequirements[i].material == "iron")
                {
                    kingdom.GetComponent<KingdomController>().iron -= upgradeRequirements[i].amount + level;
                }
                else if (upgradeRequirements[i].material == "gold")
                {
                    kingdom.GetComponent<KingdomController>().gold -= upgradeRequirements[i].amount + level;
                }

                upgradeRequirements[i].amount *= 2;
            }

            level = level + 1;

            productionTime = productionTime - level / 10f;
            //levelDisplay.GetComponent<Text>().text = level.ToString();

            if (this.TryGetComponent(typeof(PlayerBuildingController), out Component component))
            {
                this.GetComponent<PlayerBuildingController>().soundManager.GetComponent<SoundManager>().PlaySound(8);
            }
        }
        
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
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
}
