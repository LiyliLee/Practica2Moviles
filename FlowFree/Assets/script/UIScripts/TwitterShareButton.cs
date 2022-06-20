using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TwitterShareButton : MonoBehaviour
{
     private string twitterNameParamter = "Este juego es lo mejor que me ha pasado en la vida. Ya me he pasado el nivel " + 
        GameManager.GetInstance().GetLevelToPlay() + " del paquete " + GameManager.GetInstance().GetPackName();
     private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
     private string LINK_GAME = "https://www.ucm.es/";

     public void shareOnTwitter()
     {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + UnityWebRequest.EscapeURL(twitterNameParamter + "\n" + LINK_GAME));
     }
}
