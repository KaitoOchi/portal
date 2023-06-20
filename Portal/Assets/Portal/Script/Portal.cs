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
    }

    /// <summary>
    /// カメラの更新処理。
    /// </summary>
    void CameraUpdate()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 cameraPos = GameCamera.transform.position;
        Vector3 portalPos = OtherPortal.transform.position;

        //高さを合わせる。
        playerPos.y = cameraPos.y;
        portalPos.y = cameraPos.y;

        Vector3 diff = (cameraPos - playerPos);

        diff += portalPos;

        //プレイヤーとポータルの延長線上にカメラを向ける。
        GameCamera.transform.LookAt(diff);
        GameCamera.transform.localRotation *= Quaternion.Euler(1.0f, 180.0f, 180.0f);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!m_isWarp)
            {
                Warp();
            }
            Debug.Log(transform.forward);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_isWarp = false;
        }
    }
}
