using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMove m_playerMove;
    PlayerCamera m_playerCamera;

    bool m_cursorLock = true;   //カーソルのロック状態


    /// <summary>
    /// プレイヤーカメラを取得。
    /// </summary>
    /// <returns></returns>
    public PlayerCamera GetPlayerCamera()
    {
        return m_playerCamera;
    }

    /// <summary>
    /// カーソルのロック状態を取得。
    /// </summary>
    /// <returns></returns>
    public bool GetCursorLock()
    {
        return m_cursorLock;
    }


    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーについているコンポーネントを取得。
        m_playerMove = GetComponent<PlayerMove>();
        m_playerCamera = GetComponent<PlayerCamera>();

        //カーソルをロック状態にする。
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraLock();

        if (!m_cursorLock)
        {
            return;
        }
    }

    void UpdateCameraLock()
    {
        if (m_cursorLock)
        {
            //Escが押されたらロック解除。
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_cursorLock = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            //マウスの入力でロック。
            if (Input.GetMouseButton(0))
            {
                m_cursorLock = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
