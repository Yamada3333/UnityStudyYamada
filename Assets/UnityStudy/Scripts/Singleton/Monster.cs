using UnityEngine;
using VContainer;

namespace UnityStudy.Scripts.Singleton
{
    public class Monster : MonoBehaviour
    {
        private Singleton06 singleton06;
        private Singleton07 singleton07;

        [Inject]
        public void Constructor(Singleton06 setSingleton06, Singleton07 setSingleton07)
        {
            singleton06 = setSingleton06;
            singleton07 = setSingleton07;
        }

        private void Start()
        {
            Singleton01.Instance.SetHp(50);
            Debug.Log(Singleton01.Instance.Hp);

            var singleton01AttackPower = Singleton01.Instance.GetAttackPower();
            Debug.Log(singleton01AttackPower);
            
            
            Singleton02.Instance.DebugLog();
            Singleton03.Instance.DebugLog();
            Singleton04.Instance.DebugLog();
            Singleton05.Instance.DebugLog();
            singleton06.DebugLog();
            singleton07.DebugLog();
        }
    }
}
