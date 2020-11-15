using UnityEngine;

namespace DependencyInjection
{
    public static class Injected
    {
        public static TMonoBehaviour Create<TMonoBehaviour>(TMonoBehaviour behaviour, Transform parent = null)
            where TMonoBehaviour : MonoBehaviour
        {
            var result = Object.Instantiate(behaviour, parent);
            foreach (var injectable in result.GetComponentsInChildren<MonoBehaviour>())
            {
                IocContainer.Inject(injectable);
            }

            return result;
        }
        
        public static GameObject Create(GameObject original, Transform parent = null)
        {
            var result = Object.Instantiate(original, parent);
            
            foreach (var injectable in result.GetComponentsInChildren<MonoBehaviour>())
            {
                IocContainer.Inject(injectable);
            }

            return result;
        }
    }
}