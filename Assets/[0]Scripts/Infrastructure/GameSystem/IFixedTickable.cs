namespace Infrastructure.GameSystem
{
    public interface IFixedTickable
    {
        void FixedTick(float fixedDeltaTime);
    }
}
