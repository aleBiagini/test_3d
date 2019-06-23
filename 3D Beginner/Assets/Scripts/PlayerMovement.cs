using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    // vettore che rappresenta la posizione di un gameObject
    Vector3 m_Movement;
    Animator m_Animator;
    Rigidbody m_RigidBody;
    // quaterna che rappresenta la rotazione di un GameObj
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        // fissa il movimento massimo sempre ad una unità
        m_Movement.Normalize();
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("isWalking", isWalking);

        // costruisco la velocità di rotazione del personaggio
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void OnAnimatorMove()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_RigidBody.MoveRotation(m_Rotation);
    }
}
