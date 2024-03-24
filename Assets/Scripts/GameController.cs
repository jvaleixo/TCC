using System.Collections;
using System.Collections.Generic;
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

    private static Text healthText;
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
    }
}
