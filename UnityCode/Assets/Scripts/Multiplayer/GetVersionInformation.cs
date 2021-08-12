using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Net;
using System;

public class GetVersionInformation : MonoBehaviour
{
    public bool NewVersionAvaliable = false;
    public string Version = "";
    public string CurrVer = "";
    public UnityEngine.UI.Text VersionDeeta;
    public GameObject VersionDownloadUI;
    public GameObject NoConnectionUI;






    IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("https://www.google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //var GetVersion = Process.Start(".\\MonoBleedingEdge\\Python\\StartGetVersion.bat");
        //GetVersion.Start();
        //CurrVer = File.ReadAllText(".\\MonoBleedingEdge\\Python\\currver.txt");
        //GetVersion.Kill();
        //byte[] bytes;
        


        StartCoroutine(checkInternetConnection((isConnected) => {
            // handle connection status here

            if (isConnected)
            {
                NoConnectionUI.SetActive(false);
                string url = "https://raw.githubusercontent.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/main/VersionControl/CurrentVersion";


                print("Downloading Version Data...");
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(url, "CurrVer.txt");
                    }
                    CurrVer = File.ReadAllText("CurrVer.txt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Download Failed\n{0}",
                    ex.Message.ToString());
                }
                VersionDeeta.text = "Version: " + Version;
                if (Version + "\n" != CurrVer)
                {
                    print("New Version Avaliable!");
                    NewVersionAvaliable = true;
                }
                if (NewVersionAvaliable)
                {
                    VersionDownloadUI.SetActive(true);
                }
                else
                {
                    VersionDownloadUI.SetActive(false);
                }
            }
            else
            {
                NoConnectionUI.SetActive(true);
            }
            

        }));



    }



    public void CloseNoConnectionBox()
    {
        NoConnectionUI.SetActive(false);
    }

    public void DownloadNewVersion()
    {
        
    }

    public void DontDownload()
    {
        VersionDownloadUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //VersionDeeta.text = ("Version: " + CurrVer);
    }
}
