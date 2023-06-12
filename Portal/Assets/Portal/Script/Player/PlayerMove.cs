using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    float MoveSpeed = 0.1f;
    [SerializeField, Header("ジャンプ量")]
    float JumpPower = 5.0f;
    [SerializeField, Header("重力")]
    Vector3 Gravity;
    [SerializeField, Header("GroundCheck")]
    GroundCheck Ground_Check;


    Rigidbody           m_rigidbody;            //Rigidbody。

    GameObject          m_camera;               //カメラ。
    PlayerController    m_playerController;     //プレイヤーコントローラー。

    Vector2             m_moveDirection;        //移動方向。
    Vector3             m_moveSpeed;            //移動速度。      
    bool                m_isInputMove = false;  //移動入力しているかどうか。
    bool                m_isJump = false;       //ジャンプしているかどうか。
    float               m_moveTimer = 0.0f;     //移動時のタイマー。


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyを取得。
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.useGravity = false;

        //PlayerControllerを取得。
        m_playerController = GetComponent<PlayerController>();

        //子オブジェクトのカメラを取得。
        m_camera = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MoveY();
    }

    private void FixedUpdate()
    {
        if (!m_playerController.GetCursorLock())
        {
            return;
        }

        MoveX();
    }

    void MoveX()
    {
        //キーの入力から値を入手。
        if (Ground_Check.GetIsGround())
        {
            m_moveDirection = Vector2.zero;

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.01f)
            {
                m_moveDirection.y = Input.GetAxisRaw("Vertical");
                m_isInputMove = true;
            }

            //m_moveDirection = Vector2.zero;
            //m_moveDirection.y = Input.GetAxisRaw("Vertical");
            //m_moveDirection.x = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.01f)
            {
                m_moveDirection.x = Input.GetAxisRaw("Horizontal");
                m_isInputMove = true;
            }

            //移動入力されてないなら
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.01f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            {
                //m_moveDirection.x -= 0.1f;
                //m_moveDirection.y -= 0.1f;
                m_moveTimer = 0.0f;
                m_isInputMove = false;
            }

            //m_moveDirection.x = Mathf.Clamp(m_moveDirection.x, 2.0f, 0.0f);
            //m_moveDirection.y = Mathf.Clamp(m_moveDirection.y, 2.0f, 0.0f);

            //地面に触れている間、減速。
            m_moveTimer -= (m_moveTimer * 5.0f) * Time.deltaTime;
            m_moveTimer = Mathf.Max(1.0f, m_moveTimer);
        }
        else
        {
            //移動速度を乗算。
            m_moveTimer += Time.deltaTime;
            m_moveTimer = Mathf.Max(1.0f, m_moveTimer);
        }

        Debug.Log(m_moveTimer);

        if (m_isInputMove)
        {

        }
        else
        {

        }

        //カメラの前方向と右方向を取得。
        Vector3 forward = m_camera.transform.forward;
        Vector3 right = m_camera.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;

        right *= m_moveDirection.x;
        forward *= m_moveDirection.y;

        // 移動速度に上記で計算したベクトルを加算する。
        m_moveSpeed = Vector3.zero;
        m_moveSpeed += right + forward;

        //斜め移動を早くしないよう正規化する。
        m_moveSpeed.Normalize();
        m_moveSpeed *= MoveSpeed * m_moveTimer;

        //移動させる。
        m_rigidbody.velocity = new Vector3(m_moveSpeed.x, m_rigidbody.velocity.y, m_moveSpeed.z);
    }

    void MoveY()
    {
        //地面に接地しているなら、ジャンプ可能。
        if(Ground_Check.GetIsGround())
        {
            Jump();
        }
        //接地していないなら、重力を与える。
        else
        {
            m_rigidbody.AddForce(Gravity, ForceMode.Acceleration);
            m_isJump = false;
        }
    }

    /// <summary>
    /// ジャンプ処理。
    /// </summary>
    void Jump()
    {
        //Spaceが押されたら。
        if (Input.GetKey(KeyCode.Space) && !m_isJump)
        {
            m_rigidbody.AddForce(new Vector3(0.0f, JumpPower, 0.0f),　ForceMode.Impulse);
            m_isJump = true;
        }
    }
}
