using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 바닦에 닿을 시 캐릭터 변경
public class ChangeCharacter : MonoBehaviour
{
    // 현재 캐릭터 상태
    public enum characterState : int
    {
        DOROTHY,
        SCARECROW,
        WOODCUTTER,
        LION
    }
    public characterState currentState = characterState.DOROTHY;
    public characterState nextState = characterState.SCARECROW;
}
