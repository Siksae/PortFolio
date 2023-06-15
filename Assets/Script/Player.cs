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

    [Header("�÷��̾� �⺻ ������Ʈ")] //�÷��̾��� �⺻ ������Ʈ�� ����
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_2DBox;
    private Animator m_Animator;
    private Animation m_Animation;
    private Transform m_trs;

    [Header("�÷��̾� ����")] //�÷��̾� ������ ����  
    private bool m_doAttack;
    private float m_doAttacktimer;
    private float m_attackSpeed = 1f;

    [Header("�÷��̾� �̵�")] //�÷��̾� �̵� ����
    private bool _moving;
    private bool m_moving
    {
        get => _moving;

        set 
        {
            _moving = value;
        }
    }
    private bool _dashing;
    bool m_dashing
    {
        get => _dashing;
        set
        {
            _dashing = value;
        }
    }
    [SerializeField] private float m_playermovespeed = 5f;
    private float m_playermovespeedlimit = 10f;
    [Header("�÷��̾� ����")] //�÷��̾� ������ ���� : �� �ν�, �� �ν�, ���� ���ǵ� (gravity)

    [Header("�÷��̾� ������")] //�÷��̾� �������� ���� : �� �ν�, �� �ν�, ���̸� �ݴ�� ����, ANIM(JUMPUP) 

    [Header("�÷��̾� �����")] //�÷��̾� ����� ���� : �� �ν�, �� �ν�, ���� �𼭸� �ν�, ANIM(JUMPUP, JUMPDOWN), ���� ����������� �ٸ� ���� Lock

    [Header("�÷��̾� ȸ��")] //�÷��̾� ȸ�� ���� : ���� �ð�, ���� ��/��, ���� Ÿ�̸�, ANIM �߰�

    [SerializeField] private Vector3 m_moveDir; 

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
        m_Animator = GetComponent<Animator>();
        m_Animation = GetComponent<Animation>();
        m_trs = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        moving();
        Attack();
        dashing();
        checkAnim();
    }
    
    private void moving()
    {
        if (m_doAttack == true)
        {
            return;
        }
        m_moveDir.x = Input.GetAxisRaw("Horizontal");
        if (m_moveDir.x != 0)
        {
            transform.position +=  m_moveDir * m_playermovespeed * Time.deltaTime;
            transform.localScale = m_moveDir.x == 1f ? new Vector3(3f, 3f, 3f) : new Vector3(-3f, 3f, 3f);
        }
    }

    private void Attack() // �÷��̾� ���� �Լ�
    {
        if (Input.GetKey(KeyCode.Z))
        {
            
            if (!m_Animation.IsPlaying("Attack1"))
            {
                m_doAttack = true;
            }
            else
            {
                return; 
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.Z) && m_Animation.IsPlaying("Attack1"))
        {
            m_doAttack = false;
        }
    }

    private void dashing() // �÷��̾� �뽬 �Լ�
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _dashing = true;
            if (m_playermovespeedlimit > m_playermovespeed)
            {
                m_playermovespeed += m_playermovespeed * Time.deltaTime; // ��� ���ǵ� ���� ������
            }
        }
        else
        {
            _dashing = false;
            m_playermovespeed = 5f; // ����
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
        m_Animator.SetBool("move", m_moveDir.x != 0);
        m_Animator.SetBool("doattack", m_doAttack);
        m_Animator.SetBool("dodash", m_dashing);
        m_Animator.SetFloat("playerMoveSpeed", m_playermovespeed);
        m_Animator.SetFloat("playerAttackSpeed", m_attackSpeed);

    }
}
