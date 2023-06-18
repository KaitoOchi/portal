using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Portal : MonoBehaviour
{
    [SerializeField, Header("もう片方のポータル")]
    Portal OtherPortal;
    [SerializeField, Header("プレイヤー")]
    PlayerController Player;
    [SerializeField, Header("方向")]
    Vector3 Direction;
    [SerializeField, Header("カメラ")]
    Camera GameCamera;

    bool m_isWarp = false;      //ワープしたかどうか。


    /// <summary>
    /// ワープしたかどうかを設定。
    /// </summary>
    /// <param name="warp"></param>
    public void SetIsWarp(bool warp)
    {
        m_isWarp = warp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraUpdate();

        float diff = Vector3.SqrMagnitude(Player.transform.position - this.transform.position);

        if (diff < Mathf.Pow(2.0f, 2.0f))
        {
            if (!m_isWarp)
            {
                Warp();
            }
            Debug.Log("a");
        }
        else
        {
            m_isWarp = false;
        }
    }

    /// <summary>
    /// カメラの更新処理。
    /// </summary>
    void CameraUpdate()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 cameraPos = playerPos;

        //float angle = Mathf.Atan2(transform.position.z - playerPos.z, transform.position.z - playerPos.x);

        //GameCamera.transform.rotation = Quaternion.Euler(0.0f, -angle * 180.0f, 180.0f);

        //GameCamera.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        //GameCamera.transform.rotation = Quaternion.LookRotation(diff);

        GameCamera.transform.LookAt(cameraPos);
        GameCamera.transform.rotation *= Quaternion.Euler(1.0f, 1.0f, -180.0f);
    }

    /// <summary>
    /// ワープ時の処理。
    /// </summary>
    void Warp()
    {
        Vector3 diff = Player.transform.position - transform.position;

        Player.transform.position = OtherPortal.transform.position + diff;

        Player.GetPlayerCamera().SetPlayerDirection(Direction);

        OtherPortal.SetIsWarp(true);
    }
}
