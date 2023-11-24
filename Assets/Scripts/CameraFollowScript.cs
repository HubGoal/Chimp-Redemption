using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject Player;
    public float minX;
    public float maxX;
    public float disableClampValue = 9999f;

    void Update()
    {
        // Obtener la posici�n actual de la c�mara
        Vector3 position = transform.position;

        // Verificar si disableClampValue es igual a 9999 para aplicar o no el clamp
        if (disableClampValue == 9999f)
        {
            // Si disableClampValue es 9999, la c�mara sigue al jugador sin restricciones
            position.x = Player.transform.position.x;
        }
        else
        {
            // Establecer la posici�n x de la c�mara siguiendo al jugador con clamp
            position.x = Mathf.Clamp(Player.transform.position.x, minX, maxX);
        }

        // Aplicar la nueva posici�n a la c�mara
        transform.position = position;
    }
}
