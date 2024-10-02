using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public bool IsPaused = false;

    
    public GameObject PauseMenu;
    public PlayerController PlayerData;

    public Image healthBar;
    public TextMeshProUGUI clipCounter;
    public TextMeshProUGUI AmmoCounter;
    public TextMeshProUGUI HealthCounter;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            PlayerData = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthBar.fillAmount = Mathf.Clamp((float)PlayerData.currentHealth / (float)PlayerData.maxHealth, 0, 1);

            if (PlayerData.weaponId < 0)
            {
                clipCounter.gameObject.SetActive(false);
                AmmoCounter.gameObject.SetActive(false);
            }
            else
            {
                clipCounter.gameObject.SetActive(true);
                AmmoCounter.gameObject.SetActive(true);

                clipCounter.text = "Clip: " + PlayerData.currentClip + "/" + PlayerData.clipSize;
                AmmoCounter.text = "Ammo: " + PlayerData.currentAmmo;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsPaused)
                {
                    PauseMenu.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    Time.timeScale = 0;

                    IsPaused = true;
                }

                else
                    Resume();
            }

        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        IsPaused = false;
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
