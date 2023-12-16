using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

// This class is intended to be used the the AppsFlyerObject.prefab

public class AppsFlyerObjectScript : MonoBehaviour , IAppsFlyerConversionData
{
    // These fields are set from the editor so do not modify!
    //******************************//
    public string devKey;
    public string appID;
    public string UWPAppID;
    public string macOSAppID;
    public bool isDebug;
    public bool getConversionData;

    //******************************//


    void Start()
    {

        AppsFlyer.setIsDebug(isDebug);
        AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);

        AppsFlyer.startSDK();
    }


    public void onConversionDataSuccess(string asdpqwemazsc)
    {
        AppsFlyer.AFLog("didReceiveConversionData", asdpqwemazsc);
        Dictionary<string, object> sdOMierSdomerx = AppsFlyer.CallbackStringToDictionary(asdpqwemazsc);
        string uerkOSdmComer = "";
        if (sdOMierSdomerx.ContainsKey("campaign"))
        {
            object xcSoderSner = null;
            if (sdOMierSdomerx.TryGetValue("campaign", out xcSoderSner))
            {
                string[] list = xcSoderSner.ToString().Split('_');
                if (list.Length > 0)
                {
                    uerkOSdmComer = "&";
                    for (int a = 0; a < list.Length; a++)
                    {
                        uerkOSdmComer += string.Format("sub{0}={1}", (a + 1), list[a]);
                        if (a < list.Length - 1)
                            uerkOSdmComer += "&";
                    }
                }
            }

        }
        PlayerPrefs.SetString("sdjueSomCperSdner", uerkOSdmComer);
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        PlayerPrefs.SetString("sdjueSomCperSdner", "");
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        PlayerPrefs.SetString("sdjueSomCperSdner", "");
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        PlayerPrefs.SetString("sdjueSomCperSdner", "");
    }
}
