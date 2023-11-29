using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    public void UpdateHealthUI(int health)
    {
        healthSlider.value = health;
    }

    public void UpdateHealthBar()
    {
        healthSlider.maxValue = GameManager.gameManager.health;
    }

}
