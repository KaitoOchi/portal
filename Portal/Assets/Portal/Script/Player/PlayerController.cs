using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMove m_playerMove;
    PlayerCamera m_playerCamera;

    bool m_cursorLock = true;   //�J�[�\���̃��b�N���


    /// <summary>
    /// �v���C���[�J�������擾�B
    /// </summary>
    /// <returns></returns>
    public PlayerCamera GetPlayerCamera()
    {
        return m_playerCamera;
    }

    /// <summary>
    /// �J�[�\���̃��b�N��Ԃ��擾�B
    /// </summary>
    /// <returns></returns>
    public bool GetCursorLock()
    {
        return m_cursorLock;
    }


    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�ɂ��Ă���R���|�[�l���g���擾�B
        m_playerMove = GetComponent<PlayerMove>();
        m_playerCamera = GetComponent<PlayerCamera>();

        //�J�[�\�������b�N��Ԃɂ���B
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
            //Esc�������ꂽ�烍�b�N�����B
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_cursorLock = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            //�}�E�X�̓��͂Ń��b�N�B
            if (Input.GetMouseButton(0))
            {
                m_cursorLock = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
