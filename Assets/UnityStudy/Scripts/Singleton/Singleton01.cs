using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStudy.Scripts.Singleton
{
    public class Singleton01 : MonoBehaviour
    {
        // シングルトンとは、インスタンスが1つしか存在しないことを保証するデザインパターン
        
        
        // シングルトンインスタンス
        // 外部から参照できるようにするためにpublic staticで宣言
        // private setで外部からの変更を禁止
        public static Singleton01 Instance { get; private set; }

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                // 既にインスタンスが存在する場合は削除
                Destroy(gameObject);
            }
        }
        
        
        // 変数も同様にpublicで宣言
        // private setで外部からの変更を禁止したい（必須ではない）
        public int Hp { get; private set; } = 100;
        
        // 外部からの変更を許可する場合は以下のようにしたい
        // 開発ツールによっては"使用箇所"のようにどこで変更されているかを確認できる可能性がある
        public void SetHp(int hp)
        {
            Hp = hp;
        }

        
        // インスペクターで設定したい。private で他のクラスから変更されないようにする
        [SerializeField] private int attackPower;
        
        public int GetAttackPower()
        {
            return attackPower;
        }
        
        // 最低限のシングルトン
        // シーン遷移するとオブジェクトが破棄される
        // 使用したいシーン全てに配置する必要がある
        
        
        
        
        
        
        public void SceneChange()
        {
            SceneManager.LoadScene("Singleton02Scene");
        }
    }
}
