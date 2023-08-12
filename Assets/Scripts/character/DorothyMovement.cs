using character;


public class DorothyMovement : IAbility
{

    public string Name { get; }

    public DorothyMovement()
    {
        Name = Character.CharacterState.DOROTHY.ToString();
    }

    public void Action(Character character)
    {
        character.possibleJump = 2;
    }

    public void Init(Character character)
    {
        character.possibleJump = 1;
    }
}