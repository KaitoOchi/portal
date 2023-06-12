using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool m_isGround = true;    //�n�ʂɐݒu���Ă��邩�ǂ����B


    /// <summary>
    /// �n�ʂɐݒu���Ă��邩�ǂ������擾�B
    /// </summary>
    /// <returns></returns>
    public bool GetIsGround()
    {
        return m_isGround;
    }

    /// <summary>
    /// �R���W�����ɐG�ꂽ�u�Ԃɓ���֐��B
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
    /// �R���W�������痣�ꂽ�u�Ԃɓ���֐��B
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
