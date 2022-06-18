using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public GameObject clickSound;
    public GameObject tradeSound;
    public GameObject bribeSound;
    public GameObject buildSound;
    public GameObject destroyBuildingSound;
    public GameObject destroyKingdomSound;
    public GameObject feedCitizensSound;
    public GameObject upgradeKingdomSound;

    string prev = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseOverUi())
        {
       //     Debug.Log(EventSystem.current.currentSelectedGameObject.name + " " + prev);
            // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            if (EventSystem.current.currentSelectedGameObject != null &&
                EventSystem.current.currentSelectedGameObject.TryGetComponent(typeof(Button), out Component component)
                && (EventSystem.current.currentSelectedGameObject.name != prev ||
                Input.GetMouseButtonDown(0)))
            {

                prev = EventSystem.current.currentSelectedGameObject.name;


                if (EventSystem.current.currentSelectedGameObject.name == "MakeTrade")
                {
                    PlaySound(2);
                } else if (EventSystem.current.currentSelectedGameObject.name == "Bribe")
                {
                    PlaySound(3);
                } else
                {
                    PlaySound(1);
                }


            }
        }
    }

    bool isMouseOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
       
    }

    public void PlaySound(int id)
    {
        switch (id)
        {
            case 1:
                clickSound.GetComponent<AudioSource>().Play();
                break;
            case 2:
                tradeSound.GetComponent<AudioSource>().Play();
                break;

            case 3:
                bribeSound.GetComponent<AudioSource>().Play();
                break;
            case 4:
                buildSound.GetComponent<AudioSource>().Play();
                break;
            case 5:
                destroyBuildingSound.GetComponent<AudioSource>().Play();
                break;
            case 6:
                destroyKingdomSound.GetComponent<AudioSource>().Play();
                break;
            case 7:
                feedCitizensSound.GetComponent<AudioSource>().Play();
                break;
            case 8:
                upgradeKingdomSound.GetComponent<AudioSource>().Play();
                break;
            default:
                break;
        }
    }
}
