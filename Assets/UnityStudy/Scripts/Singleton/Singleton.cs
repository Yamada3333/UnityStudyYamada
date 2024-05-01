using UnityEngine;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = (T) FindObjectOfType(typeof(T));
                    if (_instance is null)
                    {
                        SetupInstance();
                    }
                }

                return _instance;
            }
        }

        public virtual void Awake()
        {
            RemoveDuplicates();
        }

        private static void SetupInstance()
        {
            _instance = (T)FindObjectOfType(typeof(T));

            if (_instance is null)
            {
                var gameObj = new GameObject
                {
                    name = typeof(T).Name
                };
                
                _instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }
    
        private void RemoveDuplicates()
        {
            if (_instance is null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public virtual void DebugLog()
        {
            Debug.Log(name);
        }
        
        // シーンに存在しない場合は新しく作成する
        // インスペクター等で参照の設定が必要な場合は注意が必要
    }
}
