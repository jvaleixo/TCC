using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject healthContainer;
    private float fillValue; //0.17 cada ponto
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillValue = (float)GameController.health;
        fillValue = fillValue / GameController.maxHealth;
        healthContainer.GetComponent<Image>().fillAmount = fillValue;  
    }
}
