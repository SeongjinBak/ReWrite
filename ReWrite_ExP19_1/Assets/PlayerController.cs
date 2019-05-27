using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubProject14
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D rb2D { get; set; }
        public Vector3 moveVector { get; set; }
        public float moveSpeed = 20;
        public JoystickScript joystick;
        public Animator animator;

        public Vector3 prevVectorInfo;


    void Start()
        {
            animator = GetComponent<Animator>();
            System.GC.Collect();
            rb2D = GetComponent<Rigidbody2D>();
            moveVector = new Vector3(0, 0, 0);

           // animator.SetFloat("DirX", vector.x);
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();


        }
        void FixedUpdate()
        {
            Move();
            EaseVelocity();


        }
        public void HandleInput() {
            moveVector = PoolInput();
        }
        public Vector3 PoolInput()
        {
            Vector3 direction = Vector3.zero;

            direction.x = joystick.GetHorizontalValue();
            direction.y = joystick.GetVerticalValue();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))

            { direction.y = 0; }
            
            else 
            { direction.x = 0; }

            if (Mathf.Abs( direction.x )>Mathf.Abs(direction.y))
            {
                direction.y = 0f;
                animator.SetBool("Walking", true);
            }
            else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                direction.x = 0f;
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
            if(direction.x > 0)
            {
                direction.x = 1f;
            }
            else if(direction.x < 0)
            {
                direction.x = -1f;
            }
            if (animator.GetBool("Walking") == true)
            {
                prevVectorInfo.x = direction.x;
                prevVectorInfo.y = direction.y;
            }
            animator.SetFloat("DirX", direction.x);
            animator.SetFloat("DirY", direction.y);

            if(animator.GetBool("Walking") == false)
            {

                animator.SetFloat("DirX", prevVectorInfo.x);
                animator.SetFloat("DirY", prevVectorInfo.y);
            }
            
            if (direction.magnitude > 1)
            {
                direction.Normalize();
                
            }
            return direction;
        }
        public void Move()
        {
            rb2D.AddForce(moveVector * moveSpeed);
        }
        public void EaseVelocity()
        {
            Vector3 easeVelocity = rb2D.velocity;
            easeVelocity.x *= 0.75f;
            easeVelocity.y *= 0.75f;
            easeVelocity.z = 0;
            rb2D.velocity = easeVelocity;
        }
    }
}