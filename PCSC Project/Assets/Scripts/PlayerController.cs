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
    public bool flashlight = false;

    [Header("JetPack Stats")]
    public bool JetPackOn = false;
    public float JetPackPower = 1.5f;

    [Header("Weapon Stats")]
    public int weaponId = 0;
    public int fireMode = 0;
    public float fireRate = 0;
    public float maxAmmo = 0;
    public float currentAmmo = 0;
    public float currentClip = 0;
    public float clipSize = 0;
    public float reloadAmount = 0;
    public bool canFire = true;

    [Header("projectiles")]
    public GameObject bullet;
    public float bulletSpeed = 0;
    public float bulletLifeSpan = 0;

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

        if(Input.GetMouseButton(0) && canFire && currentClip > 0 && weaponId >= 1)
        {
            GameObject b = Instantiate(bullet, weaponSlot.position, weaponSlot.rotation);
            b.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * bulletSpeed);
            Destroy(b, bulletLifeSpan);

            canFire = false;
            currentClip--;
            StartCoroutine("cooldownFire");
        }

        Vector3 temp = myRB.velocity;

        if (!sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                sprintMode = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprintMode = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
            reloadClip();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            other.gameObject.transform.position = weaponSlot.position;
            other.gameObject.transform.SetParent(weaponSlot);

            switch(other.gameObject.name)
            { 
                case "Weapon 1":

                    weaponId = 1;
                    fireMode = 0;
                    fireRate = 1f;
                    maxAmmo = 5000;
                    currentAmmo = 5000;
                    currentClip = 100;
                    clipSize = 100;
                    reloadAmount = 100;
                    bulletLifeSpan = 2.5f;
                    bulletSpeed = 2500;
                    break;

                case "Weapon 2":
                    weaponId = 2;
                    fireMode = 0;
                    fireRate = .1f;
                    maxAmmo = 250;
                    currentAmmo = 150;
                    currentClip = 50;
                    clipSize = 50;
                    reloadAmount = 50;
                    bulletLifeSpan = 1f;
                    bulletSpeed = 3000;
                    break;

                default:
                    break;
            }

        }
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

        if((currentAmmo < maxAmmo) && collision.gameObject.tag == "ammoPickUp")
        {
            currentAmmo += reloadAmount;

            if (currentAmmo > maxAmmo)
                currentAmmo = maxAmmo;

            Destroy(collision.gameObject);
        }
        
    }

    public void reloadClip()
    {
        if (currentClip >= clipSize)
            return;

        else
        {
            float reloadCount = clipSize - currentClip;

            if (currentAmmo < reloadCount)
            {
                currentClip += currentAmmo;

                currentAmmo = 0;
                return;
                
            }

            else 
            {
                currentClip += reloadCount;

                currentAmmo -= reloadCount;
                return;
            }
        }
    }
    IEnumerator cooldownFire()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
}
