using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class FBholder : MonoBehaviour
{
	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBNotLoggedIn;
	public GameObject UIFBAvatar;
	public GameObject UIFBUserName;
	public Text DebugText;
	private AccessToken aToken;
	private GameObject go;
	private ClientSocket cs;

	private Dictionary<string, string> profile = null;

	void Awake ()
	{
		cs.initClientSocket();
		if (!FB.IsInitialized)
		{
			FB.Init(SetInit, OnHideUnity);
		}
		else
		{
			FB.ActivateApp();
		}

	}

	void Start()
	{   
		////////
		go = new GameObject("ClientSocket");
		cs = go.AddComponent<ClientSocket>();
		//ClientSocket을 GameObject에 포함
	}

	private void SetInit()
	{
		//Debug.Log("FB Init done.");
		
		if(FB.IsLoggedIn)
		{
			DealWithFBMenus(true);
			FB.ActivateApp();
			Debug.Log("FB Init done.");
		}
		else
		{
			DealWithFBMenus(false);
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		if(!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void FBlogin()
	{
		List<string> perms = new List<string>() { "public_profile", "email", "user_friends"};
		FB.LogInWithReadPermissions(perms, LoginCallback);

	}

	public void FBlogout()
	{
		FB.LogOut();
		DealWithFBMenus(false);
	}

	private void LoginCallback(ILoginResult result)
	{
		if(FB.IsLoggedIn)
		{
			//Debug.Log("FB login worked!");
			DealWithFBMenus(true);
		   
			//////////////
			aToken = AccessToken.CurrentAccessToken;
			cs.sendtest(aToken.UserId);
			//로그인 후 아이디 서버로 전송하는 부분

			foreach (string perm in aToken.Permissions)
			{
				Debug.Log(perm);
			}
		}
		else
		{
			Debug.Log("FB Login fail");
			DealWithFBMenus(false);
		}
	}

	void DealWithFBMenus(bool isLoggedIn)
	{
		if(isLoggedIn)
		{
			UIFBIsLoggedIn.SetActive(true);
			UIFBNotLoggedIn.SetActive(false);

			// get profile picture code
			FB.API(Util.GetPictureURL("me", 128, 128), HttpMethod.GET, DealWithProfilePicture);
			// get username code
			FB.API("/me?fields=id,first_name", HttpMethod.GET, DealWithUserName);

		}
		else
		{
			UIFBIsLoggedIn.SetActive(false);
			UIFBNotLoggedIn.SetActive(true);
		}
	}

	void DealWithProfilePicture(IGraphResult result)
	{
		if(result.Error != null)
		{
			Debug.Log("problem with getting profile picture");

			FB.API(Util.GetPictureURL("me", 128, 128), HttpMethod.GET, DealWithProfilePicture);
			return;
		}

		Image UserAvatar = UIFBAvatar.GetComponent<Image>();
		UserAvatar.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
	}

	void DealWithUserName(IGraphResult result)
	{
		if (result.Error != null)
		{
			Debug.Log("problem with getting profile picture");

			FB.API("/me?fields=id, first_name", HttpMethod.GET, DealWithUserName);
			return;
		}

		profile = Util.DeserializeJSONProfile(result.RawResult);

		Text UserMsg = UIFBUserName.GetComponent<Text>();
		UserMsg.text = "Hello, " + profile["first_name"];
		

	}

	public void PrintDebug()
	{
		DebugText.text = "";
		AccessToken aToken = AccessToken.CurrentAccessToken;
		DebugText.text = aToken.UserId;
	}
}
