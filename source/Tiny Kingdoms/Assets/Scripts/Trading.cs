using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trading : MonoBehaviour
{
    /*
    public float playerLevel = 0;
    public float NPCLevel = 0;

    public float valueWood;
    public float valueStone;
    public float valueWheat;
    public float valueCoal;
    public float valueIron;
    public float valueGold;

    public int pWood;
    public int pWheat;
    public int pStone;
    public int pIron;
    public int pCoal;
    public int pGold;

    public int NPCWood;
    public int NPCWheat;
    public int NPCStone;
    public int NPCIron;
    public int NPCCoal;
    public int NPCGold;

    public GameObject addWood;
    public GameObject removeWood;
    public GameObject woodAmount;

    public GameObject addWheat;
    public GameObject removeWheat;
    public GameObject wheatAmount;

    public GameObject addStone;
    public GameObject removeStone;
    public GameObject stoneAmount;

    public GameObject addCoal;
    public GameObject removeCoal;
    public GameObject coalAmount;

    public GameObject addIron;
    public GameObject removeIron;
    public GameObject ironAmount;

    public GameObject addGold;
    public GameObject removeGold;
    public GameObject goldAmount;

    public GameObject NPCwoodAmount;
    public GameObject NPCwheatAmount;
    public GameObject NPCstoneAmount;
    public GameObject NPCcoalAmount;
    public GameObject NPCironAmount;
    public GameObject NPCgoldAmount;

    public GameObject makeTradeButton;
    public GameObject bribeButton;

    public GameObject playerKingdom;

    float resetTime = 10f;
    public float timer;

    */

    // Start is called before the first frame update
    void Start()
    {
        /*
        resetTime = 20f;

        playerKingdom = GameObject.Find("Kingdom");

        this.GetComponent<KingdomController>().menu.SetActive(true);
        this.GetComponent<Trader>().tradeMenu.SetActive(true);

        addWood = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/AddWood");
        addWood.GetComponent<Button>().onClick.AddListener(() => Wood(1));
        removeWood = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/RemoveWood");
        removeWood.GetComponent<Button>().onClick.AddListener(() => Wood(-1));
        woodAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/WoodAmount");

        addWheat = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/AddWheat");
        addWheat.GetComponent<Button>().onClick.AddListener(() => Wheat(1));
        removeWheat = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/RemoveWheat");
        removeWheat.GetComponent<Button>().onClick.AddListener(() => Wheat(-1));
        wheatAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/WheatAmount");

        addStone = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/AddStone");
        addStone.GetComponent<Button>().onClick.AddListener(() => Stone(1));
        removeStone = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/RemoveStone");
        removeStone.GetComponent<Button>().onClick.AddListener(() => Stone(-1));
        stoneAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/StoneAmount");

        addCoal = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/AddCoal");
        addCoal.GetComponent<Button>().onClick.AddListener(() => Coal(1));
        removeCoal = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/RemoveCoal");
        removeCoal.GetComponent<Button>().onClick.AddListener(() => Coal(-1));
        coalAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/CoalAmount");

        addIron = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/AddIron");
        addIron.GetComponent<Button>().onClick.AddListener(() => Iron(1));
        removeIron = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/RemoveIron");
        removeIron.GetComponent<Button>().onClick.AddListener(() => Iron(-1));
        ironAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/IronAmount");

        addGold = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/AddGold");
        addGold.GetComponent<Button>().onClick.AddListener(() => Gold(1));
        removeGold = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/RemoveGold");
        removeGold.GetComponent<Button>().onClick.AddListener(() => Gold(-1));
        goldAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/GoldAmount");

        NPCwoodAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/NPCWoodAmount");
        NPCwheatAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/NPCWheatAmount");
        NPCstoneAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/NPCStoneAmount");
        NPCcoalAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/NPCCoalAmount");
        NPCironAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/NPCIronAmount");
        NPCgoldAmount = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/NPCGoldAmount");

        makeTradeButton = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/MakeTrade");
        makeTradeButton.GetComponent<Button>().onClick.AddListener(() => MakeTrade());

        bribeButton = GameObject.Find(this.name + "/Menu/Canvas/TradeMenu/Bribe");
        bribeButton.GetComponent<Button>().onClick.AddListener(() => Bribe());

        this.GetComponent<Trader>().tradeMenu.SetActive(false);
        this.GetComponent<KingdomController>().menu.SetActive(false);

        ResetTrade();
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (playerLevel < NPCLevel)
        {
            makeTradeButton.GetComponent<Button>().interactable = false;
        } else if (this.GetComponent<NPCKingdomController>().relationship > 0 && playerLevel >= NPCLevel)
        {
            makeTradeButton.GetComponent<Button>().interactable = true;
        }

        if (pGold > 0)
        {
            bribeButton.GetComponent<Button>().interactable = true;
        } else
        {
            bribeButton.GetComponent<Button>().interactable = false;
        }

        timer -= 1f * Time.deltaTime;
        if (timer <= 0)
        {
            timer = resetTime;
            if (pWheat == 0 && pWood == 0 && pStone == 0 && pIron == 0 && pCoal == 0 && pGold == 0)
                ResetTrade();
        }
        */
    }
    /*
    void ResetTrade ()
    {
        playerLevel = 0;
        NPCLevel = 0;

        pWheat = 0;
        pWood = 0;
        pStone = 0;
        pIron = 0;
        pCoal = 0;
        pGold = 0;

        if (this.GetComponent<KingdomController>().wheat >= 20)
        {
            NPCWheat = (int)Mathf.Floor(Random.Range(0, 10));
            valueWheat = (float)1/this.GetComponent<KingdomController>().wheat * (this.GetComponent<NPCKingdomController>().relationship / 10);
            NPCLevel += NPCWheat;
        } else
        {
            valueWheat = (20-this.GetComponent<KingdomController>().wheat) * (this.GetComponent<NPCKingdomController>().relationship / 10);
        }

        if (this.GetComponent<KingdomController>().wood >= 20)
        {
            NPCWood = (int)Mathf.Floor(Random.Range(0, 10));
            valueWood = (float)1 / this.GetComponent<KingdomController>().wood * (this.GetComponent<NPCKingdomController>().relationship / 10);
            NPCLevel += NPCWood;
        }
        else
        {
            valueWood = (20 - this.GetComponent<KingdomController>().wood) * (this.GetComponent<NPCKingdomController>().relationship / 10);
        }

        if (this.GetComponent<KingdomController>().stone >= 20)
        {
            NPCStone = (int)Mathf.Floor(Random.Range(0, 10));
            valueStone = (float)1 / this.GetComponent<KingdomController>().stone * (this.GetComponent<NPCKingdomController>().relationship / 10);
            NPCLevel += NPCStone;
        }
        else
        {
            valueStone = (20 - this.GetComponent<KingdomController>().stone) * (this.GetComponent<NPCKingdomController>().relationship / 10);
        }

        if (this.GetComponent<KingdomController>().coal >= 20)
        {
            NPCCoal = (int)Mathf.Floor(Random.Range(0, 10));
            valueCoal = (float)1 / this.GetComponent<KingdomController>().coal * (this.GetComponent<NPCKingdomController>().relationship / 10);
            NPCLevel += NPCCoal;
        }
        else
        {
            valueCoal = (20 - this.GetComponent<KingdomController>().coal) * (this.GetComponent<NPCKingdomController>().relationship / 10);
        }

        if (this.GetComponent<KingdomController>().iron >= 20)
        {
            NPCIron = (int)Mathf.Floor(Random.Range(0, 10));
            valueIron = (float)1 / this.GetComponent<KingdomController>().iron * (this.GetComponent<NPCKingdomController>().relationship / 10);
            NPCLevel += NPCIron;
        }
        else
        {
            valueIron = (20 - this.GetComponent<KingdomController>().iron) * (this.GetComponent<NPCKingdomController>().relationship / 10);
        }

        if (this.GetComponent<KingdomController>().gold >= 20)
        {
            NPCGold = (int)Mathf.Floor(Random.Range(0, 10));
            valueGold = (float)2 *1/ this.GetComponent<KingdomController>().gold * (this.GetComponent<NPCKingdomController>().relationship / 10);
            NPCLevel += NPCGold;
        }
        else
        {
            valueGold = 2*(20 - this.GetComponent<KingdomController>().gold) * (this.GetComponent<NPCKingdomController>().relationship / 10);
        }

        woodAmount.GetComponent<Text>().text = "" + pWood;
        wheatAmount.GetComponent<Text>().text = "" + pWheat;
        stoneAmount.GetComponent<Text>().text = "" + pStone;
        coalAmount.GetComponent<Text>().text = "" + pCoal;
        ironAmount.GetComponent<Text>().text = "" + pIron;
        goldAmount.GetComponent<Text>().text = "" + pGold;

        NPCwoodAmount.GetComponent<Text>().text = "" + NPCWood;
        NPCwheatAmount.GetComponent<Text>().text = "" + NPCWheat;
        NPCstoneAmount.GetComponent<Text>().text = "" + NPCStone;
        NPCcoalAmount.GetComponent<Text>().text = "" + NPCCoal;
        NPCironAmount.GetComponent<Text>().text = "" + NPCIron;
        NPCgoldAmount.GetComponent<Text>().text = "" + NPCGold;
    }

    public void Wood(int pm)
    {
        if (pWood + pm >= 0 && playerKingdom.GetComponent<KingdomController>().wood - pm >= 0)
        {
            pWood += pm;
            playerKingdom.GetComponent<KingdomController>().wood -= pm;
            woodAmount.GetComponent<Text>().text = "" + pWood;
            playerLevel += pm * valueWood;
        }
    }

    public void Wheat(int pm)
    {
        if (pWheat + pm >= 0 && playerKingdom.GetComponent<KingdomController>().wheat - pm >= 0)
        {
            pWheat += pm;
            playerKingdom.GetComponent<KingdomController>().wheat -= pm;
            wheatAmount.GetComponent<Text>().text = "" + pWheat;
            playerLevel += pm * valueWheat;
        }
    }

    public void Stone(int pm)
    {
        if (pStone + pm >= 0 && playerKingdom.GetComponent<KingdomController>().stone - pm >= 0)
        {
            pStone += pm;
            playerKingdom.GetComponent<KingdomController>().stone -= pm;
            stoneAmount.GetComponent<Text>().text = "" + pStone;
            playerLevel += pm * valueStone;
        }
    }

    public void Iron(int pm)
    {
        if (pIron + pm >= 0 && playerKingdom.GetComponent<KingdomController>().iron - pm >= 0)
        {
            pIron += pm;
            playerKingdom.GetComponent<KingdomController>().iron -= pm;
            ironAmount.GetComponent<Text>().text = "" + pIron;
            playerLevel += pm * valueIron;
        }
    }

    public void Coal(int pm)
    {
        if (pCoal + pm >= 0 && playerKingdom.GetComponent<KingdomController>().coal - pm >= 0)
        {
            pCoal += pm;
            playerKingdom.GetComponent<KingdomController>().coal -= pm;
            coalAmount.GetComponent<Text>().text = "" + pCoal;
            playerLevel += pm * valueCoal;
        }
    }

    public void Gold(int pm)
    {
        if (pGold + pm >= 0 && playerKingdom.GetComponent<KingdomController>().gold - pm >= 0)
        {
            pGold += pm;
            playerKingdom.GetComponent<KingdomController>().gold -= pm;
            goldAmount.GetComponent<Text>().text = "" + pGold;
            playerLevel += pm * valueGold;
        }
    }

    public void MakeTrade()
    {
        this.GetComponent<KingdomController>().wood += pWood;
        this.GetComponent<KingdomController>().wheat += pWheat;
        this.GetComponent<KingdomController>().stone += pStone;
        this.GetComponent<KingdomController>().coal += pCoal;
        this.GetComponent<KingdomController>().iron += pIron;
        this.GetComponent<KingdomController>().gold += pGold;

        playerKingdom.GetComponent<KingdomController>().wood += NPCWood;
        playerKingdom.GetComponent<KingdomController>().wheat += NPCWheat;
        playerKingdom.GetComponent<KingdomController>().stone += NPCStone;
        playerKingdom.GetComponent<KingdomController>().coal += NPCCoal;
        playerKingdom.GetComponent<KingdomController>().iron += NPCIron;
        playerKingdom.GetComponent<KingdomController>().gold += NPCGold;

        ResetTrade();
        timer = resetTime;
    }

    public void Bribe()
    {
        this.GetComponent<KingdomController>().gold += pGold;
        this.GetComponent<NPCKingdomController>().relationship += pGold*(int)Mathf.Floor(Random.Range(1, 5));

        this.GetComponent<NPCKingdomController>().relationship = Mathf.Clamp(this.GetComponent<NPCKingdomController>().relationship, -50, 50);

        this.GetComponent<NPCKingdomController>().relDisplay.GetComponent<Text>().text = "" + this.GetComponent<NPCKingdomController>().relationship;
        /*     pGold = 0;
             goldAmount.GetComponent<Text>().text = "" + pGold; */
      //  ResetTrade();
  //  }

}
