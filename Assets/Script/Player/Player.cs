using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� �⺻ ������Ʈ")] //�÷��̾��� �⺻ ������Ʈ�� ����
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_playerCollBox2D;
    private Animator m_Animator;
    private Animation m_Animation;
    private Collider2D[] m_objplayercheckColl;
    [SerializeField] private Transform m_trsobj;
    
    [Header("�÷��̾� ����")] //�÷��̾� ������ ����  
    private bool m_Attack; //����Ű �Էº���
    private bool m_doAttack; //������ �ϰ� �ִ���
    private float m_doAttacktimer = 1f;
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
    [SerializeField] private float m_playermovespeed = 5f;  //�ʱ� �뽬 �ӵ�
    private float m_playermovespeedlimit = 10f;             //��� �ӵ� ����
    [Header("�÷��̾� ����")] //�÷��̾� ������ ���� : �� �ν�, �� �ν�, ���� ���ǵ� (gravity)
    private bool m_jump; //jump
    private bool m_dojump;
    [SerializeField] private bool m_dowallgrap;
    private bool m_wallgrap;
    private float m_jumppower = 7f;
    private float m_gravity;
    [SerializeField] private bool m_checkGround = true;
    [SerializeField] private bool m_checkWall;

    [Header("�÷��̾� ������")] //�÷��̾� �������� ���� : �� �ν�, �� �ν�, ���̸� �ݴ�� ����, ANIM(JUMPUP) 

    [Header("�÷��̾� �����")] //�÷��̾� ����� ���� : �� �ν�, �� �ν�, ���� �𼭸� �ν�, ANIM(JUMPUP, JUMPDOWN), ���� ����������� �ٸ� ���� Lock

    [Header("�÷��̾� ȸ��")] //�÷��̾� ȸ�� ���� : ���� �ð�, ���� ��/��, ���� Ÿ�̸�, ANIM �߰�
    private bool m_dodge;
    private bool m_invin;
    private float m_invinTime;
    private float m_invinTimer;
    private float m_shiftTime; //�뽬, ȸ�� �Ǻ� ����

    [Header("�÷��̾� ����")]
    private float m_PlayerHP;
    private float m_PlayerMP;
    private float m_PlayerSP;
    
    [SerializeField] private Vector3 m_moveDir;
    private Vector3 m_checkDir;
    private bool m_isRight;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_playerCollBox2D = GetComponent<BoxCollider2D>();
        m_objplayercheckColl = GetComponentsInChildren<Collider2D>();
        m_Animator = GetComponent<Animator>();
        m_Animation = GetComponent<Animation>();
        m_trsobj = GetComponent<Transform>();
    }
    void Update()
    {
        checkCamera();
        Dircheck();
        moving();
        dashing();
        dodge();
        jump();
        gripwall();
        Attack();
        checkAnim();
        aminNameCheck();

    }
    private void checkCamera() //Camera�� Player�� ����ٴմϴ�.
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
    private void Dircheck() //�÷��̾� ������ üũ�մϴ�.
    {
        m_checkDir = transform.localScale;
        if (Mathf.Sign(m_checkDir.x) == 1)
        {
            m_isRight = true;
        }
        else
        {
            m_isRight = false;
        }
    }
    private void moving() //�÷��̾� ������
    {
        if(m_doAttack == true || m_wallgrap == true)
        {
            return;
        }
        m_moveDir.x = Input.GetAxisRaw("Horizontal");
        if (m_moveDir.x != 0 && m_checkWall == true)
        {
            transform.position +=  Vector3.zero;
            m_rigid.velocity = m_rigid.velocity;
            transform.localScale = m_moveDir.x == 1f ? new Vector3(3f, 3f, 3f) : new Vector3(-3f, 3f, 3f);
        }
        else if (m_moveDir.x != 0)
        {
            transform.position += m_moveDir * m_playermovespeed * Time.deltaTime;
            transform.localScale = m_moveDir.x == 1f ? new Vector3(3f, 3f, 3f) : new Vector3(-3f, 3f, 3f);
        }
        
    }

    private void Attack() // �÷��̾� ���� �Լ�
    {
        if (Input.GetKey(KeyCode.Z))
        {
            m_Attack = true;
        }
        else if (!Input.GetKey(KeyCode.Z))
        {
            if (m_Attack == true) //������ �� ����Ű�� ���� �� �������
            {
                m_doAttacktimer -= 0.1f;
                if (m_doAttacktimer < 0f)
                {
                    m_doAttacktimer = 1f;
                    m_Attack = false;
                }
            }
           
        }
    }
    private void dashing() // �÷��̾� �뽬 �Լ�
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _dashing = true;
            if (m_playermovespeedlimit > m_playermovespeed)
            {
                //GameObject obj = Instantiate()
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
        m_gravity = m_rigid.velocity.y;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (m_checkGround == true || (m_checkGround == false && m_wallgrap == true))
            {
                m_rigid.bodyType = RigidbodyType2D.Dynamic;
                m_rigid.velocity = Vector2.up * m_jumppower;
                m_jump = true;
                m_dojump = true;
                m_wallgrap = false;
            }
        }
        else
        {
            m_jump = false;
        }
    }
    private void Invin()
    {
        if (m_dodge == true)
        {
            m_invin = true;
        }
        else
        {
            m_invin = false;
        }

    }
    public void CollCheck(HitBox.e_stateType _state, HitBox.e_hitType _hit, Collider2D _coll) //�ݶ��̴� �켱
    {
        switch (_state)
        {
            case HitBox.e_stateType.Enter :
                switch (_hit)
                {
                    case HitBox.e_hitType.Ground:
                        m_checkGround = true;
                        m_dojump = false;
                        collOnOff(m_objplayercheckColl, HitBox.e_hitType.WallGrap, false);
                        break;
                    case HitBox.e_hitType.Wall:
                        m_checkWall = true;
                        break;
                    case HitBox.e_hitType.Hit:
                        break;
                    case HitBox.e_hitType.Attack:
                        break;
                    case HitBox.e_hitType.WallGrap:
                        m_dowallgrap = true;
                        break;
                }
                break;
            case HitBox.e_stateType.Stay:
                break;
            case HitBox.e_stateType.Exit:
                switch (_hit)
                {
                    case HitBox.e_hitType.Ground:
                        m_checkGround = false;
                        m_dojump = true;
                        collOnOff(m_objplayercheckColl, HitBox.e_hitType.WallGrap, true);
                        break;
                    case HitBox.e_hitType.Wall:
                        m_checkWall = false;
                        break;
                    case HitBox.e_hitType.Hit:
                        break;
                    case HitBox.e_hitType.Attack:
                        break;
                    case HitBox.e_hitType.WallGrap:
                            m_dowallgrap = false;
                        break;
                }
                break;
        }
    }

    private void collOnOff(Collider2D[] collider2Ds, HitBox.e_hitType _Name, bool _bool)
    {
        for (int iNum = collider2Ds.Length - 1; iNum > 0; iNum--)
        {
            if (collider2Ds[iNum].name == _Name.ToString())
            {
                collider2Ds[iNum].enabled = _bool;
            }
        }
    }
    private void gripwall()
    {
        if (m_wallgrap == true && m_dowallgrap == true)
        {
            m_rigid.bodyType = RigidbodyType2D.Static;
        }
    }

    private void dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (m_dodge == true)
            {
                return;
            }
            else
            {
                m_dodge = true;
            }
        }
        if (m_isRight == true && m_dodge == true)
        {
            transform.position += Vector3.right * 3  * Time.deltaTime ;
        }
        else if(m_isRight == false && m_dodge == true)
        {
            transform.position += Vector3.left * 3 * Time.deltaTime;
        }
        Invin();
    }

    private void checkAnim()
    {
        m_Animator.SetBool("move", m_moveDir.x != 0);
        m_Animator.SetBool("doattack", m_doAttack);
        m_Animator.SetBool("dodash", m_dashing);
        m_Animator.SetBool("Attack", m_Attack);
        m_Animator.SetBool("dododge", m_dodge);
        m_Animator.SetBool("Jump", m_jump);
        m_Animator.SetBool("dojump", m_dojump);
        m_Animator.SetBool("wallgrap", m_wallgrap);
        m_Animator.SetBool("doGrapWall", m_dowallgrap);
        m_Animator.SetBool("WallCheck", m_checkWall);
        m_Animator.SetBool("Ground", m_checkGround);
        m_Animator.SetFloat("gravity", m_gravity);
        m_Animator.SetFloat("playerMoveSpeed", m_playermovespeed);
        m_Animator.SetFloat("playerAttackSpeed", m_attackSpeed);
    }
    private void aminNameCheck()
    {
            m_doAttack = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack") == true ? true : false;
            m_dodge = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Dodge") == true? true : false;
            m_wallgrap = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Grap") == true ? true : false;
    }
}
