using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
public class PlayerController : BaseCharactorController
{
    public enum PlayerType
    {
        Player,
        Enemy,
    }
    [SerializeField] Animator animator;
    int playerIndex;
    Rigidbody rb;
    float walkSpeed = 700f;
    Vector3 walkVec;
    int maxSize;
    PlayerType type;
    Vector3 mouseDownPos;

    public override void OnStart()
    {
        base.size = 0;
        this.ObserveEveryValueChanged(count => Variables.playerProperties[playerIndex].eatenCount)
            .Subscribe(count => CheckSizeUp(count))
            .AddTo(this.gameObject);
        rb = GetComponent<Rigidbody>();
        transform.localScale = Vector3.one;
        maxSize = Variables.playerSizes.Last().size;

        type = (playerIndex == 0) ? PlayerType.Player : PlayerType.Enemy;

        switch (type)
        {
            case PlayerType.Player:
                break;
            case PlayerType.Enemy:
                walkVec = Vector3.forward;
                break;
        }
    }

    public void SetParam(int playerIndex)
    {
        this.playerIndex = playerIndex;
        name = "Player_" + playerIndex;
    }

    public override void OnUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            animator.SetTrigger("Run");
        }

        switch (type)
        {
            case PlayerType.Player:
                Controller();
                break;
            case PlayerType.Enemy:
                break;
        }

        SetVelocityFromWalkVec();
    }


    void OnCollisionEnter(Collision col)
    {

        switch (type)
        {
            case PlayerType.Player:

                break;
            case PlayerType.Enemy:
                OnCollisionWall(col);
                break;
        }

        OnCollisionCharactor(col);
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    void SetVelocityFromWalkVec()
    {
        float degree = Vector2ToDegree(new Vector2(walkVec.z, walkVec.x));
        transform.eulerAngles = new Vector3(0, degree, 0);
        Vector3 vel = walkVec.normalized * walkSpeed * Time.deltaTime;
        //落下しなくなるため、上に飛ばないようにする
        if (rb.velocity.y < 0) vel.y = rb.velocity.y;
        rb.velocity = vel;
    }


    void OnCollisionWall(Collision col)
    {
        if (col.transform.CompareTag("Ground")) { return; }
        Vector3 normal = col.contacts[0].normal;
        normal.y = 0;
        Vector3 reflectVec = Vector3.Reflect(walkVec, normal);
        walkVec = reflectVec;
    }

    void OnCollisionCharactor(Collision col)
    {
        var colCharactor = col.gameObject.GetComponent<BaseCharactorController>();
        if (colCharactor == null) { return; }
        //おなじだと両方消えるので
        if (colCharactor.size >= base.size) { return; }

        Variables.playerProperties[playerIndex].eatenCount++;
        colCharactor.Killed();
        animator.SetTrigger("Attack");
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
    }

    void SetWalkVec()
    {

        Vector2 mouseVec = Input.mousePosition - mouseDownPos;

        //タップで止まる対策
        if (mouseVec.sqrMagnitude < 1.0f) { return; }
        walkVec.x = mouseVec.x;
        walkVec.z = mouseVec.y;

    }

    void CheckSizeUp(int eatenCount)
    {
        if (base.size > maxSize) { return; }
        int eatenCountToNextSize = Variables.playerSizes[base.size].eatenCountToNextSize;
        if (eatenCount < eatenCountToNextSize) { return; }

        size++;
        transform.localScale += Vector3.one;
        walkSpeed += 100;
    }

    public static float Vector2ToDegree(Vector2 vec)
    {
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
    }
}
