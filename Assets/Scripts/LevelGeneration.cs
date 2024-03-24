using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // 0 -> LR ; 1 -> LRB ; 2 -> LRT ; 3 -> LRBT

    private int direction;
    public float moveAmount;
    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration; //com isso sendo publíco, é possível identificar de outros scripts se o caminho principal foi gerado totalmente ou nao
    public LayerMask room;

    public int downCounter;
    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0],transform.position,Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if(timeBtwRoom <= 0 && stopGeneration == false) {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    //logica para mover o gerador de rooms
    private void Move()
    {
        if (direction == 1 || direction == 2) // direita
        {
            if (transform.position.x < maxX) {
                downCounter = 0; //resetando o contador de idas para baixo do gerador
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                // logica para gerar tipos de rooms aleatorias
                int rand = Random.Range(0, rooms.Length); 
                Instantiate(rooms[rand],transform.position,Quaternion.identity);

                direction = Random.Range(1, 6); //ultimo numero nao incluso
                //logica para evitar que o gerador de niveis nao gere rooms em cima de rooms ja existentes
                if (direction == 3) { 
                    direction = 2;
                } else if (direction == 4)
                {
                    direction = 5;
                }

            } else
            {
                direction = 5;
            }
        } else if (direction == 3 || direction == 4) {
            if (transform.position.x > minX) {
                downCounter = 0; //resetando o contador de idas para baixo do gerador
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); // logica para gerar tipos de rooms aleatorias
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                //logica para evitar que o gerador de niveis nao gere rooms em cima de rooms ja existentes
                direction = Random.Range(3, 6); //ultimo numero nao incluso 
            } else
            {
                direction = 5;
            }
        } else if (direction == 5)
        {
            downCounter++;
            if(transform.position.y > minY)
            {
                //logica para identificar o tipo de room gerada, para evitar deadends em conjunto com o script RoomType
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1,room);
                //detectando se a room gerada nao é do tipo 1 ou 3
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3){
                    //logica para evitar deadends ao gerar duas rooms com abertura para baixo seguidas
                    if(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3],transform.position, Quaternion.identity);
                    } else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        //gerando uma room que tenha abertura em baixo
                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;
                //logica para gerar rooms com saida em cima e em baixo de modo que o jogador nao fique preso se o gerador gerar uma room para baixo
                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand],transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); //ultimo numero nao incluso //logica para evitar que o gerador de niveis nao gere rooms em cima de rooms ja existentes
            }
            else
            {
                //parando a geracao de rooms por chegar no limite de baixo do mapa
                stopGeneration = true;
            }
        }
    }
}
