using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public bool IsPaused = false;

[Header("Volume")]
    public AudioMixer audioMixer;

[Header("Main Pieces")]
    public GameObject SaveProfiles;
    public GameObject PauseMenu;
    public GameObject PlayerInterface;
    public PlayerController PlayerData;

[Header("Game UI")]
    public Image healthBar;
    public TextMeshProUGUI clipCounter;
    public TextMeshProUGUI AmmoCounter;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            PlayerData = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthBar.fillAmount = Mathf.Clamp(PlayerData.currentHealth / (float)PlayerData.maxHealth, 0, 1);

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
                    PlayerInterface.SetActive(false);
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
        PlayerInterface.SetActive(true);
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

    public void PauseScreen()
    {
        PlayerInterface.SetActive(false);
        PauseMenu.SetActive(true);
        IsPaused = true;

    }

    public void SettingScreen()
    {
        PauseMenu.SetActive(false);
        IsPaused = true;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality (int QualityLvl)
    {
        QualitySettings.SetQualityLevel(QualityLvl);
    }
}
