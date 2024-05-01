using UnityEngine;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton03 : MonoBehaviour
    {
        private static Singleton03 _instance;

        public static Singleton03 Instance
        {
            get
            {
                if (_instance is null)
                {
                    SetupInstance();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                // シーンを切り替えてもオブジェクトが破棄されないようにする
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static void SetupInstance()
        {
            // シーンの中に既に存在する場合はそれを使う
            _instance = FindObjectOfType<Singleton03>();

            if (_instance is null)
            {
                // シーンの中に存在しない場合は新しく作成する
                var gameObj = new GameObject
                {
                    name = "Singleton 03"
                };

                _instance = gameObj.AddComponent<Singleton03>();
                DontDestroyOnLoad(gameObj);
            }
        }

        public void DebugLog()
        {
            Debug.Log(name);
        }
        
        // シーンに存在しない場合は新しく作成する
        // インスペクター等で参照の設定が必要な場合は注意が必要
    }
}
