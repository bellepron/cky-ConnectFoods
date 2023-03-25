using UnityEngine;

namespace CKY.Pooling
{
    public class SingletonPersistent<T> : MonoBehaviour where T : class
    {
        private static T _instance;
        private void Awake()
        {
            OnPerAwake();

            if (_instance == null)
            {
                _instance = GetComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            OnFirstAwake();
        }
        public static T Instance
        {
            get => (T)_instance;
        }

        protected virtual void OnPerAwake() { }

        protected virtual void OnFirstAwake() { }
    }
}