using Infrastructure.GameSystem;


namespace Game.Components
{
    internal interface INeedTickableProcessor
    {
        void SetTickableProcessor(TickableProcessor processor);
    }
}
