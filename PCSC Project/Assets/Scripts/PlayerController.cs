using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Access Modifier, Data Type, Name
public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;
    Camera playerCam;

    Vector2 camRotation;

    public Transform weaponSlot;

    public bool sprintMode = false;
    [Header("Player Stats")]
    public int maxHealth = 5;
    public int currentHealth = 5;
    public int healthRestore = 1;

    [Header("Weapon Stats")]
    public bool canFire = true;

    [Header("Movement Settings")]
    public float speed = 10.0f;
    public float sprintMultiplier = 2.5f;
    public float jumpheight = 5.0f;

    [Header("User Settings")]
    public bool sprintToggleBtn = true;
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
        }

        if (sprintToggleOption)
        {
            print(Input.GetAxisRaw("Vertical"));
            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxisRaw("Vertical") > 0))
                sprintMode = !sprintMode;

            if (Input.GetAxisRaw("Vertical") <= 0)
                sprintMode = false;
        }


        if (!sprintMode)
            temp.x = Input.GetAxisRaw("Vertical") * speed;

        if (sprintMode)
            temp.x = Input.GetAxisRaw("Vertical") * speed * sprintMultiplier;

        if (sprintToggleBtn)
            if (Input.GetKeyDown(KeyCode.Delete))
                sprintToggleOption = (sprintToggleOption ? false : true);
       
        temp.z = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, groundDetectDistance))
            temp.y = jumpheight;

        myRB.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((currentHealth < maxHealth) && collision.gameObject.tag == "healthPickUp")
        {
            currentHealth += healthRestore;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Weapon")
            collision.gameObject.transform.SetParent(weaponSlot);
    }
    IEnumerator cooldownFire(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}
