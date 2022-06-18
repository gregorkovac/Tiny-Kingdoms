using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceParticle : MonoBehaviour
{
    public GameObject particles;

    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
      /*  if (Input.GetMouseButton(0))
            PlayParticles(); */
    }

    public void PlayParticles(float x = 0, float z = 0)
    {
        particles.GetComponent<ParticleSystem>().Play();
        particles.transform.position = new Vector3(x, 2f, z);
    }
}
