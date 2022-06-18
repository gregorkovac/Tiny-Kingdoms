using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class RequirementMenu : MonoBehaviour
{

    public GameObject menu;
    public GameObject okayreqText;
    public GameObject missingreqText;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        menu.GetComponent<Transform>().transform.position = new Vector2(Input.mousePosition.x + 320, Input.mousePosition.y);
    }

    public void Display(string okaytext, string missingtext)
    {
        menu.SetActive(true);
        okayreqText.GetComponent<Text>().text = okaytext;
        missingreqText.GetComponent<Text>().text = missingtext;
    }

    public void Hide()
    {
        menu.SetActive(false);
    }
}
