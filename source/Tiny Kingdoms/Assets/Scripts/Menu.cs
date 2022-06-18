using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public GameObject globalManager;

    public int id;

    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        Set();
    }

    // Update is called once per frame
    void Update()
    {
        if (id == 0 || globalManager == null)
            Set();

        if (id != globalManager.GetComponent<GlobalManager>().selectedMenu)
            this.gameObject.SetActive(false);

      //  transform.LookAt(cam, Vector3.down);
    }

    public void Set()
    {
        globalManager = GameObject.FindGameObjectWithTag("GlobalManager");
        id = globalManager.GetComponent<GlobalManager>().NewMenu();
    }
}
