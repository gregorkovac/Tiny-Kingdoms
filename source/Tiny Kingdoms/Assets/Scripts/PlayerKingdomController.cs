using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKingdomController : MonoBehaviour
{
    [HideInInspector] public int wheat = 0;
    [HideInInspector] public int wood = 0;
    [HideInInspector] public int stone = 0;
    [HideInInspector] public int coal = 0;
    [HideInInspector] public int iron = 0;
    [HideInInspector] public int gold = 0;
    [HideInInspector] public int citizens = 20;
    [HideInInspector] public int attackKnights = 0;
    [HideInInspector] public int defenceKnights = 0;

    int wheatPrev = 0;
    int woodPrev = 0;
    int stonePrev = 0;
    int coalPrev = 0;
    int ironPrev = 0;
    int goldPrev = 0;
    int citizensPrev = 20;
    int attackKnightsPrev = 0;
    int defenceKnightsPrev = 0;

    public struct state
    {
        public float timer;
        public bool good;
    };

    state wheatState, woodState, stoneState, coalState, ironState, goldState,
        citizensState, attackKnightsState, defenceKnightsState;

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

    public GameObject map;

    public GameObject pauseMenu;

    public bool isPlaced = false;

    public GameObject requirementsMenu;

    RaycastHit hit;
    Ray ray;

    public Vector3 targetPos;
    public Vector3 prevHit = new Vector3(0f, 2.5f, 0f);

    float flashTime = 1f;

    float mspeed = 1f;

    public GameObject soundManager;

    GameObject placeParticle;

    // Start is called before the first frame update
    void Start()
    {
        buildingMenu.SetActive(false);
        this.tag = "0";
        isPlaced = false;
        pauseMenu = GameObject.Find("Pause");
       // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       // if (Physics.Raycast(ray, out hit, Mathf.Infinity))
       // {
       //     transform.position = new Vector3(hit.point.x, 2.5f, hit.point.z);
       //     targetPos = transform.position;
       // }
        transform.position = prevHit;

        placeParticle = GameObject.Find("PlaceParticle");
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlaced == false)
        {
            Place();
        } else
        {
            if (transform.position != targetPos)
            {
                float nx = 0, nz = 0;
                mspeed = 1f;
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

                transform.position = new Vector3(Mathf.Floor((transform.position.x + nx)), transform.position.y, Mathf.Floor((transform.position.z + nz)));
            }

            wheat = this.GetComponent<KingdomController>().wheat;
            wood = this.GetComponent<KingdomController>().wood;
            stone = this.GetComponent<KingdomController>().stone;
            coal = this.GetComponent<KingdomController>().coal;
            iron = this.GetComponent<KingdomController>().iron;
            gold = this.GetComponent<KingdomController>().gold;
            citizens = this.GetComponent<KingdomController>().citizens;
            attackKnights = this.GetComponent<KingdomController>().attackKnights;
            defenceKnights = this.GetComponent<KingdomController>().defenceKnights;

            wheatDisplay.GetComponent<Text>().text = wheat.ToString();
            woodDisplay.GetComponent<Text>().text = wood.ToString();
            stoneDisplay.GetComponent<Text>().text = stone.ToString();
            coalDisplay.GetComponent<Text>().text = coal.ToString();
            ironDisplay.GetComponent<Text>().text = iron.ToString();
            goldDisplay.GetComponent<Text>().text = gold.ToString();
            citizenDisplay.GetComponent<Text>().text = citizens.ToString();
            attackKnightsDisplay.GetComponent<Text>().text = attackKnights.ToString();
            defenceKnightsDisplay.GetComponent<Text>().text = defenceKnights.ToString();

            if (citizens + attackKnights + defenceKnights <= 0)
            {
                GetComponent<KingdomController>().health = 0;
            }


            //////////////////////////////
            if (wheat > wheatPrev)
            {
                wheatState.good = true;
                wheatState.timer = flashTime;
            } else if (wheat < wheatPrev)
            {
                wheatState.good = false;
                wheatState.timer = flashTime;
            }

            if (wheatState.timer > 0)
            { 
                wheatState.timer -= 1f * Time.deltaTime;
                if (wheatState.good == true)
                    wheatDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, wheatState.timer);
                else
                    wheatDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, wheatState.timer);

            } else
            {
                wheatDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (wood > woodPrev)
            {
                woodState.good = true;
                woodState.timer = flashTime;
            }
            else if (wood < woodPrev)
            {
                woodState.good = false;
                woodState.timer = flashTime;
            }

            if (woodState.timer > 0)
            {
                woodState.timer -= 1f * Time.deltaTime;
                if (woodState.good == true)
                    woodDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, woodState.timer);
                else
                    woodDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, woodState.timer);
            }
            else
            {
                woodDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (coal > coalPrev)
            {
                coalState.good = true;
                coalState.timer = flashTime;
            }
            else if (coal < coalPrev)
            {
                coalState.good = false;
                coalState.timer = flashTime;
            }

            if (coalState.timer > 0)
            {
                coalState.timer -= 1f * Time.deltaTime;
                if (coalState.good == true)
                    coalDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, coalState.timer);
                else
                    coalDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, coalState.timer);

            }
            else
            {
                coalDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (stone > stonePrev)
            {
                stoneState.good = true;
                stoneState.timer = flashTime;
            }
            else if (stone < stonePrev)
            {
                stoneState.good = false;
                stoneState.timer = flashTime;
            }

            if (stoneState.timer > 0)
            {
                stoneState.timer -= 1f * Time.deltaTime;
                if (stoneState.good == true)
                    stoneDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, stoneState.timer);
                else
                    stoneDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, stoneState.timer);

            }
            else
            {
                stoneDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (iron > ironPrev)
            {
                ironState.good = true;
                ironState.timer = flashTime;
            }
            else if (iron < ironPrev)
            {
                ironState.good = false;
                ironState.timer = flashTime;
            }

            if (ironState.timer > 0)
            {
                ironState.timer -= 1f * Time.deltaTime;
                if (ironState.good == true)
                    ironDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, ironState.timer);
                else
                    ironDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, ironState.timer);

            }
            else
            {
                ironDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (gold > goldPrev)
            {
                goldState.good = true;
                goldState.timer = flashTime;
            }
            else if (gold < goldPrev)
            {
                goldState.good = false;
                goldState.timer = flashTime;
            }

            if (goldState.timer > 0)
            {
                goldState.timer -= 1f * Time.deltaTime;
                if (goldState.good == true)
                    goldDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, goldState.timer);
                else
                    goldDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, goldState.timer);

            }
            else
            {
                goldDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (citizens > citizensPrev)
            {
                citizensState.good = true;
                citizensState.timer = flashTime;
            }
            else if (citizens < citizensPrev)
            {
                citizensState.good = false;
                citizensState.timer = flashTime;
            }

            if (citizensState.timer > 0)
            {
                citizensState.timer -= 1f * Time.deltaTime;
                if (citizensState.good == true)
                    citizenDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, citizensState.timer);
                else
                    citizenDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, citizensState.timer);

            }
            else
            {
                citizenDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (attackKnights > attackKnightsPrev)
            {
                attackKnightsState.good = true;
                attackKnightsState.timer = flashTime;
            }
            else if (attackKnights < attackKnightsPrev)
            {
                attackKnightsState.good = false;
                attackKnightsState.timer = flashTime;
            }

            if (attackKnightsState.timer > 0)
            {
                attackKnightsState.timer -= 1f * Time.deltaTime;
                if (attackKnightsState.good == true)
                    attackKnightsDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, attackKnightsState.timer);
                else
                    attackKnightsDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, attackKnightsState.timer);

            }
            else
            {
                attackKnightsDisplay.GetComponent<Text>().color = Color.white;
            }

            //////////////////////////////
            if (defenceKnights > defenceKnightsPrev)
            {
                defenceKnightsState.good = true;
                defenceKnightsState.timer = flashTime;
            }
            else if (defenceKnights < defenceKnightsPrev)
            {
                defenceKnightsState.good = false;
                defenceKnightsState.timer = flashTime;
            }

            if (defenceKnightsState.timer > 0)
            {
                defenceKnightsState.timer -= 1f * Time.deltaTime;
                if (defenceKnightsState.good == true)
                    defenceKnightsDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.green, defenceKnightsState.timer);
                else
                    defenceKnightsDisplay.GetComponent<Text>().color = Color.Lerp(Color.white, Color.red, defenceKnightsState.timer);

            }
            else
            {
                defenceKnightsDisplay.GetComponent<Text>().color = Color.white;
            }



            wheatPrev = wheat;
            woodPrev = wood;
            stonePrev = stone;
            coalPrev = coal;
            ironPrev = iron;
            goldPrev = gold;
            citizensPrev = citizens;
            attackKnightsPrev = attackKnights;
            defenceKnightsPrev = defenceKnights;
        }
    }

    void Place()
    {
        targetPos = new Vector3(0,0,0);
        if (pauseMenu.GetComponent<PauseMenu1>().pause == false)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                int tile = map.GetComponent<Map>().getTile((int)(Mathf.Round(hit.point.x / 10)), (int)(Mathf.Round(hit.point.z / 10)));
                if (tile == 1)
                {
                    //transform.position = hit.point;
                    //transform.position = new Vector3(Mathf.Floor(transform.position.x / 10) * 10, 2.5f, Mathf.Floor(transform.position.z / 10) * 10);
                    // targetPos = new Vector3(Mathf.Floor(transform.position.x / 10) * 10, 2.5f, Mathf.Floor(transform.position.z / 10) * 10);

                    targetPos = new Vector3(Mathf.Round(hit.point.x / 10) * 10, 2.5f, Mathf.Round(hit.point.z / 10) * 10);
                }

            }
            if (Input.GetKey(KeyCode.Mouse0) && map.GetComponent<Map>().getTile((int)(Mathf.Round(transform.position.x / 10)), (int)(Mathf.Round(transform.position.z / 10))) == 1)
            {
                isPlaced = true;
                map.GetComponent<Map>().buildingPlaced((int)(Mathf.Round(hit.point.x / 10)), (int)(Mathf.Round(hit.point.z / 10)), -1);
                soundManager.GetComponent<SoundManager>().PlaySound(4);
                placeParticle.GetComponent<PlaceParticle>().PlayParticles(hit.point.x, hit.point.z);
            }

            if (targetPos.y == 0f)
                targetPos = prevHit;

            if (transform.position != targetPos)
            {
                float nx = 0, nz = 0;
                mspeed = 1f;
                if (targetPos.x > transform.position.x)
                {
                    nx = mspeed;
                } else if (targetPos.x < transform.position.x)
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

                transform.position = new Vector3(Mathf.Floor((transform.position.x+nx)), transform.position.y, Mathf.Floor((transform.position.z+nz)));
            }

            prevHit = targetPos;
        }
    }

    public void PlaceBuilding()
    {


        if (buildingMenu.activeSelf == false)
        {
            buildingMenu.SetActive(true);
        }
        else
        {
            buildingMenu.SetActive(false);
        }
    }

    public void ShowRequirements(int n)
    {
        string s = "";
        string s2 = "";

        switch (n)
        {
            case 1:
                if (wood >= 4) s += "4 wood\n";
                else s2 += "4 wood\n";

                if (stone >= 1) s += "1 stone\n";
                else s2 += "1 stone\n";

                if (citizens >= 1) s += "1 citizen\n";
                else s2 += "1 citizen\n";

                break;

            case 2:
                if (wood >= 5) s += "5 wood\n";
                else s2 += "5 wood\n";

                if (citizens >= 2) s += "2 citizens\n";
                else s2 += "2 citizens\n";

                break;

            case 3:
                if (wood >= 5) s += "5 wood\n";
                else s2 += "5 wood\n";
                if (citizens >= 1) s += "1 citizen\n";
                else s2 += "1 citizen\n";
                break;

            case 4:
                if (wood >= 6) s += "6 wood\n";
                else s2 += "6 wood\n";
                if (stone >= 6) s += "6 stone\n";
                else s2 += "6 stone\n";
                if (citizens >= 2) s += "2 citizens\n";
                else s2 += "2 citizens\n";
                break;

            case 5:
                if (wood >= 10) s += "10 wood\n";
                else s2 += "10 wood\n";
                if (citizens >= 3) s += "3 citizens\n";
                else s2 += "3 citizens\n";
                break;

            case 6:
                if (wheat >= 4) s += "4 wheat\n";
                else s2 += "4 wheat\n";
                if (coal >= 2) s += "2 coal\n";
                else s2 += "2 coal\n";
                break;

            case 7:
                if (stone >= 2) s += "2 stone\n";
                else s2 += "2 stone\n";

                if (wood >= 2) s += "2 wood\n";
                else s2 += "2 wood\n";

                if (iron >= 2) s += "2 iron\n";
                else s2 += "2 iron\n";

                if (gold >= 1) s += "1 gold\n";
                else s2 += "1 gold\n";

                break;
            default:
                s = "";
                break;
        }

        requirementsMenu.GetComponent<RequirementMenu>().Display(s, s2);
    }
}
