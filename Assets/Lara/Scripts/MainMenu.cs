using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Do You Copy");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Credits");
    }

    public void ToMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
