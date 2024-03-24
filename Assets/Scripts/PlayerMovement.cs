using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f; //velocidade de movimento
    public Rigidbody2D rb; //rigid body do jogador
    Vector2 movement; // consegue guardar os valores de horizontal e vertical ao mesmo tempo
    Vector2 shooting;
    public GameObject bulletPrefab; //prefab da bala utilizada no jogo
    public float bulletSpeed; // velocidade da bala para nao virar uma 'arma laser'
    private float lastFire; // momento do ultimo tiro
    public float fireDelay; // delay entre os tiros
    public static int collectAmount = 0;
    public static int enemyKilled = 0;
    public static int tiros = 0;
    public static int danoI;
    // Update is called once per frame
    void Update() //inputs do jogador
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // 1 direita ; 0 nada ; -1 esquerda
        movement.y = Input.GetAxisRaw("Vertical");
        shooting.x = Input.GetAxisRaw("ShootHorizontal"); // 1 direita ; 0 nada ; -1 esquerda
        shooting.y = Input.GetAxisRaw("ShootVertical");
    }

    void FixedUpdate() //controle do movimento do boneco do jogador
    {
        rb.MovePosition(rb.position+movement*movementSpeed*Time.fixedDeltaTime); //time.fixeddeltatime é usado para que o movementspeed se mantenha constante
        if((shooting.x != 0 || shooting.y != 0) && (Time.time > lastFire + fireDelay)) //logica para
        {
            Shoot(shooting);
            lastFire = Time.time;
        } 
    }
    
    void Shoot(Vector2 x)
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
            (x.x < 0) ? Mathf.Floor(x.x) * bulletSpeed : Mathf.Ceil(x.x) * bulletSpeed,
            (x.y < 0) ? Mathf.Floor(x.y) * bulletSpeed : Mathf.Ceil(x.y) * bulletSpeed
            );// fazendo uso de operador ternario para garantir que a velocidade do tiro se mantera sempre a mesma
        if(x.y == 1)
        {
            bullet.transform.Rotate(Vector3.forward * 90);
        }
        if (x.y == -1)
        {
            bullet.transform.Rotate(Vector3.forward * 270);
        }
        if (x.x == -1)
        {
            bullet.transform.Rotate(Vector3.forward * 180);
        }
    }
    
}
