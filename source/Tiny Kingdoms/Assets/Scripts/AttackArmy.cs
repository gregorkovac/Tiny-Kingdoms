using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AttackArmy : MonoBehaviour
{

    public GameObject target = null;
    public GameObject[] buildings;
    public GameObject targetKingdom;

    public KingdomController parentKingdom;

    public bool isAttacking = false;

    float timerInterval = 0.1f;
    float timer;

    float attackInterval = 1f;
    float timer2;

    public int minB = 0;

    public int dmg;

    public int health;
    float prevHealth;
    float dmgTimer;

    Vector3 startPos;

    public GameObject closeMenuButton;
    public GameObject menu;
    public GameObject attackButton;

    RaycastHit hit;
    Ray ray;

    public GameObject globalManager;

    GameObject damageModel;

    public GameObject soundManager;

    // Start is called befaore the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        damageModel = GameObject.Find(this.name + "/DamageModel");
        if (damageModel != null)
            damageModel.SetActive(false);
        dmgTimer = 0f;

        timerInterval = 0.0001f;
        attackInterval = 0.1f;
        timer2 = attackInterval;
        isAttacking = true;

        health = 100;
        prevHealth = 100;

        menu = GameObject.Find(this.name + "/Menu");
        closeMenuButton = GameObject.Find(this.name + "/Menu/Canvas/CloseMenuButton");
        attackButton = GameObject.Find(this.name + "/Menu/Canvas/AttackButton");
        closeMenuButton.GetComponent<Button>().onClick.AddListener(CloseMenu);
        attackButton.GetComponent<Button>().onClick.AddListener(Defend);
        menu.SetActive(false);

        parentKingdom = this.GetComponentInParent<KingdomController>();
        dmg = GetComponentInParent<KingdomController>().attackKnights;

        soundManager = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
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
            prevHealth = health;
        }

        if (health <= 0)
        {
            soundManager.GetComponent<SoundManager>().PlaySound(5);
            parentKingdom.attackKnights = (int)(parentKingdom.attackKnights/10);
            timerInterval = 0.0001f;
            attackInterval = 0.1f;
            timer2 = attackInterval;
            transform.position = startPos;
            health = 100;
            isAttacking = false;
            this.gameObject.SetActive(false);
    
        }
  /*      ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == this.name)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    menu.SetActive(true);
                }
            }
        } */

        if (isAttacking == true)
        {
            if (target == null)
                newTarget();
            else
            {

                timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
                if (timer <= 0)
                {
                    timer = timerInterval;
                    Vector3 pos = transform.position;
                    if (target.GetComponent<Transform>().position.x > this.GetComponent<Transform>().position.x)
                    {
                        pos.x += 0.05f;
                    }
                    else if (target.GetComponent<Transform>().position.x < this.GetComponent<Transform>().position.x)
                    {
                        pos.x -= 0.05f;
                    }

                    if (target.GetComponent<Transform>().position.z > this.GetComponent<Transform>().position.z)
                    {
                        pos.z += 0.05f;
                    }
                    else if (target.GetComponent<Transform>().position.z < this.GetComponent<Transform>().position.z)
                    {
                        pos.z -= 0.05f;
                    }

                    transform.position = pos;

                    if (transform.position.x <= target.GetComponent<Transform>().position.x + 1f &&
                        transform.position.x >= target.GetComponent<Transform>().position.x - 1f &&
                        transform.position.z <= target.GetComponent<Transform>().position.z + 1f &&
                        transform.position.z >= target.GetComponent<Transform>().position.z - 1f)
                    {
                        timer2 -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier; ;
                        if (timer2 <= 0)
                        {
                            timer2 = attackInterval;

                            if (target.name.Contains("Kingdom"))
                            {
                                if (target.GetComponent<KingdomController>().health - dmg <= 0)
                                {
                                    parentKingdom.wood += (int)Mathf.Floor(Random.Range(10, 20));
                                    parentKingdom.wheat += (int)Mathf.Floor(Random.Range(10, 20));
                                    parentKingdom.stone += (int)Mathf.Floor(Random.Range(10, 20));
                                    parentKingdom.coal += (int)Mathf.Floor(Random.Range(10, 20));
                                    parentKingdom.iron += (int)Mathf.Floor(Random.Range(10, 20));
                                    parentKingdom.gold += (int)Mathf.Floor(Random.Range(15, 30));
                                }
                                target.GetComponent<KingdomController>().health -= dmg;
                            }
                            else
                            {
                                if (target.GetComponent<BuildingController>().health - dmg <= 0)
                                {
                                    for (int i = 0; i < target.GetComponent<BuildingController>().materials.Length; i++)
                                    {
                                        if (target.GetComponent<BuildingController>().materials[i] == "wood")
                                            parentKingdom.wood += (int)Mathf.Floor(Random.Range(1, 5));
                                        if (target.GetComponent<BuildingController>().materials[i] == "wheat")
                                            parentKingdom.wheat += (int)Mathf.Floor(Random.Range(1, 5));
                                        if (target.GetComponent<BuildingController>().materials[i] == "stone")
                                            parentKingdom.stone += (int)Mathf.Floor(Random.Range(1, 5));
                                        if (target.GetComponent<BuildingController>().materials[i] == "coal")
                                            parentKingdom.coal += (int)Mathf.Floor(Random.Range(1, 5));
                                        if (target.GetComponent<BuildingController>().materials[i] == "iron")
                                            parentKingdom.iron += (int)Mathf.Floor(Random.Range(1, 5));
                                        if (target.GetComponent<BuildingController>().materials[i] == "gold")
                                            parentKingdom.gold += (int)Mathf.Floor(Random.Range(1, 5));
                                    }
                                }
                                target.GetComponent<BuildingController>().health -= dmg;
                            }

                        }
                    }
                }
            }
        } else
        {
            if (transform.position == startPos)
            {
                this.gameObject.SetActive(false);
            } else
            {
                timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier; ;
                if (timer <= 0f) {
                    timer = timerInterval;
                    Vector3 pos = transform.position;
                    if (transform.position.x < startPos.x)
                        pos.x += 0.05f;
                    else if (transform.position.x > startPos.x)
                        pos.x -= 0.05f;

                    if (transform.position.z < startPos.z)
                        pos.z += 0.05f;
                    else if (transform.position.z > startPos.z)
                        pos.z -= 0.05f;
                    transform.position = pos;
                }
            }
        }
    }

    public void Attack(GameObject k)
    {

        // Določi se napadeno kraljestvo glede na vhodni argument k
        targetKingdom = k;

        // Status napadanja isAttacking se spremeni na true
        isAttacking = true;

        // Moč vojske se določi glede na število napadalnih vitezov
        dmg = GetComponentInParent<KingdomController>().attackKnights;

        // Začetna pozicija je pri starševskem kraljestvu
        startPos = transform.position;

        // Poiščejo se vse zgradbe, ki imajo značko enako kot njihovo kraljestvo
        buildings = GameObject.FindGameObjectsWithTag(k.gameObject.tag);

        /* Inicializira se spremenljivka za indeks zgradbe z najmanjšo
           oddaljenostjo od vojske (minB) in spremenljivka za to razdaljo (minDis) */
        float minDis = -1;
        minB = 0;

        for (int i = 0; i < buildings.Length; i++)
        {
            /* Izračuna se oddaljenost posamezne zgradbe, nato pa se primerja
               s prejšnjo najmanjšo oddaljenostjo. Tako se poišče zgradba,
               ki je najbližje vojski */
            if (i == 0 || minDis >= Mathf.Sqrt( (transform.position.x - buildings[i].GetComponent<Transform>().position.x)*
                (transform.position.x - buildings[i].GetComponent<Transform>().position.x) +
                (transform.position.z - buildings[i].GetComponent<Transform>().position.z) *
                (transform.position.z - buildings[i].GetComponent<Transform>().position.z))) {
                minDis = Mathf.Sqrt((transform.position.x - buildings[i].GetComponent<Transform>().position.x) *
                (transform.position.x - buildings[i].GetComponent<Transform>().position.x) +
                (transform.position.z - buildings[i].GetComponent<Transform>().position.z) *
                (transform.position.z - buildings[i].GetComponent<Transform>().position.z));
                minB = i;
            }
        } 

        // Tarča vojske postane zgradba, ki je najbližje
        target = buildings[minB];

        // Ponastavi se časovnik za napadanje
        timer = timerInterval;
    }

    void newTarget() {

            // Prejšnja tarča se izbriše iz tabele
            buildings[minB] = null;

            /* Deklarira in inicializira se spremenljivka ok, ki prekini napadanje,
               če funkcija ne najde nove tarče */
            bool ok = true;

            /* Inicializira se spremenljivka za indeks zgradbe z najmanjšo
               oddaljenostjo od vojske (minB) in spremenljivka za to razdaljo (minDis) */
            float minDis = -1;
            minB = 0;

            for (int i = 0; i < buildings.Length; i++) {
                if (buildings[i] != null) {

                    /* Izračuna se oddaljenost posamezne zgradbe, nato pa se primerja
                    s prejšnjo najmanjšo oddaljenostjo. Tako se poišče zgradba,
                    ki je najbližje vojski */
                    if ((ok == true) || minDis >= Mathf.Sqrt((transform.position.x - buildings[i].GetComponent<Transform>().position.x) *
                        (transform.position.x - buildings[i].GetComponent<Transform>().position.x) +
                        (transform.position.z - buildings[i].GetComponent<Transform>().position.z) *
                        (transform.position.z - buildings[i].GetComponent<Transform>().position.z))) {
                        minDis = Mathf.Sqrt((transform.position.x - buildings[i].GetComponent<Transform>().position.x) *
                        (transform.position.x - buildings[i].GetComponent<Transform>().position.x) +
                        (transform.position.z - buildings[i].GetComponent<Transform>().position.z) *
                        (transform.position.z - buildings[i].GetComponent<Transform>().position.z));
                        minB = i;
                    }
                    // Ker je v tabeli vsaj ena zgradba, se ok nastavi na false
                    ok = false;
                }
            }

            if (ok == true) {
                /* Če je ok true, kar pomeni, da v tabeli ni več zgradb,
                   se napadanje ustavi*/
                isAttacking = false;
                timer = timerInterval;
            } else {
                // Določi se nova tarča
                target = buildings[minB];

                // Ponastavi se časovnik za napadanje
                timer = timerInterval;
            }
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void Defend() {
        targetKingdom.GetComponent<KingdomController>().defenceArmy.SetActive(true);
       // Debug.Log(targetKingdom.GetComponent<KingdomController>().defenceArmy);
        targetKingdom.GetComponent<KingdomController>().defenceArmy.GetComponent<DefenceArmy>().Attack(this.gameObject);
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
