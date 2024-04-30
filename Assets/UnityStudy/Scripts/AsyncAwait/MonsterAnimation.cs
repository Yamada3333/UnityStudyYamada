using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;

namespace UnityStudy.Scripts.AsyncAwait
{
    public class MonsterAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject theMonster;
        private SkeletonAnimation monsterAnimator;

        private void Start()
        {
            var monster = Instantiate(theMonster, transform);
            
            var scalingFactor = GetScalingFactor(monster.name);
            monster.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f) * scalingFactor;
            monster.transform.localPosition = new Vector2(0.0f, -0.6f);
        
            monsterAnimator = monster.GetComponent<SkeletonAnimation>();
        }
    
        public void AttackAnimation() //Names are: Idle, Walk, Dead and Attack
        {
            monsterAnimator.AnimationState.SetAnimation(0, "Attack", false);
        }

        private static float GetScalingFactor(string monsterName)
        {
            var scalingFactors = new Dictionary<string, float>
            {
                {"Salamander", 1.3f},
                {"Yeti", 0.75f},
                {"Golem", 0.75f},
                {"Floating", 0.67f},
                {"Rabbit", 0.75f},
                {"Mushroom", 0.67f},
                {"Book", 0.67f},
                {"Corrupted", 1.7f},
                {"Ox", 1.25f},
                {"Raptor", 1.25f},
                {"Orc", 1.1f},
                {"Snail", 0.9f},
                {"Shell", 1.2f},
                {"Lizard", 2f},
                {"Hamy", 1.5f}
            };

            var scalingFactor = scalingFactors.FirstOrDefault(pair => monsterName.Contains(pair.Key)).Value;
            return scalingFactor != 0 ? scalingFactor : 1.0f;
        }
    }
}
