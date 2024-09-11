using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
//Access Modifier, Data Type, Name
public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;
    Camera playerCam;

    Vector2 camRotation;

    public bool sprintMode = false;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpheight = 5.0f;

    [Header("User Settings")]
    public bool sprintToggleOption = false;
    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2.0f;
    public float Ysensitivity = 2.0f;
    public float camRotationLimit = 90f;
    public float groundDetectDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        playerCam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);

        playerCam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        Vector3 temp = myRB.velocity;

        if (!sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                sprintMode = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprintMode = false;
            print(sprintMode);
        }

        if (sprintToggleOption)
        {
            print(Input.GetAxisRaw("Vertical"));
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Vertical") > 0))
                sprintMode = true;

            if (Input.GetAxisRaw("Vertical") <= 0)
                sprintMode = false;
        }


        if (!sprintMode)
            temp.x = Input.GetAxisRaw("Vertical") * speed;

        if (sprintMode)
            temp.x = Input.GetAxisRaw("Vertical") * speed * sprintMultiplier;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            sprintMode = false;

        temp.z = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpheight;

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    }
}
