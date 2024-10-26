using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public GameObject Bullet;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Solid")
        {
            Destroy(Bullet);
        }
    }
}
