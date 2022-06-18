using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    public GameObject ship;
    public GameObject shipMenu;

    float resetTime = 10f;
    public float timer;

    public GameObject globalManager;

    int dir = 0;

    /* dir
     * 0 = down
     * 1 = up
     * 2 = left
     * 3 = right
     */


    // Start is called before the first frame update
    void Start()
    {

        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");

        ship = GameObject.Find(this.name + "/Ship");
        shipMenu = GameObject.Find(this.name + "Ship/Menu");
        ship.SetActive(false);
        resetTime = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= GameObject.Find("Map").GetComponent<Map>().ySize * 10 - 10
            && dir != 1)
        {
            // Debug.Log("Heyyy");
            if (dir == 0)
            {
                transform.Rotate(0f, 180f, 0f, Space.Self);
                ship.transform.Rotate(0f, -180f, 0f, Space.Self);
            }
            else if (dir == 2)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                ship.transform.Rotate(0f, -90f, 0f, Space.Self);
            }
            else if (dir == 3)
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
                ship.transform.Rotate(0f, 90f, 0f, Space.Self);
            }
            dir = 1;
        }

        if (transform.position.z <= 0 && dir != 0)
        {
            // Debug.Log("Heyyy");
            if (dir == 1)
            {
                transform.Rotate(0f, 180f, 0f, Space.Self);
                ship.transform.Rotate(0f, -180f, 0f, Space.Self);
            }
            else if (dir == 2)
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
                ship.transform.Rotate(0f, 90f, 0f, Space.Self);
            }
            else if (dir == 3)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                ship.transform.Rotate(0f, -90f, 0f, Space.Self);
            }
            dir = 0;
        }

        if (transform.position.x >= GameObject.Find("Map").GetComponent<Map>().xSize * 10 - 10
            && dir != 3)
        {
            // Debug.Log("Heyyy");
            if (dir == 0)
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
                ship.transform.Rotate(0f, 90f, 0f, Space.Self);
            }
            else if (dir == 1)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                ship.transform.Rotate(0f, -90f, 0f, Space.Self);
            }
            else if (dir == 2)
            {
                transform.Rotate(0f, 180f, 0f, Space.Self);
                ship.transform.Rotate(0f, -180f, 0f, Space.Self);
            }
            dir = 3;
        }

        if (transform.position.x <= 0 && dir != 2)
        {
            // Debug.Log("Heyyy");
            if (dir == 0)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
                ship.transform.Rotate(0f, -90f, 0f, Space.Self);
            }
            else if (dir == 1)
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
                ship.transform.Rotate(0f, 90f, 0f, Space.Self);
            }
            else if (dir == 3)
            {
                transform.Rotate(0f, 180f, 0f, Space.Self);
                ship.transform.Rotate(0f, -180f, 0f, Space.Self);
            }
            dir = 2;
        }

        timer -= 1f * Time.deltaTime * globalManager.GetComponent<GlobalManager>().timeMultiplier;
        if (timer <= 0)
        {
            timer = resetTime;
            if (Random.Range(1, 4) == 2)
            {
                ship.SetActive(true);
                ship.GetComponent<Trader>().ResetTrade();
            } else if (shipMenu.activeSelf == false)
            {
                ship.SetActive(false);
            }
        }
    }
}
