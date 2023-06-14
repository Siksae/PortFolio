using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    class PlayerMovement
    {
        int time;
        int timer;

    }
    public static Player Instance;

    [Header("플레이어 기본 컴포넌트")]
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_2DBox;
    private Animator m_Anim;

    [Header("플레이어 공격")]
    private bool m_doAttack;
    private float m_doAttacktimer;
    private float m_attackSpeed = 0.5f;

    [Header("플레이어 점프")]

    [Header("플레이어 벽점프")]

    [Header("플레이어 벽잡기")]

    [Header("플레이어 벽점프")]

    [SerializeField] private Vector3 m_moveDir;
    [SerializeField] private float m_playermovespeed = 5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
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
        Attack();
        checkAnim();
    }
    
    private void moving()
    {
        if (m_doAttack == true)
        {
            return;
        }
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


    private void Attack()
    {
        if (m_moveDir.x != 0f)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (m_doAttack == true)
            {
                return;
            }
            m_doAttack = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z) || m_doAttack == true)
        {
            m_doAttacktimer += 0.1f;

            if (m_doAttacktimer > 2f)
            {
                m_doAttack = false;
                m_doAttacktimer = 0f;
            }              
        }
    }


    private void jump()
    {

    }

    private void gripwall()
    {

    }

    private void checkAnim()
    {
        m_Anim.SetBool("move", m_moveDir.x != 0);
        m_Anim.SetBool("doattack", m_doAttack);
        m_Anim.SetFloat("playerAttackSpeed", m_attackSpeed);
    }
}
