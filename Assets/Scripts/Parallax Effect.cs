using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Vector2 currentPosition, lastPosition, positionDifference;
    private bool parallaxNow;
    public float speed;

    void LateUpdate()
    {
        if (parallaxNow == true)
        {
            DetectCameraMovement();
            transform.Translate(new Vector3(positionDifference.x, positionDifference.y, 0) * speed * Time.deltaTime, Space.World);
        }
    }

    void DetectCameraMovement()
    {
        currentPosition = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y); // Corrected 'transform.y' to 'transform.position.y'

        if (currentPosition == lastPosition)
        {
            positionDifference = Vector2.zero;
        }
        else
        {
            positionDifference = currentPosition - lastPosition;
        }

        lastPosition = currentPosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "MainCamera")
        {
            lastPosition = currentPosition - new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y); // Corrected 'transform.x' and 'transform.position.y'
            parallaxNow = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "MainCamera")
        {
            parallaxNow = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "MainCamera")
        {
            positionDifference = Vector2.zero;
            parallaxNow = false;
        }
    }
}
