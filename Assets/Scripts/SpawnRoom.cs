using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;
    //logica para gerar rooms aleatorias apos o gerador ter gerado o caminho principal
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom); //criando um circulo que detecta se existe algum room por perto
        //criando uma room aleatoria
        if (roomDetection == null && levelGen.stopGeneration == true)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand],transform.position,Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
