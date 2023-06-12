using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField, Header("カメラ感度")]
    Vector2 Sensityvity = new Vector2(3.0f, 3.0f);
    [SerializeField, Header("カメラ回転の上限"), Tooltip("xが最小値、yが最大値")]
    Vector2 CameraLimit = new Vector2(-90.0f, 90.0f);


    GameObject          m_camera;              //カメラオブジェクト。
    PlayerController    m_playerController;    //プレイヤーコントローラー。

    Quaternion          m_cameraRot;           //カメラの回転。
    Quaternion          m_characterRot;        //プレイヤーの回転。


    // Start is called before the first frame update
    void Start()
    {
        //PlayerControllerを取得。
        m_playerController = GetComponent<PlayerController>();

        //子オブジェクトのカメラを取得。
        m_camera = transform.GetChild(0).gameObject;

        //自身とカメラの回転を取得。
        m_cameraRot = m_camera.transform.localRotation;
        m_characterRot = transform.localRotation;
    }

    private void LateUpdate()
    {
        if (!m_playerController.GetCursorLock())
        {
            return;
        }

        //マウスの入力を取得。
        float xRot = Input.GetAxis("Mouse X") * Sensityvity.x;
        float yRot = Input.GetAxis("Mouse Y") * Sensityvity.y;

        m_cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        m_characterRot *= Quaternion.Euler(0, xRot, 0);

        ClampRotation();

        m_camera.transform.localRotation = m_cameraRot;
        transform.localRotation = m_characterRot;
    }

    /// <summary>
    /// 回転の上限を設定。
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
