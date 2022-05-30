using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TwitterShareButton : MonoBehaviour
{
     public string twitterNameParamter = "Este juego es lo mejor que me ha pasado en la vida.";
     private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
     public string LINK_GAME = "https://freesstylers.github.io/District-Dance-Battle/";

     public void shareOnTwitter()
     {
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + UnityWebRequest.EscapeURL(twitterNameParamter + "\n" + LINK_GAME));
     }
}
