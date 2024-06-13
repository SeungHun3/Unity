using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class SUniTask : SingleDestroy<SUniTask>
{

	CancellationTokenSource _cancellationTokenSource = new();
	CancellationTokenSource disableCancellation = new CancellationTokenSource(); //비활성화시 취소처리
	CancellationTokenSource destroyCancellation = new CancellationTokenSource(); //삭제시 취소처리

	// Awake, Start와 같은 기능 구현되어있음
	new async UniTaskVoid Awake()
	{

		Debug.Log(await AsyncString("Awake"));
	}


	async UniTaskVoid Start()
	{
		Debug.Log(await AsyncString("Start"));
	}

	private void OnEnable()
	{
		if (disableCancellation != null)
		{
			disableCancellation.Dispose();
		}
		disableCancellation = new CancellationTokenSource();
	}

	private void OnDisable()
	{
		disableCancellation.Cancel();
	}

	protected void OnDestroy()
	{
		destroyCancellation.Cancel();
		destroyCancellation.Dispose();
	}

	// Delay방법 
	async UniTask Delay()
	{
		await UniTask.Delay(1000); //1초
		await UniTask.Delay(TimeSpan.FromSeconds(1));
		await UniTask.NextFrame();
	}


	void CancelCallBack()
	{
		Debug.Log("CallBack");
	}
	void CancelCallBack1()
	{
		Debug.Log("CallBack1");
	}

	// 비동기 업데이트
	public async UniTaskVoid AsyncUpdateTask()
	{
		CancellationToken token = new();
		_cancellationTokenSource = new CancellationTokenSource();
		token = _cancellationTokenSource.Token;

		token.Register(CancelCallBack);
		token.Register(CancelCallBack1);

		_cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);

		await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(token))
		{
			Debug.Log("Update() " + Time.frameCount);
		}
	}

	public void CancelCurrentTask()
	{
		if (_cancellationTokenSource != null)
		{
			_cancellationTokenSource.Cancel();
		}
	}


	// 동시로드
	public async UniTaskVoid LoadManyAsync()
	{
		//전부 완료되면 다음으로 넘어감
		var (a, b, c) = await UniTask.WhenAll(
			LoadAsSprite("foo"),
			LoadAsSprite("bar"),
			LoadAsSprite("baz"));
	}

	async UniTask<Sprite> LoadAsSprite(string path)
	{
		var resource = await Resources.LoadAsync<Sprite>(path);
		return (resource as Sprite);
	}



	public async UniTaskVoid TestManyAsync()
	{
		CancellationToken token = new();
		_cancellationTokenSource = new CancellationTokenSource();
		token = _cancellationTokenSource.Token;

		token.Register(CancelCallBack);
		token.Register(CancelCallBack1);
		_cancellationTokenSource.RegisterRaiseCancelOnDestroy(this);

		//전부 완료되면 다음으로 넘어감
		var (a, b) = await UniTask.WhenAll(
			AsyncString("a"),
			AsyncString("b")).AttachExternalCancellation(token);

		Debug.Log(a +" , " + b);


	}

	public async UniTask<string> AsyncString(string str)
	{


		Debug.Log(str);
		await UniTask.Delay(1000);
		string log = str + ": End";
		return log;
	}

	public IProgress<float> MyProgress;
	float progressValue = 0f;

	public void CreateProgress()
	{
		MyProgress = Progress.Create<float>(x => Debug.Log(x));
	}


	public async UniTask ChangeProgress()
	{
		//progressValue += 1.0f;
		//MyProgress.Report(progressValue);


		var timeoutToken = new CancellationTokenSource();
		timeoutToken.CancelAfterSlim(TimeSpan.FromSeconds(5));
		//timeoutToken 생명주기 5초 -> 이후 사라지며 비동기에서는 토큰이 없어 에러가 난다.

		try
		{
			var request = await UnityWebRequest.Get("http://google.co.jp")
			.SendWebRequest()
			.ToUniTask(progress: MyProgress).AttachExternalCancellation(timeoutToken.Token);
		}
		catch (OperationCanceledException ex)
		{
			if (timeoutToken.IsCancellationRequested)
			{
				UnityEngine.Debug.Log("Timeout.");
			}
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}

	}
}
