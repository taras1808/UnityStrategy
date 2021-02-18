using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 4f;

    public float gravity = -9.8f;

    public float jumpHeight = 1.2f;

    public float groundDistance = 0.15f;

    public Vector3 velocity;

    public Transform groundCheck;

    public bool isGrounded;

    public LayerMask playerMask;

    private DoubleClicker tabDoubbleCatch = new DoubleClicker(KeyCode.Space);

    private bool creativeMode = false;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (tabDoubbleCatch.DoubleClickCheak())
        {
            creativeMode = !creativeMode;
        }

        if (creativeMode)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = 2f;
                
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                velocity.y = -2f;
            }
            else
            {
                velocity.y = 0f;
            }
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~playerMask);
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = move * 2f;
            }

            if (isGrounded && velocity.y <= -2)
            {
                velocity.y = -2f;
                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }

            controller.Move(velocity * Time.deltaTime);
        }

        controller.Move(move * speed * Time.deltaTime);
    }



    public class DoubleClicker
    {
        /// <summary>
        /// Construcor with keycode and deltaTime set
        /// </summary>
        public DoubleClicker(KeyCode key, float deltaTime)
        {
            //set key
            this._key = key;

            //set deltaTime
            this._deltaTime = deltaTime;
        }

        /// <summary>
        /// Construcor with defult deltatime 
        /// </summary>
        public DoubleClicker(KeyCode key)
        {
            //set key
            this._key = key;
        }

        private KeyCode _key;
        private float _deltaTime = defultDeltaTime;

        //defult deltaTime
        public const float defultDeltaTime = 0.3f;

        /// <summary>
        /// Current key property
        /// </summary>
        public KeyCode key
        {
            get { return _key; }
        }

        /// <summary>
        /// Current deltaTime property
        /// </summary>
        public float deltaTime
        {
            get { return _deltaTime; }
        }

        //time pass
        private float timePass = 0;
        /// <summary>
        /// Cheak for double press
        /// </summary>
        public bool DoubleClickCheak()
        {
            if (timePass > 0) { timePass -= Time.deltaTime; }

            if (Input.GetKeyDown(_key))
            {
                if (timePass > 0) { timePass = 0; return true; }

                timePass = _deltaTime;
            }

            return false;
        }
    }
}
