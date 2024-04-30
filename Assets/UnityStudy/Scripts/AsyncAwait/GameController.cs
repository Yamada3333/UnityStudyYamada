using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStudy.Scripts.AsyncAwait
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private List<Monster> monsters;
        
        [SerializeField] private Button attackButton;
        [SerializeField] private Button attackAsyncButton;
        [SerializeField] private Button attackCoroutineButton;
        
        [SerializeField] private Button moveButton;
        [SerializeField] private Button moveSequenceButton;
        [SerializeField] private Button moveAsyncButton;
        [SerializeField] private Button stateButton;

        private void Start()
        {
            attackButton.onClick.AddListener(Attack);
            attackAsyncButton.onClick.AddListener(() => AttackAsync().Forget());
            attackCoroutineButton.onClick.AddListener(() => StartCoroutine(AttackCoroutine()));
            moveButton.onClick.AddListener(Move);
            moveSequenceButton.onClick.AddListener(MoveSequence);
            moveAsyncButton.onClick.AddListener(() => MoveAsync().Forget());
            stateButton.onClick.AddListener(() => MonsterStateController().Forget());
        }

        private void Attack()
        {
            foreach (var monster in monsters)
            {
                monster.Attack();
            }
        }
        
        private IEnumerator AttackCoroutine()
        {
            foreach (var monster in monsters)
            {
                monster.Attack();
                // 1秒待つ
                yield return new WaitForSeconds(Duration);
            }
        }

        private async UniTaskVoid AttackAsync()
        {
            foreach (var monster in monsters)
            {
                monster.Attack();
                // 1秒待つ
                await UniTask.WaitForSeconds(Duration);
            }
        }
        
        // async 非同期処理を行う宣言
        // await 処理が終わるまで"待機"
        // UniTaskVoid 非同期処理を行う際の戻り値の型(終了を待たない)
        // UniTask<T> 非同期処理を行う際の戻り値の型(終了を待つ)
        // Forget() 非同期処理を行う際に、戻り値を無視する
        
        /*
         * コルーチンのデメリット
         * MonoBehaviour を継承しているクラスでしか使用できない
         * 他の非同期処理と組み合わせにくい(ファイルの読み込み、オンライン通信など)
         * try-catch で例外処理ができない
         *
         * ※難しい処理を行いたくなるほど、コルーチンは使いづらくなる
         *
         * AsyncAwait のデメリット
         * UniTask を使用するため、ライブラリを追加する必要がある
         * https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask
         * 難しい
         */

        private void Move()
        {
            monsters[4].transform.DOMoveX(1.0f, Duration).SetRelative();
            // SetDelay(1.0f) 1秒待つ
            monsters[4].transform.DOMoveY(1.0f, Duration).SetDelay(Duration).SetRelative()
                // OnComplete でアニメーションが終了した後の処理を記述
                .OnComplete(() => monsters[4].transform.DOMove(originalPosition, Duration).SetRelative());
        }
        
        private void MoveSequence()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(monsters[4].transform.DOMoveX(1.0f, Duration).SetRelative());
            sequence.Append(monsters[4].transform.DOMoveY(1.0f, Duration).SetRelative());
            sequence.Append(monsters[4].transform.DOMove(originalPosition, Duration).SetRelative());
            sequence.Play();
        }

        private async UniTaskVoid MoveAsync()
        {
            // ディレイを考えず、上から順に直観的な記述が可能
            await monsters[4].transform.DOMoveX(1.0f, Duration).SetRelative().AsyncWaitForCompletion();
            await monsters[4].transform.DOMoveY(1.0f, Duration).SetRelative().AsyncWaitForCompletion();
            // 途中に処理を挟むことも容易
            monsters[4].Attack();
            await UniTask.WaitForSeconds(Duration);
            await monsters[4].transform.DOMove(originalPosition, Duration).SetRelative().AsyncWaitForCompletion();
        }

        
        // 状態遷移に使用できる
        // ただし、複雑な状態遷移には使いづらい
        private async UniTaskVoid MonsterStateController()
        {
            while (true)
            {
                // ドローフェイズ
                // await DrawState();
                // メインフェイズ
                // await MainState();
                // バトルフェイズ
                // await BattleState();
                // エンドフェイズ
                // await EndState();
                
                await AttackState();
                var key = await MoveState();
                // スペースが押されたらbreak;
                if(key == " ") break;
            }
            
            Debug.Log("終了");
        }
        
        private async UniTask AttackState()
        {
            Debug.Log("スペースが押されたら攻撃");
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            monsters[4].Attack();
        }

        private async UniTask<string> MoveState()
        {
            Debug.Log("何かが押されたら移動");
            await UniTask.WaitUntil(() => Input.anyKeyDown);
            var key = Input.inputString;
            await monsters[4].transform.DOMoveX(1.0f, Duration).SetRelative().AsyncWaitForCompletion();
            await monsters[4].transform.DOMoveY(1.0f, Duration).SetRelative().AsyncWaitForCompletion();
            await monsters[4].transform.DOMove(originalPosition, Duration).SetRelative().AsyncWaitForCompletion();
            return key;
        }



        private readonly Vector3 originalPosition = new (-1.0f, -1.0f, 0.0f);
        private const float Duration = 1.0f;
    }
}
