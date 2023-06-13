using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 기본 컴포넌트")]
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_2DBox;
    private Animator m_Anim;

    [SerializeField] private Vector3 m_moveDir;
    [SerializeField] private float m_playermovespeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_2DBox = GetComponent<BoxCollider2D>();
        m_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moving();
        checkAnim();
    }
    
    private void moving()
    {
        m_moveDir.x = Input.GetAxisRaw("Horizontal");
        if (m_moveDir.x == 1f)
        {
            transform.position += Vector3.right * m_playermovespeed * Time.deltaTime;
            transform.localScale = new Vector3(3f, 3f, 3f); 
        }
        else if (m_moveDir.x == -1f)
        {
            transform.position += Vector3.left * m_playermovespeed * Time.deltaTime;
            transform.localScale = new Vector3(-3f, 3f, 3f);
        }
        
    }
    private void checkAnim()
    {
        m_Anim.SetBool("move", m_moveDir.x != 0);
        m_Anim.SetBool("attack", Input.GetKeyDown(KeyCode.Z));
    }

    private void Attack()
    {
        
    }
}
