using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BarracksController : MonoBehaviour
{

    public int knights = 0;

    public float productionTime = 1f;
    float timer;

    public bool isTraining;

    public GameObject kingdom;

    public GameObject knightsDisplay;
    public GameObject trainDefenceButton;
    public GameObject trainAttackButton;
    public GameObject trainingButton;
    public GameObject menu;

    bool start = true;

    public GameObject globalManager;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        menu = GameObject.Find(this.name + "/Menu");

        knightsDisplay = GameObject.Find(this.name + "/Menu/Canvas/Knights");
        trainDefenceButton = GameObject.Find(this.name + "/Menu/Canvas/TrainDefence");
        trainAttackButton = GameObject.Find(this.name + "/Menu/Canvas/TrainAttack");
        trainingButton = GameObject.Find(this.name + "/Menu/Canvas/Training");

        if (this.TryGetComponent(typeof(PlayerBuildingController), out Component component))
        {

            kingdom = GameObject.FindGameObjectWithTag("0");
        }

        trainDefenceButton.GetComponent<Button>().onClick.AddListener(TrainDefence);
        trainAttackButton.GetComponent<Button>().onClick.AddListener(TrainAttack);
        trainingButton.GetComponent<Button>().onClick.AddListener(Training);

        isTraining = false;

        knightsDisplay.GetComponent<Text>().text = "0";

        // menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            start = false;
            menu.SetActive(false);
        }

        if (this.GetComponent<BuildingController>().placed == true && isTraining == true)
        {
            timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
            if (timer <= 0f)
            {
                timer = productionTime;
                if (kingdom != null)
                {
                    if (kingdom.GetComponent<KingdomController>().citizens > 0)
                    {
                        knights += 1;
                        kingdom.GetComponent<KingdomController>().citizens--;
                        knightsDisplay.GetComponent<Text>().text = "" + knights;
                    }
                }
            }
        }
    }

    public void TrainDefence()
    {
        if (knights > 0)
        {
            knights--;
            kingdom.GetComponent<KingdomController>().defenceKnights++;
            knightsDisplay.GetComponent<Text>().text = "" + knights;

        }
    }

    public void TrainAttack()
    {
        if (knights > 0)
        {
            knights--;
            kingdom.GetComponent<KingdomController>().attackKnights++;
            knightsDisplay.GetComponent<Text>().text = "" + knights;

        }
    }

    public void Training()
    {
        if (isTraining == true)
        {
            isTraining = false;
            GameObject.Find(this.name + "/Menu/Canvas/Training/Text").GetComponent<Text>().text = "Start Training";
        }
        else
        {
            isTraining = true;
            GameObject.Find(this.name + "/Menu/Canvas/Training/Text").GetComponent<Text>().text = "Stop Training";
        }
    }
}
