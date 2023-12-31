using System.Collections;
using System.Collections.Generic;
using character;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Character;

// 양철 나무꾼
public class WoodcutterMovement : IAbility
{

    public string Name { get; }

    public WoodcutterMovement()
    {
        Name = CharacterState.WOODCUTTER.ToString();
    }

    public void Action(Character character)
    {
        // 질량 2.0로 변경 -> 무거운 타일 이동 가능
        character.rb.mass = 2.0f;
    }

    public void Init(Character character)
    {
        // throw new System.NotImplementedException();
        character.rb.mass = 1.0f;
    }
}
