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
    private static float tempo = 0;
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
        contador = GameObject.FindGameObjectsWithTag("Enemy");
        numeroInimigos = contador.Length;
        //tempo = Time.time - tempoaux;
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Facil" || SceneManager.GetActiveScene().name == "Facil 2"))
        {
            numeroInfos++;
            //Random.RandomRange() //escolhe aleatorio entre as salas medias
            SceneManager.LoadScene("Medio"); //chama a proxima cena
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Medio" || SceneManager.GetActiveScene().name == "Medio 2"))
        {
            numeroInfos++;
            //Random.RandomRange() //escolhe aleatorio entre as salas medias
            SceneManager.LoadScene("Dificil"); //chama a proxima cena
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Dificil" || SceneManager.GetActiveScene().name == "Dificil 2"))
        {
            numeroInfos++;
            SceneManager.LoadScene("Boss"); //chama a proxima cena
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
        //outputJSON();
        SceneManager.LoadScene("Tutorial");
        health = maxHealth;
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
