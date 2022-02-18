using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject PauseInterface;
    public GameObject SettingInterface;
    public GameObject PropsInterface;
    public GameObject ControlInterface;
    bool isPauseOpen = false;
    bool isSettingOpen = false;
    bool isPropOpen = false;

    public Image m_Power;
    public Image m_BulletInterval;
    public Image m_PlayerSpeed;
    public PlayerControl playerControl;

    void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        m_PlayerSpeed.fillAmount =playerControl.movementSpeed_Final/20;
        m_BulletInterval.fillAmount = 1/playerControl.ShootInterval_Final*0.1f;
        m_Power.fillAmount = playerControl.rangeDamage_Final / 10;

        if (Input.GetKeyDown(KeyCode.I))
        {
            PauseMenuToggle();
            if (!isPropOpen)
            {
                OpenPropsInterface(); 
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuToggle();
            if (!isSettingOpen)
            {
                OpenSettingInterface();
            }
        }
    }
    // Update is called once per frame
    public void OpenSettingInterface()
    {
        if (SettingInterface.activeInHierarchy) return;
        ControlInterface.SetActive(false);
        PropsInterface.SetActive(false);
        SettingInterface.SetActive(true);
        isSettingOpen = true;
        isPropOpen = false;
    }
    public void OpenPropsInterface()
    {
        if (PropsInterface.activeInHierarchy) return;
            SettingInterface.SetActive(false);
            PropsInterface.SetActive(true);
            ControlInterface.SetActive(false);
        isSettingOpen = false;
        isPropOpen = true;
    }
    public void OpenControlInterface()
    {
        if (ControlInterface.activeInHierarchy) return;
            SettingInterface.SetActive(false);
            PropsInterface.SetActive(false);
            ControlInterface.SetActive(true);
        isSettingOpen = false;
        isPropOpen = false;
    }
    public void Continue()
    {
        PauseInterface.SetActive(false);
        isPauseOpen = false;
        Time.timeScale = 1;
    }
    public void PauseMenuToggle()
    {
        if (!isPauseOpen)
        {
            PauseInterface.SetActive(true);
            isPauseOpen = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseInterface.SetActive(false);
            isPauseOpen = false;
            Time.timeScale = 1;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToStudio()
    {
        SceneManager.LoadScene("Studio");
        Time.timeScale = 1f;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Start");
       
    }
}
