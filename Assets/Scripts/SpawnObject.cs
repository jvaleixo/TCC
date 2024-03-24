using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand],transform.position, Quaternion.identity);
        instance.transform.parent = transform; // logica para que os tiles gerados das rooms sejam considerados filhos da room gerada, de modo que o destruction consiga apaga-los tambem
    }
}
