using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 기본 컴포넌트")]
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_2DBox;
    private Animator m_Anim;

    private Vector3 m_moveDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moving();
    }

    private void moving()
    {
            m_moveDir.x = Input.GetAxisRaw("Horizontal");
            m_Anim.SetBool("Move", m_moveDir.x != 0);
            

        }
    }
}
