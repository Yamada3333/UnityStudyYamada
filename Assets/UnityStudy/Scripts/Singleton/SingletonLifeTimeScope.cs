using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UnityStudy.Scripts.Singleton
{
    public class SingletonLifeTimeScope : LifetimeScope
    {
        [SerializeField] private Singleton06 singleton06;
            
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(singleton06);
            builder.Register<Singleton07>(Lifetime.Singleton);
        }
        
        
        // vcontainer
        // https://package.openupm.com
        // jp.hadashikick.vcontainer
        
        // ただのクラスをシングルトンのように扱うことができる
        // ただし、シーンをまたいで値を保持することはできない
    }
}
