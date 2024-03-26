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
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void AbrirSala1()
    {
        SceneManager.LoadScene("Sala 1");
    }
    public void AbrirSala2()
    {
        SceneManager.LoadScene("Sala 2");
    }
    public void AbrirSala3()
    {
        SceneManager.LoadScene("Sala 3");
    }
    public void AbrirSala4()
    {
        SceneManager.LoadScene("Sala 4");
    }
    public void AbrirSala5()
    {
        SceneManager.LoadScene("Sala 5");
    }
    public void AbrirSala6()
    {
        SceneManager.LoadScene("Sala 6");
    }
    public void AbrirSala7()
    {
        SceneManager.LoadScene("Sala 7");
    }
    public void AbrirSala8()
    {
        SceneManager.LoadScene("Sala 8");
    }
}
