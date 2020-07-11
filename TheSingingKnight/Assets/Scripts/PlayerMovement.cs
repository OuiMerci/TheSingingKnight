using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    Vector3 forward, right;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    public void ApplyMovement(Vector2 movement)
    {
        if (GameManager.Instance.Player.SongGameplay.IsDancing)
            return;

        Vector3 rightM = right * Speed * Time.deltaTime * movement.x;
        Vector3 upM = forward * Speed * Time.deltaTime * movement.y;

        Vector3 heading = Vector3.Normalize(rightM + upM);

        if(heading != Vector3.zero)
        {
            transform.forward = heading;
            transform.position += rightM;
            transform.position += upM;
        }

        GameManager.Instance.Player.Animator.SetFloat("Speed", movement.magnitude);
    }

    public void StartMoving()
    {
        GameManager.Instance.Player.Animator.SetBool("IsMoving", true);
    }

    public void StartIdle()
    {
        GameManager.Instance.Player.Animator.SetBool("IsMoving", false);
    }
}
