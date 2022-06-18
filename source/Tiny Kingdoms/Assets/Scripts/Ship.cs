using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find(this.name + "/Menu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
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
