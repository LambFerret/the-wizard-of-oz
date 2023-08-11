using character;
using static Character;

// ����ƺ� �̵� ����
// ���� ���
public class ScarecrowMovement : IAbility
{
    public string Name { get; }

    public ScarecrowMovement()
    {
        Name = CharacterState.SCARECROW.ToString();
    }

    public void Action(Character character)
    {
        // 허수아비의 어빌리티는 은신
        //startToStealth = Time.time;
        //scarecrowCollider.enabled = false;

        //// ���� ���� �ð��� ������ �ٽ� Enemy�� �ݶ��̴��� �浹 ó��
        //if (startToStealth + timeToStealth < Time.time)
        //{
        //    scarecrowCollider.enabled = true;
        //}
    }

    public void Init(Character character)
    {

    }


}
