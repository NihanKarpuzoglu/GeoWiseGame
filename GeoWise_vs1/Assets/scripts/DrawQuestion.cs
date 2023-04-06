
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrawQuestion : MonoBehaviour
{
    public static string Difficulty;
    public static string Category;
    public string[] QuestionAnswerArray;
    public string[] QuestionSplit;
    public int QuestionIndex;
    public int ArrayLength;
    public string Question;
    public string AnswerId;
    public Text QuestionText;
    public Text isCorrect;
    public Text ScoreText;
    public string Before;
    public int Score;
    public static int userID;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //Difficulty = "1";
        //Category = "1";
        StartCoroutine(DrawQuestions());
        //PutQuestion();
    }
    public void GetButtonName(Button ButtonName)
    {
        //Debug.Log(ButtonName.ToString());
        Debug.Log("GetButtonName");
        string button_name = ButtonName.ToString();
        button_name = button_name.Trim();
        string[] button_name_arr = button_name.Split(' ');
        Before = AnswerId;
        Debug.Log("Before:" + Before);
        StartCoroutine(DrawAnswer(button_name_arr[0]));


    }
    public void PutQuestion()
    {
        Debug.Log("PutQuestion");
        QuestionSplit = QuestionAnswerArray[QuestionIndex].Split(',');
        //Debug.Log(QuestionSplit[QuestionIndex]);
        Question = QuestionSplit[0];
        AnswerId = QuestionSplit[1];
        QuestionText.text = Question;
        /*QuestionIndex++;
        if (QuestionIndex >= ArrayLength)
        {
            StartCoroutine(_GamerUpdateScore(userID, Score));
            //DashBoarda dön
        }*/
        
    }

    IEnumerator _GamerUpdateScore(int userId, int score)
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm updatePassForm = new WWWForm(); // WWW form oluşturuyorum

        updatePassForm.AddField("id", userId); // Mail verisini forma ekliyorum
        updatePassForm.AddField("score", score); // Şifre verisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityGeoVise/userUpdateScore.php", updatePassForm);
        yield return www.SendWebRequest();
        //WWW veriGonder = new WWW(hesapOlusturURL, hesapOlusturmaForm); // WWW ile veri gönderilmesi için siteye bağlanıyorum
        //yield return veriGonder; // Veriyi gönderiyorum

        string retrievingMessage = www.downloadHandler.text;

        if (retrievingMessage == "-1")//
        { // Siteden 1 yanıtı geldiyse
            /*Debug.LogWarning("Hata!!");
            string notification = "Hata!!";
            _notificationHelper.showNotification(notification, 3);*/
        }
        else
        {
            NewPage(4);
        }
    }
    public void NewPage(int sceneID)//Login Scene için scene id:0 - Admin scene icin sceneId:1
    {
        if(sceneID == 4)
        {
            PlayerPrefs.SetInt("userLog", 1);
            SceneManager.LoadScene("DashboardScene"); // Sahneyi yeniden yüklüyorum
        }
    }


    
    public IEnumerator DrawAnswer(string user_answer)
    {
        Debug.Log("DrawAnswer");
        WWWForm form = new WWWForm();
        form.AddField("user_answer", user_answer);
        Debug.Log("\'"+user_answer+"\'");
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityGeoVise/draw_answer.php", form)) //posting the AddField parameters to the php file
        {
            yield return www.SendWebRequest();  //Send the request and wait until the result is yielded

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text); //print the answer
                string answer_id = www.downloadHandler.text;
                Debug.Log("'"+answer_id+"'");
                answer_id = answer_id.Trim();
                //Debug.Log(jsonArray + "isOkey1");
                //yield return new WaitUntil(() => isDone == true);
                
                if (String.Compare(answer_id, AnswerId)==0)
                {
                    isCorrect.text = "Cevap Doğru";
                    Score += 5;
                    ScoreText.text = Score.ToString();

                    QuestionIndex++;
                    if (QuestionIndex >= ArrayLength)
                    {
                        StartCoroutine(_GamerUpdateScore(userID, Score));
                        //DashBoarda dön
                    }
                    else
                    {
                        PutQuestion();
                    }
                    
                }
                else
                {
                    isCorrect.text = "Cevap yanlış";
                }
            }
        }
    }
    // Update is called once per frame
    public IEnumerator DrawQuestions()
    {
        Debug.Log("DrawQuestions");
        WWWForm form = new WWWForm();
        form.AddField("difficulty_id", Difficulty);
        form.AddField("category_id", Category);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityGeoVise/draw_question.php", form)) //posting the AddField parameters to the php file
        {
            yield return www.SendWebRequest();  //Send the request and wait until the result is yielded

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text); //print the questions
                string jsonArray_str = www.downloadHandler.text;
                //Debug.Log(jsonArray + "isOkey1");
                //yield return new WaitUntil(() => isDone == true);
                QuestionAnswerArray = jsonArray_str.Split('*');
                //Debug.Log(QuestionArray[0]);
                //QuestionArray[0].Trim();
                QuestionIndex = 0;
                ArrayLength = QuestionAnswerArray.Length - 1;
                Score = 0;
                ScoreText.text = Score.ToString();
                PutQuestion();
                //callback(jsonArray);
            }
        }
    }
}
