using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject Player;
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        transform.position = position;
    }
}
