using UnityEngine;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton05
    {
        private static Singleton05 _instance;
        public static Singleton05 Instance
        {
            get
            {
                if(_instance is null)
                {
                    _instance = new Singleton05();
                }

                return _instance;
            }
        }
        
        // 一行で書くこともできる
        // public static Singleton05 Instance => _instance ??= new Singleton05();
        
        // コンストラクタをprivateにすることで外部からのインスタンス生成を禁止
        private Singleton05()
        {
        }

        public void DebugLog()
        {
            Debug.Log("Singleton 05");
        }
        
        // MonoBehaviourを継承していないため、シーンに配置する必要がない
        // 他のシーンに遷移した場合はインスタンスが破棄される
    }
}
