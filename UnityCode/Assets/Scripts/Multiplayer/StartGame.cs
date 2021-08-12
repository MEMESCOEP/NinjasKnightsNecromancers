using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public GameObject MainCam;
    public GameObject AudioManager;
    public AudioSource AudManSource;
    public UnityEngine.UI.Text WaitingText;
    public UnityEngine.UI.Text TimerText;
    public bool Started = false;
    public bool StartedWaitMusic = false;

    public List<Transform> SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Wait()
    {
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(1);
        Debug.Log("After Waiting 2 Seconds");
    }
    IEnumerator StartWait()
    {
        GameObject.Find("NetworkManager").GetComponent<Mirror.NetworkManagerHUD>().offsetX = 0;
        int TT = 5;
        TimerText.text = "5";
        //yield return new WaitForSeconds(1);
        WaitingText.enabled = false;
        Started = true;
        AudManSource.volume = 1;
        AudManSource.clip = AudioManager.GetComponent<AudioManager>().AudioFiles[6];
        //Debug.Log("Before Waiting 2 seconds");
        int i = 0;
        AudManSource.Play();

        while (i < 6)
        {
            TT -= 1;
            yield return new WaitForSeconds(1);
            TimerText.text = TT.ToString();
            if (!AudManSource.isPlaying)
            {
                AudManSource.Play();
            }
            //AudManSource.Play();
            
            i++;

        }

        TimerText.text = "GO!";

        MainCam.GetComponent<CameraController>().DisablePlayerMovement = false;

        AudManSource.Stop();
        AudManSource.clip = AudioManager.GetComponent<AudioManager>().AudioFiles[4];
        AudManSource.Play();
        yield return new WaitForSeconds(3);

        //AudManSource.Stop();
        AudManSource.clip = AudioManager.GetComponent<AudioManager>().AudioFiles[5];
        AudManSource.Play();
        //int io = 0;
        AudManSource.volume = 0;
        StartCoroutine(StartFade(AudManSource, 0.75f, 1));
        AudManSource.loop = true;
        TimerText.enabled = false;
        
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    // Update is called once per frame
    void Update()
    {

        //print(MainCam.GetComponent<CameraController>().Players.Length);
        if (MainCam.GetComponent<CameraController>().Players.Length == 2 && !Started)
        {
            try
            {
                MainCam.GetComponent<CameraController>().DisablePlayerMovement = true;
                MainCam.GetComponent<CameraController>().Players[0].transform.position = SpawnPoints[0].position;
                MainCam.GetComponent<CameraController>().Players[1].transform.position = SpawnPoints[1].position;
                
            }
            catch
            {
                Debug.LogError("ERROR!");
            }
            Started = true;
            AudManSource.loop = false;
            AudManSource.Stop();
            StartCoroutine(StartWait());

        }
        else if(MainCam.GetComponent<CameraController>().Players.Length == 1 && !StartedWaitMusic)
        {
            GameObject.Find("NetworkManager").GetComponent<Mirror.NetworkManagerHUD>().offsetX = 0;
            StartedWaitMusic = true;
            AudManSource.volume = 0.17f;
            AudManSource.clip = AudioManager.GetComponent<AudioManager>().AudioFiles[7];
            AudManSource.loop = true;
            AudManSource.Play();
        }
    }
}
