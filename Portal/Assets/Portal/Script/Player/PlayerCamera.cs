using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField, Header("�J�������x")]
    Vector2 Sensityvity = new Vector2(3.0f, 3.0f);
    [SerializeField, Header("�J������]�̏��"), Tooltip("x���ŏ��l�Ay���ő�l")]
    Vector2 CameraLimit = new Vector2(-90.0f, 90.0f);


    GameObject          m_camera;              //�J�����I�u�W�F�N�g�B
    PlayerController    m_playerController;    //�v���C���[�R���g���[���[�B

    Quaternion          m_cameraRot;           //�J�����̉�]�B
    Quaternion          m_characterRot;        //�v���C���[�̉�]�B


    // Start is called before the first frame update
    void Start()
    {
        //PlayerController���擾�B
        m_playerController = GetComponent<PlayerController>();

        //�q�I�u�W�F�N�g�̃J�������擾�B
        m_camera = transform.GetChild(0).gameObject;

        //���g�ƃJ�����̉�]���擾�B
        m_cameraRot = m_camera.transform.localRotation;
        m_characterRot = transform.localRotation;
    }

    private void LateUpdate()
    {
        if (!m_playerController.GetCursorLock())
        {
            return;
        }

        //�}�E�X�̓��͂��擾�B
        float xRot = Input.GetAxis("Mouse X") * Sensityvity.x;
        float yRot = Input.GetAxis("Mouse Y") * Sensityvity.y;

        m_cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        m_characterRot *= Quaternion.Euler(0, xRot, 0);

        ClampRotation();

        m_camera.transform.localRotation = m_cameraRot;
        transform.localRotation = m_characterRot;
    }

    /// <summary>
    /// ��]�̏����ݒ�B
    /// </summary>
    void ClampRotation()
    {
        m_cameraRot.x /= m_cameraRot.w;
        m_cameraRot.y /= m_cameraRot.w;
        m_cameraRot.z /= m_cameraRot.w;
        m_cameraRot.w = 1.0f;

        float angleX = Mathf.Atan(m_cameraRot.x) * Mathf.Rad2Deg * 2.0f;
        angleX = Mathf.Clamp(angleX, CameraLimit.x, CameraLimit.y);

        m_cameraRot.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
    }
}
