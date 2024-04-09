using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    
    public GameObject jogador;

    public static int health = 6;

    public static int maxHealth = 6;

    public static float moveSpeed = 5f;

    public static float fireRate = 0.5f;

    public static int bulletSize;
    
    private static int numeroInimigos =0;
    private static int numeroInfos = 0;
    //private static float tempo = 0;
    private static GameObject[] contador;
    //private static string textoVitoria = "Parabéns, você terminou o jogo!";
    public static int Health{ get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    

    // Start is called before the first frame update
    void Awake()
    { 
        if(instance == null)
        {
            instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        float time = Time.timeSinceLevelLoad; //tempo desde o carregamento da sala
        contador = GameObject.FindGameObjectsWithTag("Enemy");
        numeroInimigos = contador.Length;
        Debug.Log("tempo:"+time);
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Facil" || SceneManager.GetActiveScene().name == "Facil 2" || SceneManager.GetActiveScene().name == "Facil 3") && time <= 3)
        { //logica para salas faceis
            numeroInfos++;
            int rand = Random.Range(0,3); //escolhe aleatorio entre as salas medias
            Debug.Log("rand:" + rand);
            switch (rand)//chama a proxima cena aleatoriamente
            {
                case (1):
                SceneManager.LoadScene("Medio");
                    break;
                case (2):
                SceneManager.LoadScene("Medio 2");
                    break;
                case (3):
                SceneManager.LoadScene("Medio 3");
                    break;
            } 
            outputJSON();
        }
        else if(numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Facil" || SceneManager.GetActiveScene().name == "Facil 2" || SceneManager.GetActiveScene().name == "Facil 3") && time > 3)
        {
            numeroInfos++;
            int rand = Random.Range(0, 3); //escolhe aleatorio entre as salas medias
            Debug.Log("rand:" + rand);
            switch (rand)//chama a proxima cena aleatoriamente
            {
                case (1):
                    SceneManager.LoadScene("Facil");
                    break;
                case (2):
                    SceneManager.LoadScene("Facil 2");
                    break;
                case (3):
                    SceneManager.LoadScene("Facil 3");
                    break;
            }
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Medio" || SceneManager.GetActiveScene().name == "Medio 2" || SceneManager.GetActiveScene().name == "Medio 3") && time <= 5)
        { //logica salas medias
            numeroInfos++;
            int rand = Random.Range(0, 3); //escolhe aleatorio entre as salas medias
            Debug.Log("rand:" + rand);
            switch (rand)//chama a proxima cena aleatoriamente
            {
                case (1):
                    SceneManager.LoadScene("Dificil");
                    break;
                case (2):
                    SceneManager.LoadScene("Dificil 2");
                    break;
                case (3):
                    SceneManager.LoadScene("Dificil 3");
                    break;
            }
            outputJSON();
        } else if(numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Medio" || SceneManager.GetActiveScene().name == "Medio 2" || SceneManager.GetActiveScene().name == "Medio 3") && time > 5)
        {
            numeroInfos++;
            int rand = Random.Range(0, 3); //escolhe aleatorio entre as salas medias
            Debug.Log("rand:" + rand);
            switch (rand)//chama a proxima cena aleatoriamente
            {
                case (1):
                    SceneManager.LoadScene("Medio");
                    break;
                case (2):
                    SceneManager.LoadScene("Medio 2");
                    break;
                case (3):
                    SceneManager.LoadScene("Medio 3");
                    break;
            }
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Dificil" || SceneManager.GetActiveScene().name == "Dificil 2" || SceneManager.GetActiveScene().name == "Dificil 3") && time <= 8)
        {
            numeroInfos++;
            SceneManager.LoadScene("Boss"); //chama a proxima cena
            outputJSON();
        } else if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Dificil" || SceneManager.GetActiveScene().name == "Dificil 2" || SceneManager.GetActiveScene().name == "Dificil 3") && time > 8)
        {
            numeroInfos++;
            int rand = Random.Range(0, 3); //escolhe aleatorio entre as salas medias
            Debug.Log("rand:" + rand);
            switch (rand)//chama a proxima cena aleatoriamente
            {
                case (1):
                    SceneManager.LoadScene("Dificil");
                    break;
                case (2):
                    SceneManager.LoadScene("Dificil 2");
                    break;
                case (3):
                    SceneManager.LoadScene("Dificil 3");
                    break;
            }
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Boss"))
        {
            numeroInfos++;
            SceneManager.LoadScene("Final"); //chama a proxima cena
            outputJSON();
        }
    }
    public static void DamagePlayer(int damage,GameObject jogador)
    {
        health -= damage;

        if(Health <= 0)
        {
            KillPlayer(jogador);
        }
    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private static void KillPlayer(GameObject jogador)
    {
        Destroy(jogador);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        health = maxHealth;
        outputJSON();
       
    }


    [System.Serializable]
    public class infos
    {
        public int tiros;
        public int curas;
        public int iniM;
        public int danoI;
        public float tempo;
    }

    public static infos infoJogo = new infos();

    public static void outputJSON() //escreve um arquivo com essas infos que serao uteis no futuro
    {
        infoJogo.tempo = Time.time;
        infoJogo.tiros = PlayerMovement.tiros;
        infoJogo.curas = PlayerMovement.collectAmount;
        infoJogo.iniM = PlayerMovement.enemyKilled;
        infoJogo.danoI = PlayerMovement.danoI;
        
        string strOutput = JsonUtility.ToJson(infoJogo); //criando a string que sera salva
        File.WriteAllText(Application.dataPath + "/JSONs/infos.txt", strOutput); //salvando o arquivo na pasta do jogo
    }
}
