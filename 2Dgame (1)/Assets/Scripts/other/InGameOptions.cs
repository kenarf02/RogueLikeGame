using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameOptions : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject inventory;
    public void SetOptionsOn()
    {
        OptionsMenu.SetActive(true);
        inventory.SetActive(false);
    }
    public void GoBack()
    {
        OptionsMenu.SetActive(false);
        inventory.SetActive(true);
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
