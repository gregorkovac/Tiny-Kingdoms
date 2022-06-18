using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmWings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().Rotate(Vector3.right * 50.0f * Time.deltaTime);
    }
}
