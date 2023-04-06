using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListDropdown : MonoBehaviour
{

    [SerializeField] // Değişkene Inspector penceresinden erişilmesini sağlıyoruz.
    private string getCountryUrl = "";
    [SerializeField]
    private string getDifficultyUrl = "";
    [SerializeField]
    private string getCategoryUrl = "";
    [SerializeField]
    private string getUserInfoUrl = "";
    [SerializeField]
    private Text UserInfo;

    public Dropdown[] Dropdowns;

    public static int id;

    private string[] countries;//0. index
    private string[] difficulties;// 1.index
    private string[] categories;// 2.index

    private int difficulty;
    private int category;
    private int country;


    // Start is called before the first frame update
    void Start()
    {
        GetUserId(id);

        StartCoroutine(GetCountries(getCountryUrl));
        StartCoroutine(GetDifficulties(getDifficultyUrl));
        StartCoroutine(GetCategories(getCategoryUrl));

        Dropdowns[0].options.Clear();//difficulty
        Dropdowns[1].options.Clear();//category
        Dropdowns[2].options.Clear();//city

        /*Dropdowns[0].options.Add(new Dropdown.OptionData() { text = "1-Kolay"});
        Dropdowns[1].options.Add(new Dropdown.OptionData() { text = "1-Araç Plakaları"});
        Dropdowns[2].options.Add(new Dropdown.OptionData() { text = "1-Turkey"});*/

        CountryDropdownItemSelected(Dropdowns[0]);
        DifficultyDropdownItemSelected(Dropdowns[1]);
        CategoryDropdownItemSelected(Dropdowns[2]);

        Dropdowns[0].onValueChanged.AddListener(delegate { CountryDropdownItemSelected(Dropdowns[0]); });
        Dropdowns[1].onValueChanged.AddListener(delegate { DifficultyDropdownItemSelected(Dropdowns[1]); });
        Dropdowns[2].onValueChanged.AddListener(delegate { CategoryDropdownItemSelected(Dropdowns[2]); });

    }

    // Update is called once per frame
    void Update()
    {
        CountryDropdownItemSelected(Dropdowns[0]);
        DifficultyDropdownItemSelected(Dropdowns[1]);
        CategoryDropdownItemSelected(Dropdowns[2]);
    }

    public void onClickStart()//dropdown 2
    {
        //sahne değişeceke
        /*country
        difficulty
        category*/
        DrawQuestion.userID = id;
        DrawQuestion.Difficulty = Convert.ToString(difficulty);
        DrawQuestion.Category = Convert.ToString(category);

        
        PlayerPrefs.SetInt("userLog", 1);
        SceneManager.LoadScene("GamePlay"); 

    }
    public void CountryDropdownItemSelected(Dropdown dropdown)//dropdown 2
    {
        int index = dropdown.value;
        country = Convert.ToInt32((dropdown.options[index].text).Split('-')[0]);//id
    }
    public void DifficultyDropdownItemSelected(Dropdown dropdown)//dropdown 0
    {
        int index = dropdown.value;
        difficulty = Convert.ToInt32((dropdown.options[index].text).Split('-')[0]);//id
    }
    public void CategoryDropdownItemSelected(Dropdown dropdown)//dropdown 1
    {
        int index = dropdown.value;
        category = Convert.ToInt32((dropdown.options[index].text).Split('-')[0]);//id
    }

    public void GetUserId(int id)
    {
        Debug.Log(id);
        StartCoroutine(GetUserInfo(getUserInfoUrl, id));
    }

    IEnumerator GetUserInfo(string uri,int id)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
            WWWForm getUserInfo = new WWWForm(); // WWW Form oluşturuyorum
            getUserInfo.AddField("id", id); //id bilgisini forma ekliyorum


            UnityWebRequest www = UnityWebRequest.Post(uri, getUserInfo);
            yield return www.SendWebRequest();
            string retrievingMessage = www.downloadHandler.text;

            if (retrievingMessage == "-1")//kullanıcı aranırken hata oluştu
            {
                Debug.LogError("Hata!!");
            }
            else//username geldi
            {
                Debug.LogError(retrievingMessage); //
                UserInfo.text = retrievingMessage;

            }
        }
    }




    IEnumerator GetCountries(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string rawresponse = webRequest.downloadHandler.text;
            countries = rawresponse.Split('*');
            //Debug.Log(rawresponse);
            //Debug.Log(countries.Length);
            for (int i = 0; i < countries.Length; i++)
            {
                if (countries[i] != "")
                {
                    string[] continfo = countries[i].Split(',');
                    Dropdowns[0].options.Add(new Dropdown.OptionData() { text = continfo[0] + "-" + continfo[1] });
                    
                }

            }
        }
    }

    IEnumerator GetDifficulties(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string rawresponse = webRequest.downloadHandler.text;
            //Debug.Log(rawresponse);
            difficulties = rawresponse.Split('*');
            for (int i = 0; i < difficulties.Length; i++)
            {
                if (difficulties[i] != "")
                {
                    string[] diffinfo = difficulties[i].Split(',');
                    Dropdowns[1].options.Add(new Dropdown.OptionData() { text = diffinfo[0] + "-" + diffinfo[1] });
                }

            }
        }
    }

    IEnumerator GetCategories(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            string rawresponse = webRequest.downloadHandler.text;
            Debug.Log(rawresponse);

            categories = rawresponse.Split('*');
            for (int i = 0; i < categories.Length; i++)
            {
                if (categories[i] != "")
                {
                    string[] catginfo = categories[i].Split(',');
                    Dropdowns[2].options.Add(new Dropdown.OptionData() { text = catginfo[0] + "-" + catginfo[1] });
                    Debug.Log(catginfo[1]);
                }

            }
        }
    }

}

