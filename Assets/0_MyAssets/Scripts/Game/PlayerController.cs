using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayerController
{
    [SerializeField] Animator animator;
    Vector3 warkVec, mouseDownPos;
    float walkSpeed = 10f;
    Rigidbody rb;
    public override void OnStart()
    {
        base.OnStart();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnUpdate()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            SetWalkVec();
        }

        float degree = Vector2ToDegree(new Vector2(warkVec.z, warkVec.x));
        transform.eulerAngles = new Vector3(0, degree, 0);

        rb.velocity = warkVec.normalized * walkSpeed;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    void SetWalkVec()
    {

        Vector2 mouseVec = Input.mousePosition - mouseDownPos;

        //タップで止まる対策
        if (mouseVec.sqrMagnitude < 1.0f) { return; }
        warkVec.x = mouseVec.x;
        warkVec.z = mouseVec.y;

    }

    public static float Vector2ToDegree(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }

}
