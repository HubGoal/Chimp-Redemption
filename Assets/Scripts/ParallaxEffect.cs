using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 VelocidadMovimiento;
    public float minXLimit = -6f;
    public float maxXLimit = 10f;

    private Vector2 offset;
    private Material material;
    private Rigidbody2D Player;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Verificar si la posición x del jugador está dentro de los límites
        if (Player.transform.position.x > minXLimit && Player.transform.position.x < maxXLimit)
        {
            offset = (Player.velocity.x * 0.1f) * VelocidadMovimiento * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
    }
}
