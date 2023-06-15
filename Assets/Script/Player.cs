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

    [Header("플레이어 기본 컴포넌트")] //플레이어의 기본 컴포넌트의 정의
    private Rigidbody2D m_rigid;
    private BoxCollider2D m_2DBox;
    private Animator m_Animator;
    private Animation m_Animation;
    private Transform m_trs;

    [Header("플레이어 공격")] //플레이어 공격의 변수  
    private bool m_doAttack;
    private float m_doAttacktimer;
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
    [SerializeField] private float m_playermovespeed = 5f;
    private float m_playermovespeedlimit = 10f;
    [Header("플레이어 점프")] //플레이어 점프의 변수 : 벽 인식, 땅 인식, 점프 스피드 (gravity)

    [Header("플레이어 벽점프")] //플레이어 벽점프의 변수 : 벽 인식, 땅 인식, 벽이면 반대로 점프, ANIM(JUMPUP) 

    [Header("플레이어 벽잡기")] //플레이어 벽잡기 변수 : 벽 인식, 땅 인식, 벽의 모서리 인식, ANIM(JUMPUP, JUMPDOWN), 벽을 잡고있을때는 다른 동작 Lock

    [Header("플레이어 회피")] //플레이어 회피 변수 : 무적 시간, 무적 여/부, 무적 타이머, ANIM 추가

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

    private void Attack() // 플레이어 공격 함수
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

    private void dashing() // 플레이어 대쉬 함수
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _dashing = true;
            if (m_playermovespeedlimit > m_playermovespeed)
            {
                m_playermovespeed += m_playermovespeed * Time.deltaTime; // 대시 스피드 점점 빨라짐
            }
        }
        else
        {
            _dashing = false;
            m_playermovespeed = 5f; // 원복
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
