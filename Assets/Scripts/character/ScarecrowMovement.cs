using character;
using Unity.VisualScripting;
using UnityEngine;
using static Character;

// 허수아비 기능 
public class ScarecrowMovement : IAbility
{
    public float startTostealth;         // 은신 시작 시간
    public float timeToStealth = 3.0f;   // 은신 가능 시간
    public string Name { get; }

    public ScarecrowMovement()
    {
        Name = CharacterState.SCARECROW.ToString();
    }

    public void Action(Character character)
    {
        // 질량 0.5로 변경 -> 무거운 타일 이동 불가
        character._rb.mass = 0.5f;

        // 은신 -> Enemy와 충돌 피하기
        startTostealth = Time.time;

        //if (startTostealth + timeToStealth <= Time.time)
        //{
        //    character.isCrash = true;
        //}
    }

    public void Init(Character character)
    {
        character._rb.mass = 1.0f;
    }
}
