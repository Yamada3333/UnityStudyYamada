using UnityEngine;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton04 : Singleton<Singleton04>
    {
        public override void DebugLog()
        {
            Debug.Log(name);
        }
        
        // どんなクラスでもシングルトンにすることができる
        // SoundManager, ConfigManager, SaveDataManager, etc.
    }
}
