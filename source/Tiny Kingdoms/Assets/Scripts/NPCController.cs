using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NPCController : MonoBehaviour
{

    public int kingdomId;

    [HideInInspector] public int id = -1;

    public int buildingClass;
    // -1 = kingdom, farm, barracks
    // -2 = lumberjack cottage
    // -3 = mine
    // -4 = docks

    public bool expansions = false;

    public string[] materials;
    int randMaterial;

    [System.Serializable]
    public struct upgradeRequirement
    {
        public string material;
        public int amount;
    };

    public upgradeRequirement[] upgradeRequirements;

    public float productionTime = 1f;
    float timer;

    [HideInInspector] public int health;
    public int level = 1;

    RaycastHit hit;
    Ray ray;

    public GameObject kingdom;

    public GameObject closeMenuButton;
    public GameObject menu;

    public GameObject globalManager;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        menu = GameObject.Find(this.name + "/Menu");
        menu.GetComponent<Menu>().Set();
        closeMenuButton = GameObject.Find(this.name + "/Menu/Canvas/CloseMenuButton");

        // FIND KINGDOM

    //    if (this.GetComponent<NPCController>().expansions == false)
            menu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (materials.Length > 0)
        {
            timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
            if (timer <= 0f)
            {
                timer = productionTime;
                randMaterial = Random.Range(0, materials.Length);

                if (materials[randMaterial] == "wood")
                {
                    kingdom.GetComponent<NPCKingdomController>().wood += 1;
                }
                else if (materials[randMaterial] == "wheat")
                {
                    kingdom.GetComponent<NPCKingdomController>().wheat += 1;
                }
                else if (materials[randMaterial] == "stone")
                {
                    kingdom.GetComponent<NPCKingdomController>().stone += 1;
                }
                else if (materials[randMaterial] == "coal")
                {
                    kingdom.GetComponent<NPCKingdomController>().coal += 1;
                }
                else if (materials[randMaterial] == "iron")
                {
                    kingdom.GetComponent<NPCKingdomController>().iron += 1;
                }
                else if (materials[randMaterial] == "gold")
                {
                    kingdom.GetComponent<NPCKingdomController>().gold += 1;
                }
            }
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == this.name)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    menu.SetActive(true);
                }
            }
        }
    }
}
