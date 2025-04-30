using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float rotationSpeed = 10f;

    [SerializeField] GameObject PlayerBody;

    [SerializeField] Animator Animator;

    public string forwardKey = "W".ToLower();
    public string backwardKey = "S".ToLower();
    public string leftKey = "A".ToLower();
    public string rightKey = "D".ToLower();

    private Vector3 lastDirection = Vector3.zero;
    private Quaternion targetRotation;
    [SerializeField] Camera mainCamera;

    //bool isMoving = false;
    bool isHolding = false;
    public float stopDistance = 0.1f;
    private Vector3 targetPosition;
    private float currentSpeed = 0f;
    public float deceleration = 80f;

    void Start()
    {
        mainCamera = FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(forwardKey))
        {
            direction = transform.forward;
        }
        else if (Input.GetKeyDown(backwardKey))
        {
            direction = -transform.forward;
        }
        else if (Input.GetKeyDown(leftKey))
        {
            direction = -transform.right;
        }
        else if (Input.GetKeyDown(rightKey))
        {
            direction = transform.right;
        }

        if (direction != Vector3.zero && direction != lastDirection)
        {
            lastDirection = direction;
            targetRotation = Quaternion.LookRotation(direction);
            Animator.SetBool("isRunning", true);
        }

        if (lastDirection != Vector3.zero)
        {
            transform.Translate(lastDirection.normalized * movementSpeed * Time.deltaTime, Space.World);
        }

        PlayerBody.transform.rotation = Quaternion.Slerp(PlayerBody.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.D) && !Input.GetMouseButton(0))
        {
            lastDirection = Vector3.zero;
            Animator.SetBool("isRunning", false);
        }

        if (Input.GetMouseButton(0))
        {
            Animator.SetBool("isRunning", true);
            isHolding = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }

            currentSpeed = movementSpeed;
        }
        else
        {
            isHolding = false;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime*2);
        }

        direction = (targetPosition - transform.position);
        direction.y = 0;

        if (direction.magnitude > 0.05f && currentSpeed > 0f)
        {
            Vector3 moveDir = direction.normalized;
            transform.position += moveDir * currentSpeed * Time.deltaTime;

            
            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
        }
    }
}
    

