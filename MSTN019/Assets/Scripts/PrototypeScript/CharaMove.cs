using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMove : BaseMonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField]
    private float walkSpeed;
    private Animator animator;
    private bool Jump;
    private float JumpCount;
    private void awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        JumpCount = 0.0f;
        Jump = false;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void UpdateNormal()
    {
        if (characterController.isGrounded)
        {
            if(Jump)
            {
                JumpCount += Time.deltaTime;
            }
            else
            {
                JumpCount = 0.0f;
            }
            //JoyPadのサイト参考　https://hakonebox.hatenablog.com/entry/2018/04/15/125152
            //入力時の情報　https://qiita.com/RyotaMurohoshi/items/a5cde3c17831adda12db
            if (Input.GetKeyDown("joystick button 0"))
            {
                Jump = true;
                animator.SetBool("Jump", true);
            }
            else if (Input.GetKeyUp("joystick button 0"))
            {
                animator.SetBool("Jump", false);
            }

            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //Debug.Log(velocity);
            //Debug.Log(velocity.magnitude);

            //止まってる時、動いてる時でifを制御している、　アニメーションで（止まる、歩く、走る）があるが、Moveの数値によって切り替えるように設定している。
            if(velocity.x == 0 && velocity.z == 0)
            {
                animator.SetFloat("Move", 0f);
            }
            else if (velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Move", velocity.magnitude);
                transform.LookAt(transform.position + velocity);
            }
        }
        //ジャンプ中に制限した時間だけ移動できるように制御
        if(JumpCount < 0.7f)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(velocity * walkSpeed * Time.deltaTime);
        }
        //ジャンプアニメーションで着地した時に移動できないように設定。
        else if(JumpCount > 1.2f)
        {
            Jump = false;
        }
    }
}
