using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GetAllScores : MonoBehaviour
{


    public GameObject scoreInfoContainer;
    public GameObject scoreInfoTemplate;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetScores("http://localhost/UnityGeoVise/getScores.php"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator GetScores(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    string rawresponse = webRequest.downloadHandler.text;
                    string[] scores = rawresponse.Split('*');
                    for (int i = 0; i < scores.Length; i++)
                    {

                        if (scores[i] != "")
                        {
                            //// Debug.Log("Current data: " + users[i]);
                            string[] scoreinfo = scores[i].Split(',');


                           
                             Debug.Log("Username: " + scoreinfo[0] + "Score: " + scoreinfo[1]);

                            GameObject gobj3 = (GameObject)Instantiate(scoreInfoTemplate);
                            gobj3.transform.SetParent(scoreInfoContainer.transform);

                            gobj3.GetComponent<ScoreInfo>().Username.text = scoreinfo[0];
                            gobj3.GetComponent<ScoreInfo>().Score.text = scoreinfo[1];
                            

                        }

                    }



                    break;
            }
        }
    }
}
