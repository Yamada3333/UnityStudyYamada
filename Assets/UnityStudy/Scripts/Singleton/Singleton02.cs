using UnityEngine;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton02 : MonoBehaviour
    {
        public static Singleton02 Instance { get; private set; }

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                // シーンを切り替えてもオブジェクトが破棄されないようにする
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void DebugLog()
        {
            Debug.Log(name);
        }
        
        // シーン遷移してもオブジェクトが破棄されないようにする
        // どこのシーンでも使いたい場合に使用する
        // 最初にシーンに配置する必要がある
    }
}
