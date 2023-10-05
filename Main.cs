using Godot;
using System;

// note
// The class from which the game begins.
// It derives from the Node class because it merely needs _Ready() to be called.
// ノート
//ゲームの開始地点となるクラス。
// 単に_Ready()が呼び出される事だけを必要としているため、Nodeクラスから派生します。
// 
public partial class Main : Node
{
	// note
	// With [Export] annotation is similar to Unity-Prefab.
	// It can be set up from the inspector and
	// no need to write resource paths in the source code.
	// I like this style.
	// There are other ways to instantiate a Godot-Scene.
	// ノート
	// [Export]アノテーションはUnityのプレハブと同じように使えます。
	// インスペクターから設定可能であり、コード中にリソースパスを記述する必要がありません。
	// 私はこのスタイルが好きです。
	// 他にもGodotのシーンをインスタンス化する方法はあります。
	//
	[Export] private PackedScene InputSystemPrefab { get; set; }
	[Export] private PackedScene TitlePrefab { get; set; }
	[Export] private PackedScene OperationPrefab { get; set; }
	[Export] private PackedScene InGamePrefab { get; set; }
	[Export] private PackedScene CreditPrefab { get; set; }
	[Export] private PackedScene GameOverPrefab { get; set; }

	// note
	// Class member variable that holds the instantiated Godot-Scene.
	// ノート
	// インスタンス化したGodotシーンを保持するクラスメンバ変数
	//
	private InputSystem _inputSystem;
	private Title _title;
	private Operation _operation;
	private InGame _inGame;
	private Credit _credit;
	private GameOver _gameOver;
	
	
	// note
	// Singleton to hold Score, remaining ship, etc.
	// Inst() to avoid misunderstanding with PackedScene.Instantiate().
	// ノート
	// Scoreや残機等を保持するシングルトン
	// PackedScene.Instantiate()との誤解を避けるためInst()にしています。
	//
	private GameProperties _GP = GameProperties.Inst();

	// note
	// Godot _Ready() is the equivalent of Unity Start().
	// Similarly, Godot _Process() corresponds to Unity Update().
	// The main class does no polling in _Process() after displaying the title.
	// It switches scenes upon notification by the observer pattern.
	// ノート
	//　Godotの_Ready()はUnityのStart()に相当します。
	// 同様にGodotの_Process()はUnityのUpdate()に相当します。
	// メインクラスはタイトルを表示した後、_Process()でポーリングを行いません。
	// オブザーバーパターンによる通知を受けてシーンの切り替えを行います。
	//
	public override void _Ready()
	{
		_inputSystem = (InputSystem)InputSystemPrefab.Instantiate();
		AddChild(_inputSystem);
		sceneChange(GameScene.Main, GameScene.Title);
	}

	// note
	// Methods to be called upon notification.
	// The observer pattern in this section is implemented in NotifyableCanvaslayer.cs.
	// ノート
	// 通知を受けて呼び出されるメソッド。
	// この箇所のオブザーバーパターンはNotifyableCanvaslayer.csに実装があります。
	private void sceneChange(GameScene currnt, GameScene next)
	{
		if(GameScene.Title == currnt){
			_title.RemoveObserver(sceneChange);
			RemoveChild(_title);
		}
		if(GameScene.Title == next){
			_title = (Title)TitlePrefab.Instantiate();
			_title.InputSystem = _inputSystem;
			_title.AddObserver(sceneChange);
			AddChild(_title);
		}

		if(GameScene.Operation == currnt){
			_operation.RemoveObserver(sceneChange);
			RemoveChild(_operation);
			_GP.Setup();
		}
		if(GameScene.Operation == next){
			_operation = (Operation)OperationPrefab.Instantiate();
			_operation.InputSystem = _inputSystem;
			_operation.AddObserver(sceneChange);
			AddChild(_operation);
		}

		if(GameScene.Credit == currnt){
			_credit.RemoveObserver(sceneChange);
			RemoveChild(_credit);
		}
		if(GameScene.Credit == next){
			_credit = (Credit)CreditPrefab.Instantiate();
			_credit.InputSystem = _inputSystem;
			_credit.AddObserver(sceneChange);
			AddChild(_credit);
		}

		if(GameScene.GameOver == currnt){
			_gameOver.RemoveObserver(sceneChange);
			RemoveChild(_gameOver);
		}
		if(GameScene.GameOver == next){
			_gameOver = (GameOver)GameOverPrefab.Instantiate();
			_gameOver.InputSystem = _inputSystem;
			_gameOver.AddObserver(sceneChange);
			AddChild(_gameOver);
		}

		// note
		// Unlike other scenes, the Ingame scene releases itself from the queue 
		// after the stage is completed and continues to generate another Ingame scene.
		// If a primary thread performs this operation continuously, 
		// it will destroy its own stack.
		// Once the method call is terminated by a timer, 
		// control is returned to the game engine, 
		// and the actual release generation process is performed at a different time.
		// ノート
		// Ingameシーンは他のシーンとは異なりステージ完了後に自分自身をキューから解放し、
		// 続けて再度Ingameシーンを生成します。
		// プライマリースレッドが連続してこの操作を行うと自分自身のスタックを破壊する事に繋がります。
		// 一旦タイマーでメソッドコールを抜けてゲームエンジンに制御を戻し、
		// 実際の開放生成処理は別のタイミングで実行します。
		//
		if(GameScene.InGame == currnt)
			GetNode<Timer>("InGameFreeTimer").Start();
		if(GameScene.InGame == next)
			GetNode<Timer>("InGameStartTimer").Start();
	}


	private void OnFreeInGame()
	{
		_inGame.RemoveObserver(sceneChange);
		RemoveChild(_inGame);
		_inGame = null;
	}


	private void OnGenerateInGame()
	{
		_inGame = (InGame)InGamePrefab.Instantiate();
		_inGame.InputSystem = _inputSystem;
		_inGame.AddObserver(sceneChange);
		AddChild(_inGame);
	}

}





