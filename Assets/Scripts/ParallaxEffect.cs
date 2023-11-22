using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 VelocidadMovimiento;
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
        offset = (Player.velocity.x * 0.1f) * VelocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
