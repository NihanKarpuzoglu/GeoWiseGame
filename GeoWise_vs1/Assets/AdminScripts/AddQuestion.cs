using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System;
public class AddQuestion : MonoBehaviour
{
    public GameObject Questioninputbox;
    
     public Dropdown[] Dropdowns;
     public string[] cities;
     public string[] difficulties;
     public string[] categories;
     public int difficulty;
     public int category;
     public int city;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetCities("http://localhost/UnityGeoVise/getcities.php"));
        StartCoroutine(GetDifficulties("http://localhost/UnityGeoVise/getdifficulties.php"));
        StartCoroutine(GetCategories("http://localhost/UnityGeoVise/getcategories.php"));
        
        //DifficultyDropdown = transform.GetComponent<Dropdown>();
        Dropdowns[0].options.Clear();//difficulty
        Dropdowns[1].options.Clear();//category
        Dropdowns[2].options.Clear();//city


        DifficultyDropdownItemSelected(Dropdowns[0]);
        CategoryDropdownItemSelected(Dropdowns[1]);
        CityDropdownItemSelected(Dropdowns[2]);

        Dropdowns[0].onValueChanged.AddListener(delegate { DifficultyDropdownItemSelected(Dropdowns[0]); });
        Dropdowns[1].onValueChanged.AddListener(delegate { CategoryDropdownItemSelected(Dropdowns[1]); });
        Dropdowns[2].onValueChanged.AddListener(delegate { CityDropdownItemSelected(Dropdowns[2]); });
    }

    


    public void CityDropdownItemSelected(Dropdown dropdown)//dropdown 2
    {

        
        int index=dropdown.value;
        city = Convert.ToInt32((dropdown.options[index].text).Split('-')[0]);

    }
    public void DifficultyDropdownItemSelected(Dropdown dropdown)//dropdown 0
    {
        int index = dropdown.value;
        difficulty = Convert.ToInt32((dropdown.options[index].text).Split('-')[0]);
    }
    public void CategoryDropdownItemSelected(Dropdown dropdown)//dropdown 1
    {
        int index = dropdown.value;
        category = Convert.ToInt32((dropdown.options[index].text).Split('-')[0]);
    }


    void Update()
    {

        DifficultyDropdownItemSelected(Dropdowns[0]);
        CategoryDropdownItemSelected(Dropdowns[1]);
        CityDropdownItemSelected(Dropdowns[2]);
       

    }

    public void onButtonClicked()
    {
        StartCoroutine(addQuestionEvent());
    }

    IEnumerator addQuestionEvent()
    {
       
         
        WWWForm form = new WWWForm();
       
        form.AddField("title", Questioninputbox.GetComponent<TMP_InputField>().text);
        form.AddField("difficultly_id", difficulty);
        form.AddField("category_id", category);
        form.AddField("city_id", city);
        //Debug.Log(Questioninputbox.GetComponent<TMP_InputField>().text);
        //Debug.Log(difficulty);
        //Debug.Log(category);
        //Debug.Log(city);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityGeoVise/addQuestion.php", form))
        {
            yield return www.SendWebRequest();
            string rawresponse = www.downloadHandler.text;
            Debug.Log(rawresponse);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                //
            }
        }
    }

    IEnumerator GetCities(string uri)
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
                    cities = rawresponse.Split('*');
                    for (int i = 0; i < cities.Length; i++)
                    {

                        if (cities[i] != "")
                        {
                            // Debug.Log("Current data: " + cities[i]);
                            string[] cityinfo = cities[i].Split(',');
                            Dropdowns[2].options.Add(new Dropdown.OptionData() { text = cityinfo[0]+"-"+cityinfo[1] });
                            //Debug.Log("Current data: " + cityinfo[1]);



                        }

                    }



                    break;
            }
        }
    }

    

    IEnumerator GetDifficulties(string uri)
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
                    difficulties = rawresponse.Split('*');
                    for (int i = 0; i < difficulties.Length; i++)
                    {

                        if (difficulties[i] != "")
                        {
                            // Debug.Log("Current data: " + cities[i]);
                            string[] difficultyinfo = difficulties[i].Split(',');
                            Dropdowns[0].options.Add(new Dropdown.OptionData() { text = difficultyinfo[0]+"-"+ difficultyinfo[1] });
                           // Debug.Log("Current data: " + difficultyinfo[1]);



                        }

                    }


                    break;
            }
        }
    }

    IEnumerator GetCategories(string uri)
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
                    categories = rawresponse.Split('*');
                    for (int i = 0; i < categories.Length; i++)
                    {

                        if (categories[i] != "")
                        {
                            // Debug.Log("Current data: " + cities[i]);
                            string[] categoryinfo = categories[i].Split(',');
                            Dropdowns[1].options.Add(new Dropdown.OptionData() { text = categoryinfo[0]+"-"+categoryinfo[1] });
                            //Debug.Log("Current data: " + categoryinfo[1]);



                        }

                    }



                    break;
            }
        }
    }


}
