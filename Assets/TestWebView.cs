using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestWebView : MonoBehaviour
{
	public string URL = "https://wizcorp.jp";
	public Text Context;

	#region shared
	public void CallBack(string message)
	{
		Context.text = message;
	}
	#endregion

#if UNITY_ANDROID
	public void CallShareApp()
	{
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("ShareText", "Subject", "Body");
	}

	public void CallWebView()
	{
		Debug.Log("Call WebView");
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("OpenWebView", URL);
	}

	void Start()
	{
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("SetupCallBack", this.gameObject.name, "CallBack", "Calling back from Android");
	}
#endif

#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _nativeLog();
	[DllImport("__Internal")]
	private static extern void _openURL(string url);
	[DllImport("__Internal")]
	private static extern void _setupCallBack(string gameObject, string methodName);

	public void CallWebView()
	{
		Debug.Log("Call WebView");

		_openURL(URL);
	}

	public void CallShareApp()
	{
	}

	void Start()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_setupCallBack(this.gameObject.name, "CallBack");

			_nativeLog();
		}
	}

#endif
}