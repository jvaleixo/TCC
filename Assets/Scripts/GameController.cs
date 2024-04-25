using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

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
    //private static int numeroInfos = 0;
    private static int contadorMortes = 0;
    private static int mortesF = 0;
    private static int mortesM = 0;
    private static int mortesD = 0;
    public static int fasesJogadas = 1; //conta quantas fases o jogador já passou
    private static GameObject[] contador;
    
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
        if (PlayerMovement.health <= 0) //verifica se o jogador foi morto
        {
            outputJSON();
            Destroy(jogador);
            contadorMortes++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerMovement.health = PlayerMovement.maxHealth;
        }
        if (numeroInimigos == 0 && SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "Final")
        { //logica para seleção aleatória de fases
            fasesJogadas++;
            Debug.Log(fasesJogadas);
            int rand = Random.Range(2,11); //escolhe aleatorio entre qualquer nivel de fase
            SceneManager.LoadScene(rand);
            outputJSON(); //adicionar ao case
        }
        if (numeroInimigos == 0 && fasesJogadas == 4)
        {
            SceneManager.LoadScene("Boss"); //chama a proxima cena
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Boss"))
        {
            SceneManager.LoadScene("Final"); //chama a proxima cena
            outputJSON();
        }
        if (numeroInimigos == 0 && (SceneManager.GetActiveScene().name == "Final"))
        {
            
            outputJSON();
        }
    }
    public static void DamagePlayer(int damage,GameObject jogador)
    {
        PlayerMovement.health -= damage;
    }
    public static void HealPlayer(int healAmount)
    {
        PlayerMovement.health = Mathf.Min(PlayerMovement.maxHealth, PlayerMovement.health + healAmount);
    }

    public static void contaMortes()
    {
        contadorMortes++;
    }


    [System.Serializable]
    public class Infos
    {
        public int tiros;
        public int curas;
        public int iniM;
        public int danoI;
        public float tempo;
        public int mortes;
        public int mortesF;
        public int mortesM;
        public int mortesD;
    }

    public static Infos infoJogo = new Infos();

    public static void outputJSON() //escreve um arquivo com essas infos que serao uteis no futuro
    {
        infoJogo.tempo = Time.timeSinceLevelLoad;
        infoJogo.tiros = PlayerMovement.tiros;
        infoJogo.curas = PlayerMovement.collectAmount;
        infoJogo.iniM = PlayerMovement.enemyKilled;
        infoJogo.danoI = PlayerMovement.danoI;
        infoJogo.mortes = contadorMortes;
        infoJogo.mortesF = mortesF;
        infoJogo.mortesM = mortesM;
        infoJogo.mortesD = mortesD;
        string strOutput = JsonUtility.ToJson(infoJogo); //criando a string que sera salva
        File.WriteAllText(Application.dataPath + "/infos.txt", strOutput); //salvando o arquivo na pasta do jogo
    }
}
