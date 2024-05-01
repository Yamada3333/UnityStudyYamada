using VContainer;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton07
    {
        [Inject]
        public Singleton07()
        {
        }
        
        public void DebugLog()
        {
            UnityEngine.Debug.Log("Singleton 07");
        }
    }
}
