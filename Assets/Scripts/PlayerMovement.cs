using UnityEngine;
using static GameModeManager;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameModeManager GameModeManager;
    [SerializeField]
    private CharacterController Controller;
    [SerializeField]
    private float Speed = 4f;
    [SerializeField]
    private float SprintSpeedMultiplier = 2f;
    [SerializeField]
    private float CreativeModeSpeedMultiplier = 2f;
    [SerializeField]
    private float CreativeModeFlySpeed = 4f;
    [SerializeField]
    private float Gravity = -9.8f;
    [SerializeField]
    private float JumpHeight = 1.2f;
    [SerializeField]
    private float GroundDistance = 0.375f;
    [SerializeField]
    private Transform GroundCheck;
    [SerializeField]
    private LayerMask PlayerMask;

    private Vector3 Velocity;
    private bool IsGrounded;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (GameModeManager.Mode == GameMode.Creative)
        {
            CreativeModeMovement(ref move);
        }
        else
        {
            SurvivalModeMovement(ref move);
        }

        Controller.Move(move * Speed * Time.deltaTime);
    }

    private void CreativeModeMovement(ref Vector3 move)
    {
        move = move * CreativeModeSpeedMultiplier;
        Velocity.y = 0f;

        if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))
        {
            Velocity.y = CreativeModeFlySpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Space))
        {
            Velocity.y = -CreativeModeFlySpeed;
        }
        Controller.Move(Velocity * Time.deltaTime);
    }

    private void SurvivalModeMovement(ref Vector3 move)
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, ~PlayerMask);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move = move * SprintSpeedMultiplier;
        }

        if (IsGrounded)
        {
            if (Velocity.y <= -2)
            {
                Velocity.y = -2f;
            }

            Controller.stepOffset = 0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                Controller.stepOffset = 0f;
            }
        }
        else
        {
            Velocity.y += Gravity * Time.deltaTime;
            Controller.stepOffset = 0;
        }

        Controller.Move(Velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GroundCheck.position, GroundDistance);
    }
}
