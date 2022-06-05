using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GetAllQuestions : MonoBehaviour
{

    public GameObject questionInfoContainer;
    public GameObject questionInfoTemplate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost/UnityGeoVise/listQuestions.php"));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onButtonClickedRefreshQuestions()
    {
        Destroy(questionInfoTemplate);

        StartCoroutine(GetRequest("http://localhost/UnityGeoVise/listQuestions.php"));
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
                    string[] questions = rawresponse.Split('*');

                    


                   /* for (int i = 0; i < questions.Length; i++)
                    {

                        if (questions[i] != "")
                        {
                            //// Debug.Log("Current data: " + users[i]);
                            string[] questioninfo = questions[i].Split(',');


                            if (questioninfo[2] == "1")
                            {
                                questioninfo[2] = "Araç Plakaları";

                            }
                            if (questioninfo[2] == "2")
                            {
                                questioninfo[2] = "Yemek";

                            }
                            if (questioninfo[2] == "3")
                            {
                                questioninfo[2] = "Tarihi Eser";

                            }
                            if (questioninfo[1] == "1")
                            {
                                questioninfo[1] = "Kolay";

                            }
                            if (questioninfo[1] == "2")
                            {
                                questioninfo[1] = "Orta";

                            }
                            if (questioninfo[1] == "3")
                            {
                                questioninfo[1] = "Zor";

                            }
                            // Debug.Log("Title: " + questioninfo[0] + "Difficulty: " + questioninfo[1] + "Category :" + questioninfo[2] + "City :" + questioninfo[3]);

                            GameObject gobj2 = (GameObject)Instantiate(questionInfoTemplate);
                            gobj2.transform.SetParent(questionInfoContainer.transform);

                            gobj2.GetComponent<QuestionInfo>().QuestionTitle.text = questioninfo[0];
                            gobj2.GetComponent<QuestionInfo>().QuestionDifficulty.text = questioninfo[1];
                            gobj2.GetComponent<QuestionInfo>().QuestionCategory.text = questioninfo[2];
                            gobj2.GetComponent<QuestionInfo>().QuestionCity.text = questioninfo[3];

                        }*/




                        for (int i = 0; i < questions.Length; i++)
                    {

                        if (questions[i] != "")
                        {
                            //// Debug.Log("Current data: " + users[i]);
                            string[] questioninfo = questions[i].Split(',');
                            

                            if(questioninfo[2]=="1")
                            {
                                questioninfo[2] = "Araç Plakaları";

                            }
                            if (questioninfo[2] == "2")
                            {
                                questioninfo[2] = "Yemek";

                            }
                            if (questioninfo[2] == "3")
                            {
                                questioninfo[2] = "Tarihi Eser";

                            }
                            if (questioninfo[1] == "1")
                            {
                                questioninfo[1] = "Kolay";

                            }
                            if (questioninfo[1] == "2")
                            {
                                questioninfo[1] = "Orta";

                            }
                            if (questioninfo[1] == "3")
                            {
                                questioninfo[1] = "Zor";

                            }
                           // Debug.Log("Title: " + questioninfo[0] + "Difficulty: " + questioninfo[1] + "Category :" + questioninfo[2] + "City :" + questioninfo[3]);

                            GameObject gobj2 = (GameObject)Instantiate(questionInfoTemplate);
                            gobj2.transform.SetParent(questionInfoContainer.transform);

                            gobj2.GetComponent<QuestionInfo>().QuestionTitle.text = questioninfo[0];
                            gobj2.GetComponent<QuestionInfo>().QuestionDifficulty.text = questioninfo[1];
                            gobj2.GetComponent<QuestionInfo>().QuestionCategory.text = questioninfo[2];
                            gobj2.GetComponent<QuestionInfo>().QuestionCity.text = questioninfo[3];

                        }

                    }



                    break;
            }
        }
    }
}
