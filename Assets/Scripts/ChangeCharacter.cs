using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ٴۿ� ���� �� ĳ���� ����
public class ChangeCharacter : MonoBehaviour
{
    // ���� ĳ���� ����
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
