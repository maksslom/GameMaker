using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
    public void LoadScene(string name)
    {
        if (name != null)
        {
            SceneManager.LoadScene("Gra");
        }
    }
}
