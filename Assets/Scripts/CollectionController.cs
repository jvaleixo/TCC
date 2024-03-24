using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite ItemImage;
}
public class CollectionController : MonoBehaviour
{
    public Item item;
    public int healthChange;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.ItemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision) //o que fazer quando detectar que o jogador encostou no item
    {
        if(collision.tag == "Player")
        {
            PlayerMovement.collectAmount++;
            GameController.HealPlayer(healthChange);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
