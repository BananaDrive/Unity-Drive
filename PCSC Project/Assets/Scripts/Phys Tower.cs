using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysTower : MonoBehaviour
{
    public GameObject PhysicsTower;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = PhysicsTower.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnCollisionEnter(Collision collision)
     {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody rb = PhysicsTower.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }

     }
}
