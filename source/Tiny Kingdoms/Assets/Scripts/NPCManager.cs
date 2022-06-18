using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    GameObject map;

    public GameObject kingdom;
    public GameObject genericBuilding;
    public GameObject farm;
    public GameObject mine;
    public GameObject cottage;
    public GameObject barracks;

    GameObject kingdoms;
    GameObject[] karray;

    bool start = true;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map");

        Place:
            for (int i = 1; i < 3; i++)
            {
                do
                {
                    // Klic funkcije za postavljanje kraljestva
                    PlaceKingdom(i);

                    // Preverjanje, če je to kraljestvo bilo postavljeno
                    kingdoms = GameObject.FindGameObjectWithTag("" + i);

                } while (kingdoms == null); // Ponavljanje, dokler se kraljestvo ne postavi
            }

        int ind = 0;
        karray = FindObjectsOfType<GameObject>();
        for (int i = 0; i < karray.Length; i++)
        {
            if (!karray[i].gameObject.name.Contains("NPCKingdom"))
            {
                karray[i] = null;
            }
            else
            {
                karray[ind] = karray[i];
                ind++;
            }
        }
        System.Array.Resize<GameObject>(ref karray, ind);

        if (karray.Length <= 0)
            goto Place;


    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceKingdom(int id)
    {
        float x, z;
        int cnt = 0;

        // Iskanje naključne ploščice, na katero se lahko postavi kraljestvo
        do
        {
            x = Mathf.Floor(Random.Range(0, map.GetComponent<Map>().xSize));
            z = Mathf.Floor(Random.Range(0, map.GetComponent<Map>().xSize));
            cnt++;

            /* Zanko prekinem po 500 ponovitvah, ker ne želim, da bi se zaradi
             tega upočasnila igra */
            if (cnt >= 500) 
                break;
        /* Funkcija getTile(x, z) vrne tip ploščice na koordinatah (x, z).
         Kraljestvo lahko postavimo le na travniku (tip 1) */
        } while (map.GetComponent<Map>().getTile((int)(x), (int)(z)) != 1);

        if (cnt < 500)
        {
            /* Funkcija buildingPlaced(x, z, id) na mapi označi tip postavljene
             zgradbe na koordinatah (x, z). S tem preprečim postavljanje več
             zgradb na eno ploščico */
            map.GetComponent<Map>().buildingPlaced((int)(x),
                (int)(z), -1);

            /* Instantiate<GameObject> ustvari nov objekt iz spremenljivke
             kingdom tipa GameObject */
            GameObject tmp = Instantiate<GameObject>(kingdom, new Vector3(x * 10, 2.5f, z * 10), Quaternion.identity);

            // Za lažje delo v drugih skriptah vsakemu kraljestvu dodelim id številko 
            tmp.GetComponent<NPCKingdomController>().id = id;

            // Ime kraljestvu priredim glede na id številko
            tmp.name = tmp.name + id;

            // Kraljestvu priredim tudi značko glede na id številko
            tmp.gameObject.tag = ""+id;

            for (int i = 0; i < 5; i++)
            {
                // Vsako kraljestvo začne z najeveč 5 naključnimi zgradbami
                PlaceBuilding(i, x, z, id, tmp, (int)Mathf.Floor(Random.Range(0, 3)));
            }
        }

    }

    /* Razlaga argumentov funkcije:
       id = unikatna id številka zgradbe
       kx = x koordinata kraljestva
       kz = z koordinata kraljestva
       kid = id številka kraljestva
       kdm = objekt kraljestva
       bcl = številka, po kateri se generira tip zgradbe
     */
    public void PlaceBuilding(int id, float kx, float kz, int kid, GameObject kdm, int bcl)
    {
        float x=0, z=0;
        int cnt = 0;

        // Maksimalna oddaljenost zgradbe
        float rng = kdm.GetComponent<KingdomController>().range/10f;
        do
        {
            // Generiranje naključnih koordinat zgradbe
            x = Mathf.Floor(Random.Range(kx-rng, kx+rng)); 
            if (x < 0)
                x = 1;
            if (x > map.GetComponent<Map>().xSize)
                x = map.GetComponent<Map>().xSize - 1;

                z = Mathf.Floor(Random.Range(kz-rng, kz+rng));
            if (z < 0)
                z = 1;
            if (z > map.GetComponent<Map>().xSize)
                z = map.GetComponent<Map>().xSize - 1;

            cnt++;
            // Kot pri kraljestvu tudi tukaj ustavim zanko, če se ponovi 500-krat
            if (cnt >= 500)
                break;
         // Preverjanje, ali je ploščica na koordinatah ustrezna
        } while (map.GetComponent<Map>().getTile((int)(x), (int)(z)) != bcl);

        if (cnt < 500) {
            GameObject tmp;
            // Switch stavek za generiranje ustrezne zgradbe
            switch(bcl)
            {
                case 0:
                    /// Ustvari se nova koča
                    tmp = Instantiate<GameObject>(cottage, new Vector3(x * 10, 2.5f, z * 10), Quaternion.identity);
                    /* Pokliče se že prej razložena funkcija buildingPlaced(x, z, id),
                      enako velja tudi za ostale zgradbe */
                    map.GetComponent<Map>().buildingPlaced((int)(x), (int)(z), -2);
                    break;
                case 1:
                    // Ustvari se farma
                    tmp = Instantiate<GameObject>(farm, new Vector3(x * 10, 2.5f, z * 10), Quaternion.identity);
                    map.GetComponent<Map>().buildingPlaced((int)(x), (int)(z), -1);
                    break;
                case 2:
                    // Ustvari se rudnik
                    tmp = Instantiate<GameObject>(mine, new Vector3(x * 10, 2.5f, z * 10), Quaternion.identity);
                    map.GetComponent<Map>().buildingPlaced((int)(x), (int)(z), -3);
                    break;
                default:
                    /* V primeri, da spremenljivka bcl nekako ne ustreza nobeni
                       zgradbi, se generira generična zgradba */
                    tmp = Instantiate<GameObject>(genericBuilding, new Vector3(x * 10, 2.5f, z * 10), Quaternion.identity);
                    map.GetComponent<Map>().buildingPlaced((int)(x), (int)(z), -1);
                    break;
            }

            // Zgradbi se priredi unikatna številka
            tmp.GetComponent<BuildingController>().id = id;

            // Zgradbi se priredi značka, ki je enaka id številki kraljestva
            tmp.gameObject.tag = ""+kid;

            // Zgradbi se priredi starševsko kraljestvo
            tmp.GetComponent<BuildingController>().kingdom = kdm;

            // Zgradbi se priredi ime glede na id kraljestva in id zgradbe
            tmp.name = tmp.name + kid + id;

            /* Spremenljivka ids se uporablja za določanje id številke nove
              zgradbe. Vsakič, ko se le-ta ustvari, se ids poveča. */
            kdm.GetComponent<KingdomController>().ids++;

            // Poveča se maksimalna oddaljenost zgradbe od kraljestva
            kdm.GetComponent<KingdomController>().range += 1f;
        }
    }

    public void PlaceBuildingLoad(Vector3 pos, int id, int buildingClass, string tag, int lvl)
    {
        
        pos.x = pos.x / 10;
        pos.y = pos.y / 10;
        pos.z = pos.z / 10;
        GameObject tmp;
        map.GetComponent<Map>().buildingPlaced((int)(pos.x),
            (int)(pos.z), buildingClass);
        switch (buildingClass)
        {
            
            case -2:
                tmp = Instantiate<GameObject>(cottage, new Vector3(pos.x * 10, 2.5f, pos.z * 10), Quaternion.identity);
                break;
            case -1:
                tmp = Instantiate<GameObject>(farm, new Vector3(pos.x * 10, 2.5f, pos.z * 10), Quaternion.identity);
                break;
            case -3:
                tmp = Instantiate<GameObject>(mine, new Vector3(pos.x * 10, 2.5f, pos.z * 10), Quaternion.identity);
                break;
            default:
                tmp = Instantiate<GameObject>(barracks, new Vector3(pos.x * 10, 2.5f, pos.z * 10), Quaternion.identity);
                break;
        }

        tmp.GetComponent<BuildingController>().id = id;
        tmp.gameObject.tag = tag;
        //   tmp.GetComponent<NPCController>().kingdomId = kid;
       
        tmp.GetComponent<BuildingController>().kingdom = GameObject.Find("NPCKingdom(Clone)"+tag);
        tmp.name = tmp.name + tag + id;
        tmp.GetComponent<BuildingController>().kingdom.GetComponent<KingdomController>().ids++;
        tmp.GetComponent<BuildingController>().kingdom.GetComponent<KingdomController>().range += 1f;
        tmp.GetComponent<BuildingController>().level = lvl;
/*
        if (tmp.TryGetComponent(typeof(PlayerBuildingController), out Component component))
        {
            tmp.GetComponent<PlayerBuildingController>().prevHit = new Vector3(pos.x*10, 2.5f, pos.z*10);
            tmp.GetComponent<PlayerBuildingController>().targetPos = new Vector3(pos.x * 10, 2.5f, pos.z * 10);
            tmp.GetComponent<PlayerBuildingController>().placed = true;
        }
        */
    }
}
