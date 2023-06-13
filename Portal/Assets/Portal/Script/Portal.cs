using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Portal : MonoBehaviour
{
    [SerializeField, Header("�����Е��̃|�[�^��")]
    Portal OtherPortal;
    [SerializeField, Header("�v���C���[")]
    PlayerController Player;
    [SerializeField, Header("����")]
    Vector3 Direction;

    bool m_isWarp = false;      //���[�v�������ǂ����B


    /// <summary>
    /// ���[�v�������ǂ�����ݒ�B
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
