using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Access Modifier, Data Type, Name
public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;

    public float speed = 10.0f;
    public float jumpheight = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = myRB.velocity;

        temp.x = Input.GetAxisRaw("Vertical") * speed;
        temp.z = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.Space))
            temp.y = jumpheight;

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    }
}
