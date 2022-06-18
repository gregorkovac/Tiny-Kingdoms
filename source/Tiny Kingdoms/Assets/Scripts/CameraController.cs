using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float scrollSpeed = 100f;

    Vector2 panLimit;
    public float minScroll = 20f;
    public float maxScroll = 120f;

    GameObject map;

    public bool isometric;

    void Start()
    {
        map = GameObject.Find("Map");
        panLimit.x = map.GetComponent<Map>().xSize*10 + 50;
        panLimit.y = map.GetComponent<Map>().ySize*10 - 10;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;

        if (Input.GetKey("w")) {
            pos.z += panSpeed * Time.deltaTime;
            if (isometric == true)
                pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * Time.deltaTime;
            if (isometric == true)
                pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
            if (isometric == true)
                pos.z += panSpeed * Time.deltaTime; 
        }

        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
            if (isometric == true)
                pos.z -= panSpeed * Time.deltaTime; 
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (isometric == false)
        {
            pos.y += -scroll * scrollSpeed * Time.deltaTime;
        } else
        {
            GetComponent<Camera>().orthographicSize += -scroll * scrollSpeed * Time.deltaTime; 
        }
         
        pos.x = Mathf.Clamp(pos.x, -50, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -50, panLimit.y);
        pos.y = Mathf.Clamp(pos.y, minScroll, maxScroll);
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, 10, 30);

        transform.position = pos;
    }
}
