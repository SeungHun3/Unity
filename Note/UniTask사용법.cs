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
	CancellationTokenSource disableCancellation = new CancellationTokenSource(); //��Ȱ��ȭ�� ���ó��
	CancellationTokenSource destroyCancellation = new CancellationTokenSource(); //������ ���ó��

	// Awake, Start�� ���� ��� �����Ǿ�����
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

	// Delay��� 
	async UniTask Delay()
	{
		await UniTask.Delay(1000); //1��
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

	// �񵿱� ������Ʈ
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


	// ���÷ε�
	public async UniTaskVoid LoadManyAsync()
	{
		//���� �Ϸ�Ǹ� �������� �Ѿ
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

		//���� �Ϸ�Ǹ� �������� �Ѿ
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
		//timeoutToken �����ֱ� 5�� -> ���� ������� �񵿱⿡���� ��ū�� ���� ������ ����.

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
