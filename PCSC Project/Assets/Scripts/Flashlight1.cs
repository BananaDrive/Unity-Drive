using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flashlight1 : MonoBehaviour
{
    public float Battery = 100;
    public int LightId = 0;
    public Component FlashLightSpot;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
         Destroy(gameObject);
        }
    }
}
