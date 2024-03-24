using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int type;
    //logica para destruir a casa gerada para evitar deadends
    
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
