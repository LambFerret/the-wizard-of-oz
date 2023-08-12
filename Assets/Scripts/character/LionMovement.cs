using System.Collections;
using System.Collections.Generic;
using character;
using UnityEngine;
using static Character;
using UnityEngine.TextCore.Text;

// 사자
public class LionMovement : IAbility
{
    public bool isDash = true;

    public float startToDash = 0.0f;    
    public float timeToDash = 5.0f;   
    public float lastToDash = 0.0f;  
    public float dashToLimit = 5.0f; 
    public float dashSpeed= 1000.0f;   // 대쉬 속도

    public string Name { get; }

    public LionMovement()
    {
        Name = CharacterState.LION.ToString();
    }

    // 대쉬 스킬
    public void Action(Character character)
    {
        // 
        if (startToDash + timeToDash > Time.time)
        {
            startToDash = Time.time;

            character.rb.velocity = Vector2.zero;
            character.rb.AddForce(character.lookDirection * dashSpeed);
        }
        else if (startToDash + timeToDash < Time.time)
        {
            character.rb.AddForce(new Vector2(0, 0));
            lastToDash = Time.time;
        }
    }

    public void Init(Character character)
    {

    }
}
