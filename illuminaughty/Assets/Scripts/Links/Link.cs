
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour
{

	public void OpenTwitter()
	{
#if !UNITY_EDITOR
		openWindow("https://twitter.com/punkfruitgame");
#endif
	}

	public void OpenYoutube()
	{
#if !UNITY_EDITOR
		openWindow("https://www.youtube.com/user/cybergrapeable");
#endif
	}

	public void OpenNewgrounds()
	{
#if !UNITY_EDITOR
		openWindow("https://punkfruit.newgrounds.com/");
#endif
	}

	public void OpenItch()
	{
#if !UNITY_EDITOR
		openWindow("https://punkfruit.itch.io/");
#endif
	}

	public void OpenGamejolt()
	{
#if !UNITY_EDITOR
		openWindow("https://gamejolt.com/@punkfruit");
#endif
	}

	public void OpenPatreon()
	{
#if !UNITY_EDITOR
		openWindow("https://www.patreon.com/punkfruit");
#endif
	}

	public void OpenDiscord()
	{
#if !UNITY_EDITOR
		openWindow("https://discord.gg/rShAPPT");
#endif
	}

	public void OpenIllumiClassic()
	{
#if !UNITY_EDITOR
		openWindow("https://www.newgrounds.com/portal/view/675006");
#endif
	}

	public void OpenIllumi2()
	{
#if !UNITY_EDITOR
		openWindow("https://punkfruit.itch.io/illuminaughty2");
#endif
	}

	public void OpenPriceFreedom()
	{
#if !UNITY_EDITOR
		openWindow("https://priceforfreedom.net/");
#endif
	}

	public void OpenAcac()
	{
#if !UNITY_EDITOR
		openWindow("https://acacgames.newgrounds.com/");
#endif
	}

	public void OpenTera()
	{
#if !UNITY_EDITOR
		openWindow("http://teraurge.blogspot.com/");
#endif
	}

	public void OpenRedders()
	{
#if !UNITY_EDITOR
		openWindow("https://twitter.com/2dReddersART");
#endif
	}

	public void OpenTradeWind()
	{
#if !UNITY_EDITOR
		openWindow("https://twitter.com/RenTradewind");
#endif
	}

	public void OpenEthrk()
	{
#if !UNITY_EDITOR
		openWindow("https://twitter.com/EthrkArt");
#endif
	}

	public void OpenQuan()
	{
#if !UNITY_EDITOR
		openWindow("https://twitter.com/Quanlain");
#endif
	}

	public void OpenFreya()
	{
#if !UNITY_EDITOR
		openWindow("https://thildilig.bandcamp.com/releases");
#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

	

}