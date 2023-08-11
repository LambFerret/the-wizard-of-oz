using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static ChangeCharacter;

// 나무꾼 이동 구현
// 장애물 파괴
// 용암 건너기
public class WoodcutterMovement : MonoBehaviour
{
    public float speed = 2.5f;

    private ChangeCharacter character;

    void Start()
    {
        character = transform.parent.gameObject.GetComponent<ChangeCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        
    }

    void Jump()
    {

    }

    // 장애물 파괴 스킬
    void ObjectDestroy()
    {

    }

    // 용암 건너기 스킬
    void LavaCross()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Tile"))
        {
            character.transform.GetChild((int)character.currentState).gameObject.SetActive(false);
            character.transform.GetChild((int)character.nextState).gameObject.SetActive(true);

            character.currentState = character.nextState;
            if (character.currentState == characterState.LION)
                character.nextState = characterState.DOROTHY;
            else
                character.nextState = character.currentState + 1;
        }
    }
}
