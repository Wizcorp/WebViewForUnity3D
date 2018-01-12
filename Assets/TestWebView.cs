using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestWebView : MonoBehaviour
{
	public Text Context;
	string subject = "WORD-O-MAZE";
	string body = "PLAY THIS AWESOME. GET IT ON THE PLAYSTORE";

#if UNITY_ANDROID
	public void CallShareApp()
	{
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("shareText", subject, body);
	}

	public void CallWebView()
	{
		Debug.LogWarning("CALLWEBVIEW");
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("OpenWebView");
	}

	void Start()
	{
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("Initialize", this.gameObject.name, "CallBack");
	}
#endif

#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _nativeLog();
	[DllImport("__Internal")]
	private static extern void _openURL(string url);
	[DllImport("__Internal")]
	private static extern void _setup(string gameObject, string methodName);

	public void CallWebView()
	{
		Debug.LogWarning("Call WKView");

		_openURL("https://www.google.com");
	}

	public void CallShareApp()
	{
	}

	public void CallBack(string message)
	{
		Context.text = message;
	}

	void Start()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			_setup(this.gameObject.name, "CallBack");

			_nativeLog();
		}
	}

#endif
}