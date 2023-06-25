using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Player player;
    [SerializeField] private e_hitType e_HitType; //��Ʈ Ÿ���� �Է�
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
        player = GetComponentInParent<Player>(); //�� ������Ʈ �� �з��� �߿�!!!!! �θ��� ������Ʈ�� �ҷ���
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.CollCheck(e_stateType.Enter, e_HitType, collision); //onTriggerEnter�� �۵� (e_StateType�� �ް� ������ HitType ������ ����, collision data�� �Ѿ
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.CollCheck(e_stateType.Exit, e_HitType, collision);
    }

}
