using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime;
    public bool isEnemyBullet = false;
    public int dano;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    GameObject player;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay()); // para que a bala seja destruida automaticamente apos o tiro
    }

    void Update()
    {
        if (isEnemyBullet)
        {
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime); //funcionamento parecido com o script que faz o inimigo seguir o jogador
            if(curPos == lastPos)
            {
                Destroy(gameObject); //destroi a bala
            }
            lastPos = curPos;
        }
    }
    
    public void GetPlayer(Transform player)
    {
        playerPos = player.position; //descobre a posicao do jogador
    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(dano); //chama a funcao de morte do inimigo assim que a bala o atinge
            Destroy(gameObject);   //destroi a bala apos atingir o inimigo
        }

        if(collision.CompareTag("Player") && isEnemyBullet)
        {
            GameController.DamagePlayer(dano, player);
        }
    }
}
