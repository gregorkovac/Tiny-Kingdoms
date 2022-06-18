using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int xSize = 10, ySize = 10;

    public int[,] mapArray;

    public GameObject forest;
    public GameObject plains;
    public GameObject mountains;
    public GameObject shore;
    public GameObject shoreCorner;

    bool built = false;

    // Start is called before the first frame update
    void Start()
    {
        /*     mapArray = new int[ySize, xSize];
             for (int i = 0; i < ySize; i++)
             {
                 for (int j = 0; j < xSize; j++)
                 {
                     if (i == 0 || i == ySize - 1 || j == 0 || j == xSize - 1)
                         mapArray[i, j] = 3;
                     else
                         mapArray[i, j] = Random.Range(0,3);
                 }
             } OLD RANDOM */

        if (built == false)
        {

            mapArray = new int[ySize, xSize];
            int r;
            int cnt;
            bool ok;

            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    mapArray[i, j] = 1;
                }
            }

            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    if (i == 0 || i == ySize - 1 || j == 0 || j == xSize - 1)
                    {
                        mapArray[i, j] = 3;
                    }
                    else
                    {
                        if (Random.Range(0, 3) == 1)
                        {
                            do
                            {
                                mapArray[i, j] = Random.Range(0, 3);
                            } while (mapArray[i, j] == 1);
                        }
                    }
                }
            }

            for (int k = 0; k < 5; k++)
            {
                for (int i = 0; i < ySize; i++)
                {
                    for (int j = 0; j < xSize; j++)
                    {
                        if (!(i == 0 || i == ySize - 1 || j == 0 || j == xSize - 1))
                        {
                            if (i - 1 > 0 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i - 1, j] = mapArray[i, j];
                            }
                            if (i + 1 < ySize - 1 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i + 1, j] = mapArray[i, j];
                            }
                            if (j - 1 > 0 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i, j - 1] = mapArray[i, j];
                            }
                            if (j + 1 < xSize - 1 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i, j + 1] = mapArray[i, j];
                            }
                            if (i - 1 > 0 && j + 1 < xSize - 1 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i - 1, j + 1] = mapArray[i, j];
                            }
                            if (i - 1 > 0 && j - 1 > 0 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i - 1, j - 1] = mapArray[i, j];
                            }
                            if (i + 1 < ySize - 1 && j + 1 < xSize - 1 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i + 1, j + 1] = mapArray[i, j];
                            }
                            if (i + 1 < ySize - 1 && j - 1 > 0 && Random.Range(0, 5) == 1)
                            {
                                mapArray[i + 1, j - 1] = mapArray[i, j];
                            }
                        }
                    }
                }
            }

            for (int k = 1; k <= 2; k++)
            {
                cnt = 0;
                for (int i = 0; i < ySize; i++)
                {
                    for (int j = 0; j < xSize; j++)
                    {
                        if (mapArray[i, j] == k)
                            cnt++;
                    }

                    if (cnt < xSize)
                    {
                        for (int l = 0; l < xSize; l++)
                        {
                            mapArray[Random.Range(1, xSize - 2), Random.Range(1, ySize - 2)] = k;
                        }
                    }
                }
            }

            /*
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    if (i == 0 || i == ySize - 1 || j == 0 || j == xSize - 1)
                    {

                    }
                    else
                    {
                        do
                        {
                            r = Random.Range(0, 3);
                            cnt = 0;
                            ok = false;
                            if (mapArray[i - 1, j] == r)
                                cnt++;
                            if (mapArray[i + 1, j] == r)
                                cnt++;
                            if (mapArray[i, j + 1] == r)
                                cnt++;
                            if (mapArray[i, j - 1] == r)
                                cnt++;
                            if (mapArray[i + 1, j + 1] == r)
                                cnt++;
                            if (mapArray[i + 1, j - 1] == r)
                                cnt++;
                            if (mapArray[i - 1, j + 1] == r)
                                cnt++;
                            if (mapArray[i - 1, j - 1] == r)
                                cnt++;
                            if (Random.Range(0, 5) == 1 || (cnt >= 4))
                            {
                                mapArray[i, j] = r;
                                ok = true;

                            }
                        } while (ok != true);
                    }
                }
            }
            */

            Vector3 pos;

            pos.x = 0;
            pos.z = 0;
            pos.y = 0;

            for (int i = 0; i < ySize; i++)
            {
                pos.z = i * 10;
                for (int j = 0; j < xSize; j++)
                {
                    pos.x = j * 10;

                    if (mapArray[i, j] == 0)
                    {
                        GameObject tmp = Instantiate<GameObject>(forest, pos, Quaternion.identity);
                        tmp.transform.SetParent(transform);
                    }
                    else if (mapArray[i, j] == 1)
                    {
                        GameObject tmp = Instantiate<GameObject>(plains, pos, Quaternion.identity);
                        tmp.transform.SetParent(transform);
                    }
                    else if (mapArray[i, j] == 2)
                    {
                        GameObject tmp = Instantiate<GameObject>(mountains, pos, Quaternion.identity);
                        tmp.transform.SetParent(transform);
                    }
                    else if (mapArray[i, j] == 3)
                    {
                        GameObject tmp;
                        if ((i == 0 && j == 0) || (i == 0 && j == xSize - 1)
                            || (i == ySize - 1 && j == 0) || (i == ySize - 1 && j == xSize - 1))
                        {
                            tmp = Instantiate<GameObject>(shoreCorner, pos, Quaternion.identity);
                            tmp.transform.SetParent(transform);

                            if (i == 0 && j == xSize - 1)
                            {
                                tmp.transform.Rotate(0f, -90f, 0f, Space.Self);
                            }

                            if (i == ySize - 1 && j == 0)
                            {
                                tmp.transform.Rotate(0f, 90f, 0f, Space.Self);
                            }

                            if (i == ySize - 1 && j == xSize - 1)
                            {
                                tmp.transform.Rotate(0f, 180f, 0f, Space.Self);
                            }
                        }
                        else
                        {
                            tmp = Instantiate<GameObject>(shore, pos, Quaternion.identity);


                            tmp.transform.SetParent(transform);
                            if (i >= ySize - 1)
                            {
                                tmp.transform.Rotate(0f, 180f, 0f, Space.Self);
                            }
                            if (j <= 0)
                            {
                                tmp.transform.Rotate(0f, 90f, 0f, Space.Self);
                            }
                            if (j >= xSize - 1)
                            {
                                tmp.transform.Rotate(0f, -90f, 0f, Space.Self);
                            }
                        }

                    }
                }
            }
            built = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getTile(int x, int y)
    {
        if (x >= 0 && x+1 < xSize && y >= 0 && y+1 < ySize)
        {
            //if (mapArray[y + 1, x + 1] < 0)
            //    return mapArray[y, x];
            return mapArray[y, x];
        }
        return -1;
    }

    public void buildingPlaced(int x, int y, int bClass)
    {
        /* Debug.Log("Building placed on " + mapArray[y+1, x]);
         Debug.Log("X: " + x + "; Y: " + y+1);
         Debug.Log("========="); */
        if (x >= 0 && x < xSize && y >= 0 && y < ySize)
        {
            mapArray[y, x] = bClass;
        }
    }

    public void reloadMap()
    {

        built = true;
        GameObject[] del = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < del.Length; i++)
        {
            Destroy(del[i]);
        }

        Vector3 pos;

        pos.x = 0;
        pos.z = 0;
        pos.y = 0;

        for (int i = 0; i < ySize; i++)
        {
            pos.z = i * 10;
            for (int j = 0; j < xSize; j++)
            {
                pos.x = j * 10;

                if (mapArray[i, j] == 0 || mapArray[i, j] == -2)
                {
                    GameObject tmp = Instantiate<GameObject>(forest, pos, Quaternion.identity);
                    tmp.transform.SetParent(transform);
                }
                else if (mapArray[i, j] == 1 || mapArray[i, j] == -1)
                {
                    GameObject tmp = Instantiate<GameObject>(plains, pos, Quaternion.identity);
                    tmp.transform.SetParent(transform);
                }
                else if (mapArray[i, j] == 2 || mapArray[i, j] == -3)
                {
                    GameObject tmp = Instantiate<GameObject>(mountains, pos, Quaternion.identity);
                    tmp.transform.SetParent(transform);
                }
                else if (mapArray[i, j] == 3 || mapArray[i, j] == -4)
                {
                    GameObject tmp;
                    if ((i == 0 && j == 0) || (i == 0 && j == xSize - 1)
                        || (i == ySize - 1 && j == 0) || (i == ySize - 1 && j == xSize - 1))
                    {
                        tmp = Instantiate<GameObject>(shoreCorner, pos, Quaternion.identity);
                        tmp.transform.SetParent(transform);

                        if (i == 0 && j == xSize - 1)
                        {
                            tmp.transform.Rotate(0f, -90f, 0f, Space.Self);
                        }

                        if (i == ySize - 1 && j == 0)
                        {
                            tmp.transform.Rotate(0f, 90f, 0f, Space.Self);
                        }

                        if (i == ySize - 1 && j == xSize - 1)
                        {
                            tmp.transform.Rotate(0f, 180f, 0f, Space.Self);
                        }
                    }
                    else
                    {
                        tmp = Instantiate<GameObject>(shore, pos, Quaternion.identity);


                        tmp.transform.SetParent(transform);
                        if (i >= ySize - 1)
                        {
                            tmp.transform.Rotate(0f, 180f, 0f, Space.Self);
                        }
                        if (j <= 0)
                        {
                            tmp.transform.Rotate(0f, 90f, 0f, Space.Self);
                        }
                        if (j >= xSize - 1)
                        {
                            tmp.transform.Rotate(0f, -90f, 0f, Space.Self);
                        }
                    }
                } else
                {
                    GameObject tmp = Instantiate<GameObject>(plains, pos, Quaternion.identity);
                    tmp.transform.SetParent(transform);
                }
            }
        }
    }
}
