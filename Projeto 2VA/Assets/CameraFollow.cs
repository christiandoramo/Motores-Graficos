
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referência ao player
    public Vector3 offset;    // Distância entre a câmera e o player
    public float smoothSpeed = 0.125f;  // Velocidade de suavização

    void Start()
    {
        // Configura a distância inicial entre a câmera e o player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        //// Calcula a nova posição desejada com base na posição do player e o offset
        Vector3 desiredPosition = player.position + offset;

        //// Suaviza a transição entre a posição atual e a posição desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //// Atualiza a posição da câmera
        transform.position = desiredPosition;

        //// (Opcional) Se você quiser que a câmera sempre olhe para o player:
        //transform.LookAt(player);
    }
}
