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
        fillValue = (float)PlayerMovement.health;
        fillValue = fillValue / PlayerMovement.maxHealth;
        healthContainer.GetComponent<Image>().fillAmount = fillValue;  
    }
}
