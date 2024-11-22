using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 720f;  // Velocidade de rotação (graus por segundo)
    public Transform cameraTransform;   // Referência para a câmera
    public float cameraDistance = 5f;   // Distância da câmera para o jogador
    public float cameraHeight = 2f;     // Altura da câmera acima do jogador
    private PlayerInput playerInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

    }
    void Update()
    {
        MovePlayer();
        UpdateCameraPosition();
        RotateCameraAroundPlayer();
    }

    void MovePlayer()
    {
        // Obtenha a entrada horizontal e vertical (teclas WASD ou setas)
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        //Debug.Log($"input: {input}");
        //float horizontal = playerInput==null ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal")+ input.x;
        //float vertical = playerInput == null ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical") + input.y;
        float horizontal =  input.x;
        float vertical = input.y;
        // Direção do movimento em relação à câmera
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Só continua se houver movimento (horizontal ou vertical)
        if (moveDirection.magnitude >= 0.1f)
        {
            // Calcula o ângulo relativo à câmera
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Calcula a rotação alvo em relação ao eixo Y (apenas horizontal)
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Suavemente rotaciona o personagem em direção à rotação alvo
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move o personagem para frente na direção em que está rotacionado
            Vector3 moveDirectionForward = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDirectionForward * speed * Time.deltaTime, Space.World);
        }
    }

    void UpdateCameraPosition()
    {
        // Mantém a câmera numa posição fixa atrás do jogador
        Vector3 newCameraPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;

        // Suavemente mova a câmera para a nova posição (opcional, para suavidade)
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, newCameraPosition, Time.deltaTime * 5f);

        // Sempre olha para o jogador
        cameraTransform.LookAt(transform.position + Vector3.up * cameraHeight);
    }

    void RotateCameraAroundPlayer()
    {
        // Obtenha a rotação atual da câmera
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotaciona a câmera ao redor do jogador com base no input horizontal
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Ajusta o ângulo da câmera baseado no input horizontal do jogador
            Vector3 directionFromPlayerToCamera = cameraTransform.position - transform.position;
            directionFromPlayerToCamera = Quaternion.AngleAxis(horizontalInput * rotationSpeed * Time.deltaTime, Vector3.up) * directionFromPlayerToCamera;

            // Atualiza a posição da câmera para que ela "orbite" o jogador
            cameraTransform.position = transform.position + directionFromPlayerToCamera;

            // Sempre olha para o jogador
            cameraTransform.LookAt(transform.position + Vector3.up * cameraHeight);
        }
    }
}
