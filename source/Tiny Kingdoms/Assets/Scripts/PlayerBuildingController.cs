using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerBuildingController : MonoBehaviour
{
    public GameObject levelDisplay;
    public GameObject upgradeButton;

    bool isPlaced = false;
    public bool placed = false;

    RaycastHit hit;
    Ray ray;

    GameObject map;
    GameObject kingdom;

    int lprev = 0;

    public GameObject pauseMenu;

    public GameObject requirementsMenu;

    public Vector3 prevHit;
    public Vector3 targetPos;

    public GameObject soundManager;

    GameObject placeParticle;

 //   public GameObject buildParticles;

    // Start is called before the first frame update
    void Start()
    {
        requirementsMenu = GameObject.Find("RequirementMenu");

        levelDisplay = GameObject.Find(this.name + "/Menu/Canvas/Level");
        upgradeButton = GameObject.Find(this.name + "/Menu/Canvas/UpgradeButton");

        map = GameObject.FindGameObjectWithTag("Map");
        kingdom = GameObject.FindGameObjectWithTag("0");

        upgradeButton.GetComponent<Button>().onClick.AddListener(this.GetComponent<BuildingController>().Upgrade);
        levelDisplay.GetComponent<Text>().text = this.GetComponent<BuildingController>().level.ToString();

        //if (this.GetComponent<NPCController>().expansions == false)
        //    this.GetComponent<NPCController>().menu.SetActive(false);
        this.GetComponent<BuildingController>().kingdom = GameObject.FindGameObjectWithTag("0");
        
        this.GetComponent<BuildingController>().menu.SetActive(false);

        pauseMenu = GameObject.Find("Pause");

        soundManager = GameObject.Find("SoundManager");
    //    buildParticles = GameObject.Find(this.name + "/Particle System");

    //    transform.position = new Vector3(kingdom.transform.position.x, 2.5f, kingdom.transform.position.z);
        prevHit = kingdom.transform.position;

        placeParticle = GameObject.Find("PlaceParticle");


    }

    // Update is called once per frame
    void Update()
    {
        if (requirementsMenu == null)
            requirementsMenu = GameObject.Find("RequirementMenu");

        if ((isPlaced == false) && placed == false)
        {
            if (transform.position.x == 0f && transform.position.z == 0f)
            {
                transform.position = kingdom.transform.position;
            }

            isPlaced = false;
            Place();
        }
        else if (placed == true)
        {
            if (transform.position != targetPos)
            {
                float nx = 0, nz = 0;
                float mspeed = 1f;
                if (targetPos.x > transform.position.x)
                {
                    nx = mspeed;
                }
                else if (targetPos.x < transform.position.x)
                {
                    nx = -mspeed;
                }
                else if (targetPos.z > transform.position.z)
                {
                    nz = mspeed;
                }
                else if (targetPos.z < transform.position.z)
                {
                    nz = -mspeed;
                }

                transform.position = new Vector3(Mathf.Floor((transform.position.x + nx)), 2.5f, Mathf.Floor((transform.position.z + nz)));
            }

            if (lprev != this.GetComponent<BuildingController>().level)
            {
                levelDisplay.GetComponent<Text>().text = this.GetComponent<BuildingController>().level.ToString();
                lprev = this.GetComponent<BuildingController>().level;
            }
        }
    }

    void Place()
    {
        targetPos = new Vector3(kingdom.transform.position.x, 0, kingdom.transform.position.z);
        if (pauseMenu.GetComponent<PauseMenu1>().pause == false)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                int tile = map.GetComponent<Map>().getTile((int)(Mathf.Round(hit.point.x / 10)), (int)(Mathf.Round(hit.point.z / 10)));
                if (tile == this.GetComponent<BuildingController>().placableOn)
                {
                    //   transform.position = hit.point;
                    //   transform.position = new Vector3(Mathf.Floor(transform.position.x / 10) * 10, 2.5f, Mathf.Floor(transform.position.z / 10) * 10);
                    targetPos = new Vector3(Mathf.Round(hit.point.x / 10) * 10, 2.5f, Mathf.Round(hit.point.z / 10) * 10);
                }
            }
            if (Input.GetKey(KeyCode.Mouse0) && map.GetComponent<Map>().getTile((int)(Mathf.Round(transform.position.x / 10)), (int)(Mathf.Round(transform.position.z / 10))) == this.GetComponent<BuildingController>().placableOn)
            {
                if (Vector3.Distance(transform.position, kingdom.transform.position) <= kingdom.GetComponent<KingdomController>().range)
                {
                    isPlaced = true;
                    placed = true;
                    map.GetComponent<Map>().buildingPlaced((int)(Mathf.Round(hit.point.x / 10)), (int)(Mathf.Round(hit.point.z / 10)), this.GetComponent<BuildingController>().buildingClass);
                    this.GetComponent<BuildingController>().timer = this.GetComponent<BuildingController>().productionTime;
                    soundManager.GetComponent<SoundManager>().PlaySound(4);
                    placeParticle.GetComponent<PlaceParticle>().PlayParticles(hit.point.x, hit.point.z);
                }
            }
        }

        if (targetPos.y == 0f)
            targetPos = prevHit;

        if (transform.position != targetPos)
        {
            float nx = 0, nz = 0;
            float mspeed = 1f;
            if (targetPos.x > transform.position.x)
            {
                nx = mspeed;
            }
            else if (targetPos.x < transform.position.x)
            {
                nx = -mspeed;
            }
            else if (targetPos.z > transform.position.z)
            {
                nz = mspeed;
            }
            else if (targetPos.z < transform.position.z)
            {
                nz = -mspeed;
            }

            transform.position = new Vector3(Mathf.Floor((transform.position.x + nx)), 2.5f, Mathf.Floor((transform.position.z + nz)));
        }

        prevHit = targetPos;
    }

    public void ShowRequirements()
    {
        string s = "";
        string s1 = "";

        for (int i = 0; i < this.GetComponent<BuildingController>().upgradeRequirements.Length; i++)
        {
            if (this.GetComponent<BuildingController>().upgradeRequirements[i].material == "wood")
            {
                if (kingdom.GetComponent<KingdomController>().wood <= this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level)
                {
                    s1 += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                } else
                {
                    s += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
            }
            else if (this.GetComponent<BuildingController>().upgradeRequirements[i].material == "wheat")
            {
                if (kingdom.GetComponent<KingdomController>().wheat <= this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level)
                {
                    s1 += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
                else
                {
                    s += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
            }
            else if (this.GetComponent<BuildingController>().upgradeRequirements[i].material == "stone")
            {
                if (kingdom.GetComponent<KingdomController>().stone <= this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level)
                {
                    s1 += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";

                }
                else
                {
                    s += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
            }
            else if (this.GetComponent<BuildingController>().upgradeRequirements[i].material == "coal")
            {
                if (kingdom.GetComponent<KingdomController>().coal <= this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level)
                {
                    s1 += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";

                }
                else
                {
                    s += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
            }
            else if (this.GetComponent<BuildingController>().upgradeRequirements[i].material == "iron")
            {
                if (kingdom.GetComponent<KingdomController>().iron <= this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level)
                {
                    s1 += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";

                }
                else
                {
                    s += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
            }
            else if (this.GetComponent<BuildingController>().upgradeRequirements[i].material == "gold")
            {
                if (kingdom.GetComponent<KingdomController>().gold <= this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level)
                {
                    s1 += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";

                }
                else
                {
                    s += (this.GetComponent<BuildingController>().upgradeRequirements[i].amount + this.GetComponent<BuildingController>().level) + " " + this.GetComponent<BuildingController>().upgradeRequirements[i].material + "\n";
                }
            }
        }

        requirementsMenu.GetComponent<RequirementMenu>().Display(s, s1);
    }

    public void Hide()
    {
        requirementsMenu.GetComponent<RequirementMenu>().Hide();
    }
}
