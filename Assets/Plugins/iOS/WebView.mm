#import <UIKit/UIKit.h>
#import <WebKit/WebKit.h>

extern "C" UIViewController *UnityGetGLViewController();
extern void UnitySendMessage(const char *, const char *, const char *);

@interface NativeWebView : UINavigationController
{   
	// To use WKWebView you need to add WkWebView.framekwork via Xcode
	WKWebView *webView;
	NSString *gameObject;
	NSString *method;
}

-(void) setup:(const char*)gameObjectName method:(const char*)methodName;
-(void) openURL: (const char*) urlString;
-(void) dismissModal;

@end

@implementation NativeWebView

-(id)init
{
    webView = [[WKWebView alloc] init];

    UIViewController *webViewController = [[UIViewController alloc] init];
    webViewController.view = webView;

    self = [super initWithRootViewController: webViewController];
    self.navigationBar.barStyle = UIBarStyleDefault;
    
    UIBarButtonItem *cancelButton = [[UIBarButtonItem alloc] initWithTitle:@"Back" style: UIBarButtonItemStylePlain target:self action: @selector(dismissModal)];
    [self.navigationBar.topItem setLeftBarButtonItem: cancelButton];
    
    return self;
}

-(void) setupCallBack:(const char*)gameObjectName method:(const char*)methodName
{
	NSLog(@"Setting up callback");
    NSLog(@"data = %@", [NSString stringWithUTF8String:gameObjectName]);
    NSLog(@"data = %@",  [NSString stringWithUTF8String:methodName]);

    gameObject = [NSString stringWithUTF8String:gameObjectName];
	method = [NSString stringWithUTF8String:methodName];
}

-(void) openURL:(const char*)urlString
{
    NSURL *url = [[NSURL alloc] initWithString: [NSString stringWithUTF8String: urlString]];
    NSURLRequest *request = [[NSURLRequest alloc] initWithURL:url];
    [webView loadRequest:request];

    [UnityGetGLViewController() presentModalViewController: self animated: YES];
}

-(void) dismissModal
{
	NSLog(@"Closing modal");
    NSURL *url = [[NSURL alloc] initWithString: [NSString stringWithUTF8String: "about:blank"]];
    NSURLRequest *request = [[NSURLRequest alloc] initWithURL:url];
    [webView loadRequest:request];

	UnitySendMessage([gameObject UTF8String], [method UTF8String], "Calling back from iOS device");
 	[UnityGetGLViewController() dismissModalViewControllerAnimated: YES];
}

@end

static NativeWebView *nativeWebViewPlugin = nil;

extern "C"
{
	void _nativeLog() {
      NSLog(@"Log called from Unity");
	}

	void _setupCallBack(const char* gameObjectName, const char* methodName) {
        if (nativeWebViewPlugin == nil)
            nativeWebViewPlugin = [[NativeWebView alloc] init];

		[nativeWebViewPlugin setupCallBack:gameObjectName method:methodName];
	}

    void _openURL(const char* url) {
        [nativeWebViewPlugin openURL: url];
    }
}
