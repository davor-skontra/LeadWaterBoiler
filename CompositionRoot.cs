using UnityEngine;

namespace AlkarInjector
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        protected abstract void Main();
        private void Start()
        {
            Main();
        }
    }
}
