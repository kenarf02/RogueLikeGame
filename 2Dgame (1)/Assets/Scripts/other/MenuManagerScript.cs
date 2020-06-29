using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManagerScript : MonoBehaviour
{
    public AudioClip selection;
    private AudioSource ass;
    public GameObject Menu;
    public GameObject startStepone;
    public GameObject wantTutorial;
    public GameObject quitStepOne;
    public GameObject OptionsObj;
    public Scene PCG;
    private bool WasTutorial;

    private void Start()
    {
        if (PlayerPrefs.HasKey("TutorialPlayed"))
        {
            if (PlayerPrefs.GetInt("TutorialPlayed") == 1)
            {
                WasTutorial = true;
            }
            else
            {
                WasTutorial = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("TutorialPlayed", 0);
            WasTutorial = false;
        }
    }
    public void StartStepOne()
    {
        if(WasTutorial == true)
        {
            playsnd();
            startStepone.SetActive(true);
            Menu.SetActive(false);
        }
        else
        {
            playsnd();
            wantTutorial.SetActive(true);
            Menu.SetActive(false);
        }
        
    }
    public void PlayTutorial()
    {
        playsnd();
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("TutorialPlayed", 1);
    }
    public void FuckTutorial()
    {
        playsnd();
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("TutorialPlayed", 1);
    }
    public void TakeTutorialBack()
    {
        playsnd();
        wantTutorial.SetActive(false);
        Menu.SetActive(true);
    }
    public void TakeBackStart()
    {
        playsnd();
        startStepone.SetActive(false);
        Menu.SetActive(true);
    }
   public void StartGame()
    {
        playsnd();
        //setactive menu are you ready to die?
        SceneManager.LoadScene(1);
    }
    public void OptionsGoTO()
    {
        OptionsObj.SetActive(true);
        Menu.SetActive(false);
        playsnd();
    }
    public void TakeBackOptions()
    {
        OptionsObj.SetActive(false);
        Menu.SetActive(true);
        playsnd();
    }
    public void ExitStepOne()
    {
        playsnd();
        quitStepOne.SetActive(true);
        Menu.SetActive(false);
    }
    public void Exit()
    {
        playsnd();
        //setactive some kind of second menu like do you really want to run like a coward?
        Application.Quit();
    }
    public void TakeBackExit()
    {
        playsnd();
        quitStepOne.SetActive(false);
        Menu.SetActive(true);
    }
    private void playsnd()
    {
        ass = GetComponent<AudioSource>();
        ass.clip = selection;
        ass.Play();
    }
    public void GotoMenuGame()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BugReport()
    {
        Application.OpenURL("https://forms.gle/zMgCEYmkdWo4oFPN7");
    }
}
