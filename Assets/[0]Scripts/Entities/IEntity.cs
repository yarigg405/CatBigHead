namespace Game.Entities
{
    internal interface IEntity
    {
        T GetEntityComponent<T>();

        bool TryGetEntityComponent<T>(out T element);
    }
}