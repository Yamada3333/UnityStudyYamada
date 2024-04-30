using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityStudy.Scripts.AsyncAwait
{
    [RequireComponent(typeof(MonsterAnimation))]
    public class Monster : MonoBehaviour
    {
        private MonsterAnimation monsterAnimation;

        private void Awake()
        {
            monsterAnimation = GetComponent<MonsterAnimation>();
        }
        
        public void Attack()
        {
            monsterAnimation.AttackAnimation();
        }
    }
}
