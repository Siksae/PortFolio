using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Player player;
    [SerializeField] private e_hitType e_HitType; //히트 타입을 입력
    public enum e_hitType
    {
        Ground, Wall, Hit, Attack, WallGrap,
    }
    public enum e_stateType
    {
        Enter, Stay, Exit,
    }
    void Start()
    {
        player = GetComponentInParent<Player>(); //겟 컴포넌트 인 패런츠 중요!!!!! 부모의 컴포넌트를 불러옴
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.CollCheck(e_stateType.Enter, e_HitType, collision); //onTriggerEnter시 작동 (e_StateType을 받고 지정된 HitType 변수로 지정, collision data도 넘어감
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.CollCheck(e_stateType.Exit, e_HitType, collision);
    }

}
