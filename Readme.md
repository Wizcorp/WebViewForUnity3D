**Webview for Android and iOS** <br/>
**Written by *Jae* 2018/01/11**

# Preface

This will explain how to create webview for Android and iOS

## How it looks like
### Android
![Image](./MarkdownImages/AExample01.png)
![Image](./MarkdownImages/AExample02.png)

### iOS

# How to use

1. Build project for iOS or Android
2. Launch app and test

## Entry point

1. Assets/TestWebView.cs
2. Assets/Plugins/Android or Assets/Plugins/iOS

# How to make a build for iOS

1. Build iOS with Unity3D

![Image](./MarkdownImages/Image01.png)

2. When build process have been done then open xcodeproject which you just built

3. Go to General tab and change Signing category
- If you don't know about the password and how to do, please ask  check you email. As a Wizcorp memeber, you've already been received ID & password for it.
- You need a Apple develop ID & password

![Image](./MarkdownImages/Image02.png)

4. Add Webkit.framework in the general tab

![Image](./MarkdownImages/Image03.png)

5. Build Project & Test with a iOS device
- Press play button on the top menu bar

# How to make a build for Android

To make it run in Unity3D with Java code needs compiled .jar or .arr file.

1. Get Android webview plugin from gitlab (https://gitlab.wizcorp.net/kjmin/webviewAndroidPlugin)
2. Build android project with android studio
- By cliking hammer button on the top menu bar (WebView:app [exportjar])
![Image](./MarkdownImages/Image04.png)

3. Go to project folder > builds > intermediates > bundles > debug
![Image](./MarkdownImages/Image05.png)

4. Find a classes.jar file and place the file into Unity3D > Plugins > Android
![Image](./MarkdownImages/Image06.png)

5. Build apk file with Unity3D
![Image](./MarkdownImages/Image07.png)

6. Test (Use AVD or real android device)