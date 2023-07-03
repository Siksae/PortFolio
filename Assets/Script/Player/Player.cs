using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 기본 컴포넌트")] //플레이어의 기본 컴포넌트의 정의
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_playerCollBox2D;
    private Animator m_Animator;
    private Animation m_Animation;
    [SerializeField] private Transform m_trsobj;
    private GameManager gameManager;
    private Collider2D[] m_Coll2D;

    [Header("스크립트")]
    [SerializeField] private Playerfx Playerfx;

    [Header("플레이어 공격")] //플레이어 공격의 변수  
    private bool m_Attack; //공격키 입력변수
    public bool m_doAttack; //공격을 하고 있는지
    private float m_doAttacktimer = 1f;
    private float m_attackSpeed = 1f;


    [Header("플레이어 이동")] //플레이어 이동 변수
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
    [SerializeField] private float m_playermovespeedBasic = 5f;  //초기 대쉬 속도
    private float m_playerMoveSpeed;
    private float m_playermovespeedlimit = 10f; //대시 속도 리밋
    private float m_playerMoveSpeedLowLimit = 2f; //대시 속도 리밋
    [SerializeField] private Vector3 m_moveDir;
    private Vector3 m_checkDir;
    private bool m_isRight;

    [Header("플레이어 점프")] //플레이어 점프의 변수 : 벽 인식, 땅 인식, 점프 스피드 (gravity)
    private bool m_jump; //jump
    private bool m_dojump;
    [SerializeField] private bool m_dowallgrap;
    private bool m_wallgrap;
    private float m_jumppower = 7f;
    private float m_gravity;
    [SerializeField] private bool m_checkGround = true;
    [SerializeField] private bool m_checkWall;

    [Header("플레이어 벽점프")] //플레이어 벽점프의 변수 : 벽 인식, 땅 인식, 벽이면 반대로 점프, ANIM(JUMPUP) 

    [Header("플레이어 벽잡기")] //플레이어 벽잡기 변수 : 벽 인식, 땅 인식, 벽의 모서리 인식, ANIM(JUMPUP, JUMPDOWN), 벽을 잡고있을때는 다른 동작 Lock

    [Header("플레이어 회피")] //플레이어 회피 변수 : 무적 시간, 무적 여/부, 무적 타이머, ANIM 추가
    private bool m_dodge;
    private bool m_invin;
    private float m_invinTime;
    private float m_invinTimer;
    private float m_shiftTime; //대쉬, 회피 판별 변수

    [Header("플레이어 상태")]
    private float m_PlayerHP;
    private float m_PlayerMP;
    private float m_PlayerSP;
    

    void Start()
    {
        m_playerMoveSpeed = m_playermovespeedBasic;
        m_rigid = GetComponent<Rigidbody2D>();
        m_playerCollBox2D = GetComponent<BoxCollider2D>();
        m_Animator = GetComponent<Animator>();
        m_Animation = GetComponent<Animation>();
        m_trsobj = GetComponent<Transform>();
        gameManager = GameManager.Instance;
        m_Coll2D = GetComponentsInChildren<Collider2D>();

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
    //===============================================================
    private void checkCamera() //Camera가 Player를 따라다닙니다.
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
    private void Dircheck() //플레이어 방향을 체크합니다.
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
    private void moving() //플레이어 움직임
    {
        if(m_doAttack == true || m_wallgrap == true)
        {
            return;
        }
        m_moveDir.x = Input.GetAxisRaw("Horizontal");
        if (m_moveDir.x != 0)
        {
            _moving = true;
            transform.localScale = new Vector3(m_moveDir.x * 3f, 3f, 3f);
            if (m_checkWall == true) { transform.position = transform.position; return; }
            
            transform.position += m_moveDir * m_playerMoveSpeed * Time.deltaTime;       
            
        }
        else
        {
            _moving = false;
        }
    }

    private void Attack() // 플레이어 공격 함수
    {
        if (Input.GetKey(KeyCode.Z))
        {
            m_Attack = true;
            CollOnOff(HitBox.e_hitType.Attack, true);
        }
        else if (!Input.GetKey(KeyCode.Z))
        {
            if (m_Attack == true) //공격할 때 공격키를 누를 시 끊김방지
            {
                m_doAttacktimer -= 0.1f;
                if (m_doAttacktimer < 0f)
                {
                    m_doAttacktimer = 1f;
                    m_Attack = false;
                }
            }
            checkAttack();

        }
    }

    private void checkAttack()
    {
        if (m_doAttack == false)
        {
            CollOnOff(HitBox.e_hitType.Attack, false);
        }
        
    }
    private void dashing() // 플레이어 대쉬 함수
    {
        if (Input.GetKey(KeyCode.LeftShift) && m_dojump == false && _moving == true)
        {
            _dashing = true;

            if (m_playermovespeedlimit > m_playerMoveSpeed && _dashing)
            {
                if (m_playerMoveSpeed == m_playermovespeedBasic)
                {
                    gameManager.spawnDashingStratFx();
                }
                //GameObject obj = Instantiate()
                m_playerMoveSpeed += m_playerMoveSpeed * Time.deltaTime; // 대시 스피드 점점 빨라짐
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || m_moving == false)
        {
            _dashing = false;
            m_playerMoveSpeed = m_playermovespeedBasic; // 원복
        }
    }
    private void jump()
    {
        m_gravity = m_rigid.velocity.y;
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (m_checkGround == true || (m_checkGround == false && m_wallgrap == true))
            {
                m_playerMoveSpeed = m_playermovespeedBasic;
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
    public void CollCheck(HitBox.e_stateType _state, HitBox.e_hitType _hit, Collider2D _coll) //콜라이더 우선
    {
        switch (_hit)
        {
            case HitBox.e_hitType.Ground:
                switch (_state)
                {
                    case HitBox.e_stateType.Enter:
                        if (_coll.name == "Ground")
                        {
                            m_checkGround = true;
                            m_rigid.velocity = Vector2.zero;
                            m_dojump = false;
                        }
                        else if (_coll.tag.Equals("Enemy"))
                        {
                            m_rigid.velocity = Vector2.up * m_jumppower;
                        }
                        CollOnOff(HitBox.e_hitType.WallGrap, false);
                        break;
                    case HitBox.e_stateType.Stay:
                        break;
                    case HitBox.e_stateType.Exit:
                        m_checkGround = false;
                        m_dojump = true;
                        CollOnOff(HitBox.e_hitType.WallGrap, true);
                        break;
                }
                break;
            case HitBox.e_hitType.Wall:
                switch (_state)
                {
                    case HitBox.e_stateType.Enter:
                        m_checkWall = true;
                        break;
                    case HitBox.e_stateType.Stay:
                        break;
                    case HitBox.e_stateType.Exit:
                        m_checkWall = false;
                        break;
                }
                break;
            case HitBox.e_hitType.Hit:
                switch (_state)
                {
                    case HitBox.e_stateType.Enter:
                        break;
                    case HitBox.e_stateType.Stay:
                        break;
                    case HitBox.e_stateType.Exit:
                        break;
                }
                break;
            case HitBox.e_hitType.Attack:
                switch (_state)
                {
                    case HitBox.e_stateType.Enter:
                        break;
                    case HitBox.e_stateType.Stay:
                        break;
                    case HitBox.e_stateType.Exit:
                        break;
                }
                break;
            case HitBox.e_hitType.WallGrap:
                switch (_state)
                {
                    case HitBox.e_stateType.Enter:
                        if (_coll.name == "Ground")
                        {
                            m_dowallgrap = true;
                        }
                        break;
                    case HitBox.e_stateType.Stay:
                        break;
                    case HitBox.e_stateType.Exit:
                        m_dowallgrap = false;
                        break;
                }
                break;
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
    public void CollOnOff(HitBox.e_hitType _Name, bool _bool)
    {
        int count = m_Coll2D.Length;
        for (int iNum = count - 1; iNum > 0; iNum--)
        {
            if (m_Coll2D[iNum].ToString().Contains(_Name.ToString()) == true)
            {
                m_Coll2D[iNum].enabled = _bool;
                break;
            }
        }
    }
    private void checkAnim()
    {
        m_Animator.SetBool("move", m_moving);
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
        m_Animator.SetFloat("playerMoveSpeed", m_playerMoveSpeed);
        m_Animator.SetFloat("playerAttackSpeed", m_attackSpeed);
    }
    private void aminNameCheck()
    {
            m_doAttack = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Attack") == true ? true : false;
            m_dodge = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Dodge") == true? true : false;
            m_wallgrap = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Grap") == true ? true : false;
    }
}
