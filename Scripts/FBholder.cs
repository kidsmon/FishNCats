using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class FBholder : MonoBehaviour
{
	public GameObject    UIFBIsLoggedIn;
	public GameObject    UIFBNotLoggedIn;
	public GameObject    UIFBAvatar;
	public GameObject    UIFBUserName;
	public GameObject    UICreateNickName;
	public Text          InputField;
	public Text          DebugText;
	private AccessToken  aToken;
	private GameObject   go;
	private ClientSocket cs;
	private bool         HaveNickname = false;

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
		go = new GameObject("ClientSocket");
		cs = go.AddComponent<ClientSocket>();
		cs.initClientSocket();
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

	// === public 함수(Inspector 노출) =========================

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

	public void SendNickName()
	{
		string nickname = InputField.text;
		
		LoginReqMessage Lrm = new LoginReqMessage();
		Lrm.Mtype = "JoinIDWrite";
		Lrm.UserID = nickname;
		aToken = AccessToken.CurrentAccessToken;
		Lrm.FacebookID = aToken.UserId;
		cs.sendData(Lrm);
		UICreateNickName.SetActive(false);
		DealWithFBMenus(true);
	}
  
	// === prviate 함수 =======================================
	private void LoginCallback(ILoginResult result)
	{
		if(FB.IsLoggedIn)
		{
			//Debug.Log("FB login worked!");
			RequestMessage reqMe = new RequestMessage();
			reqMe.Mtype = "LoginInfoRead";
			aToken = AccessToken.CurrentAccessToken;
			reqMe.FacebookID = aToken.UserId;
			//Debug.Log(reqMe.FacebookID);
			//Debug.Log(aToken.UserId);
			cs.sendData(reqMe);


			//Read후 HaveNickname 초기화후 true면 게임정보받아오기, false면 회원가입
			if (!HaveNickname)
			{//가입이 필요
				UICreateNickName.SetActive(true);
				UIFBNotLoggedIn.SetActive(false);
			}
			else
			{//이미 가입된상태 게임에 필요한 정보 불러오기
				DealWithFBMenus(true);
			}
		   
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

	private void DealWithFBMenus(bool isLoggedIn)
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

	private void DealWithProfilePicture(IGraphResult result)
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

	private void DealWithUserName(IGraphResult result)
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
