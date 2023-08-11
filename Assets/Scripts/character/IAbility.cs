namespace character
{
    public interface IAbility
    {

        string Name { get; }
        void Action(Character character);

        void Init(Character character);
    }
}