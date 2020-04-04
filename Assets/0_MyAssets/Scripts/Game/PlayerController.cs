using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    Vector3 warkVec, mouseDownPos;
    float walkSpeed = 10f;
    Rigidbody rb;
    public void OnStart()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnUpdate()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        SetWalkVec();



        //warkVec = GetWalkVec();
        float degree = Vector2ToDegree(new Vector2(warkVec.z, warkVec.x));
        transform.eulerAngles = new Vector3(0, degree, 0);

        rb.velocity = warkVec.normalized * walkSpeed;
    }

    void SetWalkVec()
    {
        if (!Input.GetMouseButton(0)) { return; }
        Vector2 mouseVec = Input.mousePosition - mouseDownPos;

        //タップで止まる対策
        if (mouseVec.sqrMagnitude < 1.0f) { return; }
        warkVec.x = mouseVec.x;
        warkVec.z = mouseVec.y;

    }

    Vector3 GetWalkVec()
    {
        if (GetPosition(out Vector3 result))
        {
            float x = result.x - transform.position.x;
            float z = result.z - transform.position.z;
            return new Vector3(x, 0, z);
        }
        else
        {
            return warkVec;
        }
    }

    bool GetPosition(out Vector3 result)
    {
        // カメラはメインカメラを使う
        var camera = Camera.main;
        // クリック位置を取得
        var touchPosition = Input.mousePosition;
        // XZ平面を作る
        var plane = new Plane(Vector3.up, 0);
        // カメラからのRayを作成
        var ray = camera.ScreenPointToRay(touchPosition);
        // rayと平面の交点を求める（交差しない可能性もある）
        if (plane.Raycast(ray, out float enter))
        {
            result = ray.GetPoint(enter);
            return true;
        }
        else
        {
            // rayと平面が交差しなかったので座標が取得できなかった
            result = Vector3.zero;
            return false;
        }
    }

    public static float Vector2ToDegree(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }

}
