using System.Collections;
using System.Collections.Generic;
using character;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Character;

// ������ �̵� ����
// ��ֹ� �ı�
// ��� �ǳʱ�
public class WoodcutterMovement : IAbility
{
    public string Name { get; }

    public WoodcutterMovement()
    {
        Name = CharacterState.WOODCUTTER.ToString();
    }
    public void Action(Character character)
    {
        // throw new System.NotImplementedException();
    }

    public void Init(Character character)
    {
        // throw new System.NotImplementedException();
    }
}
