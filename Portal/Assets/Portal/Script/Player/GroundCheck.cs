using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool m_isGround = true;    //地面に設置しているかどうか。


    /// <summary>
    /// 地面に設置しているかどうかを取得。
    /// </summary>
    /// <returns></returns>
    public bool GetIsGround()
    {
        return m_isGround;
    }

    /// <summary>
    /// コリジョンに触れた瞬間に入る関数。
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            m_isGround = true;
        }
    }

    /// <summary>
    /// コリジョンから離れた瞬間に入る関数。
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            m_isGround = false;
        }
    }
}
