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
        float diff = Vector3.SqrMagnitude(Player.transform.position - this.transform.position);

        if (diff < Mathf.Pow(2.0f, 2.0f))
        {
            if (!m_isWarp)
            {
                Player.transform.position = OtherPortal.transform.position;
                Player.GetPlayerCamera().SetPlayerDirection(Direction);
                OtherPortal.SetIsWarp(true);
            }
            Debug.Log("a");
        }
        else
        {
            m_isWarp = false;
        }
    }
}
