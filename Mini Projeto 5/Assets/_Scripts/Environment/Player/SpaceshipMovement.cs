using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipMovement : MonoBehaviour
{
    [Header("=== Ship movement Config ===")]
    [SerializeField, Tooltip("Torque para girar a nave no eixo Yaw (esquerda/direita).")]
    private float yawTorque = 100f;

    [SerializeField, Tooltip("Torque para inclinar a nave no eixo Pitch (frente/trás).")]
    private float pitchTorque = 200f;

    [SerializeField, Tooltip("Torque para girar a nave no eixo Roll (rolagem).")]
    private float rollTorque = 500f;

    [SerializeField, Tooltip("Força de propulsão para mover a nave para frente.")]
    private float thrust = 100f;

    [SerializeField, Tooltip("Força de propulsão para mover a nave para cima/baixo.")]
    private float upThrust = 50f;

    [SerializeField, Tooltip("Força de propulsão para mover a nave lateralmente (esquerda/direita).")]
    private float strafeThrust = 50f;

    [SerializeField, Range(0.001f, 0.999f), Tooltip("Redução gradual do movimento de avanço da nave (desaceleração).")]
    private float thrustGlidReduction = 0.5f;

    [SerializeField, Range(0.001f, 0.999f), Tooltip("Redução gradual do movimento vertical (subir/descer).")]
    private float upDownGlideReduction = 0.111f;

    [SerializeField, Range(0.001f, 0.999f), Tooltip("Redução gradual do movimento lateral (esquerda/direita).")]
    private float leftRightGlideReduction = 0.111f;

    [Header("=== Ship boost Config ===")]
    [SerializeField] private float maxBoostAmount = 2f;
    [SerializeField] private float boostDeprecationRate = .25f;
    [SerializeField] private float boostRechargeRate = .5f;
    [SerializeField] private float boostMultiplier = 5f;
    public bool boosting;
    public float currentBoostAmount;

    float glide, verticalGlide, horizontalGlide = 0f; // resistência natural para evitar força infinita e causar equilibrio para cada força
    // recebe valor de thrust para equilibrar combinado glidereduction

    Rigidbody rb;


    // Input values
    private float thrust1D;
    private float strafe1D;
    private float upDown1D;
    private float roll1D;
    private Vector2 pitchYaw;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBoostAmount = maxBoostAmount;
    }

    // Update is called once per frame
    void Update()
    {
        HandleBoosting();
        HandleMovement();
    }

    /// <summary>
    ///     Função de movimentação completa usando inputs
    /// </summary>
    ///

    void HandleClampMovement()
    {
        // Movimentos de rotação, forças de torque
        // Roll - Q e E
        rb.AddRelativeTorque(Vector3.forward * roll1D * rollTorque * Time.deltaTime);
        // Pitch - mouse cina baixo
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1, 1f) * pitchTorque * Time.deltaTime);
        //// Yaw - mouse lados
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1, 1f) * yawTorque * Time.deltaTime);

        // Thrust - W e S
        if (Mathf.Abs(thrust1D) > 0.1f)
        {
            float currentThrust = boosting ? thrust * boostMultiplier : thrust;
            rb.AddRelativeForce(Vector3.forward * Mathf.Clamp(thrust1D, 0f, thrust1D * currentThrust) * Time.deltaTime);
            glide = thrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= thrustGlidReduction;
        }

        // Up down
        if (Mathf.Abs(upDown1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.up * Mathf.Clamp(upDown1D, 0f, upDown1D * upThrust) * Time.fixedDeltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide *= upDownGlideReduction;

        }
        // Strafe  A e D
        if (Mathf.Abs(strafe1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.right * Mathf.Clamp(strafe1D, 0f, strafe1D * upThrust) * Time.fixedDeltaTime);
            horizontalGlide = strafe1D * strafeThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideReduction;

        }
    }

    void HandleMovement()
    {
        // Movimentos de rotação, forças de torque
        // Roll - Q e E
        rb.AddRelativeTorque(Vector3.forward * roll1D * rollTorque * Time.deltaTime);
        // Pitch - mouse cina baixo
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1, 1f) * pitchTorque * Time.deltaTime);
        //// Yaw - mouse lados
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1, 1f) * yawTorque * Time.deltaTime);

        // Thrust - W e S
        if (Mathf.Abs(thrust1D) > 0.1f)
        {
            float currentThrust = boosting ? thrust * boostMultiplier : thrust;

            rb.AddRelativeForce(Vector3.forward * thrust1D * currentThrust * Time.deltaTime);
            glide = thrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= thrustGlidReduction;
        }

        // Up down
        if (Mathf.Abs(upDown1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.fixedDeltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide *= upDownGlideReduction;

        }
        // Strafe
        if (Mathf.Abs(strafe1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.right * strafe1D * upThrust * Time.fixedDeltaTime);
            horizontalGlide = strafe1D * strafeThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideReduction;

        }
    }

    void HandleBoosting()
    {
        if (boosting && currentBoostAmount > 0f)
        {
            currentBoostAmount -= boostDeprecationRate;
            if (currentBoostAmount <= 0f)
            {
                boosting = false;
            }
        }
        else
        {
            if (currentBoostAmount < maxBoostAmount)
            {
                currentBoostAmount += boostRechargeRate;
            }
        }
    }

    #region Input Methods
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }
    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }
    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }
    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }
    public void OnBoost(InputAction.CallbackContext context)
    {
        boosting = context.performed;
    }
    #endregion

}
