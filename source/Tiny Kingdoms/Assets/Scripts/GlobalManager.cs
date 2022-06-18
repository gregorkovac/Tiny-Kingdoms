using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalManager : MonoBehaviour
{

    public int selectedMenu;

    int ids = 0;

    public float timeMultiplier = 1f;

    RaycastHit hit;
    Ray ray;

    public GameObject menuPanel;

    public Material dmgMaterial;

    float waitForMenu;

    // Start is called before the first frame update
    void Start()
    {
        selectedMenu = -1;
        ids = 1;
    }

    // Update is called once per frame
    void Update()
    {

        float t = Mathf.PingPong(Time.time, .5f) / .5f;
        Color col = dmgMaterial.color;
        col.a = t;
        dmgMaterial.color = col;
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if ((hit.collider.tag == "Tile" || hit.collider.tag == "Untagged") && !isMouseOverUi())
                {
                    selectedMenu = -1;
                }
            }
        }
       
    }

    public int NewMenu()
    {
        ids++;
        return ids;
    }

    bool isMouseOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();

    }
}
