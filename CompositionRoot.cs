using UnityEngine;

namespace AlkarInjector
{
    public abstract class CompositionRoot : MonoBehaviour
    {
        private void Start()
        {
            Main();
        }

        protected abstract void Main();
    }
}