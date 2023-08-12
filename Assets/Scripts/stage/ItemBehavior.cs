using System;
using DG.Tweening;
using UnityEngine;

namespace stage
{
    public class ItemBehavior : MonoBehaviour
    {
        public float movementDistance = 1f;
        public float movementDuration = 1f;

        private void Awake()
        {
            // 위로 움직이는 트윈
            Tweener moveUp = transform.DOLocalMoveY(transform.localPosition.y + movementDistance, movementDuration);
            // 아래로 움직이는 트윈
            Tweener moveDown = transform.DOLocalMoveY(transform.localPosition.y, movementDuration);

            // 무한 반복을 위해 순차적인 트윈 시퀀스 생성
            Sequence sequence = DOTween.Sequence();
            sequence.Append(moveUp);
            sequence.Append(moveDown);
            sequence.SetLoops(-1); // -1을 설정하여 무한 반복

        }
    }
}