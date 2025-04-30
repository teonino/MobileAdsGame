using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float followSpeed = 4f;

    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        if (distance > 0.4f)
        {
            transform.LookAt(Player.transform.position);
            transform.Translate(Vector3.forward * distance/2 * followSpeed * Time.deltaTime);
        }
    }
}
