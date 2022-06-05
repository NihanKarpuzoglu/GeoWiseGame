using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class GetAllUsers : MonoBehaviour
{


    public GameObject userInfoContainer;
    public GameObject userInfoTemplate;
    // Start is called before the first frame update
    void Start()
    { 
        
        
        StartCoroutine(GetRequest("http://localhost/UnityGeoVise/listUsers.php"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetRequest(string uri)
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
                    string[] users = rawresponse.Split('*');
                    for(int i=0;i<users.Length;i++)
                    {

                        if (users[i] != "") {
                            // Debug.Log("Current data: " + users[i]);
                            string[] userinfo = users[i].Split(',');
                            //Debug.Log("Email: " + userinfo[0] + "Username: " + userinfo[1]);


                            GameObject gobj = (GameObject)Instantiate(userInfoTemplate);
                            gobj.transform.SetParent(userInfoContainer.transform);
                            gobj.GetComponent<UserInfo>().userEmail.text = userinfo[0];
                            gobj.GetComponent<UserInfo>().userName.text = userinfo[1];

                        }

                    }



                    break;
            }
        }
    }
}
