using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void StartG()
    {
        SceneManager.LoadScene("Facil");
        GameController.definirFaseAtual(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
