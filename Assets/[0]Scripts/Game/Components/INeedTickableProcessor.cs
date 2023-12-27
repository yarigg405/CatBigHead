using Infrastructure.GameSystem;


namespace Game.Components
{
    public interface INeedTickableProcessor
    {
        void SetTickableProcessor(TickableProcessor processor);
    }
}
