using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState //definir estados do inimigo
{
    Wander,
    Follow,
    Die,
    Attack,
};

public enum EnemyType // definir tipos de inimigos
{
    Melee,
    Ranged,
};
public class EnemyController : MonoBehaviour
{
    GameObject player;
    public GameObject bulletPrefab; // prefab da bala
    public EnemyState currentState = EnemyState.Wander; //define o inimigo para ficar andando até que veja o jogador
    public EnemyType eType; //tipo do inimigo
    public float range; //distancia em que o inimigo consegue ver o jogador
    public float speed; // velocidade de movimento do inimigo
    public float attackingRange; // distancia de ataque do inimigo
    public int health; //vida do inimigo
    public float cd; //tempo entre ataque
    public int danoM; //dano melee do inimigo
    private bool chooseDirection = false;
    private bool cdAttack = false;
    private Vector3 randomDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // referencia ao jogador
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }
        if (IsPlayerInRange(range) && currentState != EnemyState.Die) //verifica se o inimigo tem o jogador no range e se nao esta morto
        {
            currentState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Wander;
        }
        if (Vector2.Distance(transform.position, player.transform.position) <= attackingRange)
        {
            currentState = EnemyState.Attack;
        }
    }

    private bool IsPlayerInRange(float range) //retorna a distancia entre o inimigo e o jogador naquele momento
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection() //define uma direcao aleatoria para o inimigo a cada intervalo de tempo
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(1f, 6f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDirection = false;
    }

    private IEnumerator coolDown()
    {
        cdAttack = true;
        yield return new WaitForSeconds(cd);
        cdAttack = false;
    }

    void Wander() //funcao para fazer o inimigo andar normalmente
    {
        if(!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }
    }

    void Follow() //funcao para fazer o inimigo seguir o jogador quando tiver no range
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed*Time.deltaTime);
    }

    public void Death()//funcao para matar o inimigo
    {
        Destroy(gameObject);
    } 

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }
    public void Attack() //funcao para definir o ataque do inimigo
    {
        if (!cdAttack)
        {
            switch(eType)
            {
                case(EnemyType.Melee):
                    GameController.DamagePlayer(danoM, player);//chama a funcao de dar dano no jogador
                    StartCoroutine(coolDown());// espera o cooldown para dar o dano novamente
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject; // instanciando a bala do inimigo assim como é feito com a bala do jogador
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform); // dar o tiro na direcao do jogador
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0; //define a gravidade da bala em zero para que ela nao caia da cena
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(coolDown()); // espera o cooldown ate atirar novamente
                    break;
            }
        }
        
    }

}
