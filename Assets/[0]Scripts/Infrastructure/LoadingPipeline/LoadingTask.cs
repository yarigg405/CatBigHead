using UnityEngine;


namespace Infrastructure.LoadingPipeline
{
    public abstract class LoadingTask : MonoBehaviour
    {
        internal abstract void Do();
    }
}
