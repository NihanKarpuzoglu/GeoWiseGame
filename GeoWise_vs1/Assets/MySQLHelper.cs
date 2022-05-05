using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; // Sahne deðiþtirmek için kullandýðýmýz kütüphane

public class MySQLHelper : MonoBehaviour
{
    [SerializeField] // Deðiþkene Inspector penceresinden eriþilmesini saðlýyoruz.
    private string hesapOlusturURL = ""; // signUp.php

    [SerializeField] // Deðiþkene Inspector penceresinden eriþilmesini saðlýyoruz.
    private string girisYapURL = ""; // login.php

    [SerializeField] // Deðiþkene Inspector penceresinden eriþilmesini saðlýyoruz.
    private string adminLoginURL = "";//adminLogin.Php

    // Her yerden ulaþabilmek için HesapOlustur ve GirisYap adýnda iki adet deðiþken oluþturuyor ve içerisine 
    // StartCoroutine içinde kullanacaðýmýz asýl fonksiyonlarý çaðýrýyoruz.
    public void CreateAccount(string _createAccountUserName, string _createAccountEmail, string _createAccountPassword, string _createAccountPasswordAgain)
    {
        /*: Ýçine girilen coroutine fonksiyonu çalýþtýrýr ve onun bitmesini bekler. 
         Ardýndan mevcut kodu çalýþtýrmaya kaldýðý yerden devam eder.*/
        StartCoroutine(_CreateAccount(_createAccountUserName, _createAccountEmail, _createAccountPassword, _createAccountPasswordAgain));
    }
    public void GamerLogin(string _loginEmail, string _loginPassword)
    {
        StartCoroutine(_GamerLogin(_loginEmail, _loginPassword));
    }
    public void AdminLogin(string adminMail, string adminPassword)
    {
        StartCoroutine(_AdminLogin(adminMail, adminPassword));
    }
    // Giriþ yapýldýysa geçerli sahneyi yeniden yüklemesi için bir fonksiyon yazdým, 
    public void NewPage(int sceneID)//Login Scene için scene id:0 - Admin scene icin sceneId:1
    {
        if (sceneID == 0)//login page'e dönülmüþ
        {
            PlayerPrefs.SetInt("adminLog", 0);
            PlayerPrefs.SetInt("userLog", 0);

            SceneManager.LoadScene("Scene2"); // Sahneyi yeniden yüklüyorum
        }
        if (sceneID == 1)//admin sahnesine geçiþ
        {
            PlayerPrefs.SetInt("adminLog", 1); // 
                                               // yukarýdaki playerprefs 1 ise giriþ yapýlmýþ anlamýna geliyor.
            SceneManager.LoadScene("AdminScene"); // Sahneyi yeniden yüklüyorum
        }
        if(sceneID == 2)//dashboard sahnesi: oyuna geçiþ
        {
            PlayerPrefs.SetInt("userLog", 1);
            SceneManager.LoadScene("DashboardScene"); // Sahneyi yeniden yüklüyorum
        }
        
        
    }
    /*public void AdminNewPage(int sceneID, GameObject[] panels)
    {
        panels[0].SetActive(false); // ana paneli açar
        panels[1].SetActive(false); // hesap oluþturma panelini kapatýr
        panels[2].SetActive(true); // admin giriþ panelini açar
        /*PlayerPrefs.SetInt("logAdmin", 1); // 
        // yukarýdaki playerprefs 1 ise admin giriþ yapýlmýþ anlamýna geliyor.
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single); // Sahneyi yeniden yüklüyorum
    }*/

    IEnumerator _CreateAccount(string _createAccountUserName, string _createAccountEmail, string _createAccountPassword, string _createAccountPasswordAgain)
    {

        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm hesapOlusturmaForm = new WWWForm(); // WWW form oluþturuyorum

        hesapOlusturmaForm.AddField("username", _createAccountUserName); // Kullanýcý adý verisini forma ekliyorum
        hesapOlusturmaForm.AddField("mail", _createAccountEmail); // Mail verisini forma ekliyorum
        hesapOlusturmaForm.AddField("password", _createAccountPassword); // Þifre verisini forma ekliyorum
        hesapOlusturmaForm.AddField("repeatpassword", _createAccountPasswordAgain); // Þifre verisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(hesapOlusturURL, hesapOlusturmaForm);
        yield return www.SendWebRequest();
        //WWW veriGonder = new WWW(hesapOlusturURL, hesapOlusturmaForm); // WWW ile veri gönderilmesi için siteye baðlanýyorum
        //yield return veriGonder; // Veriyi gönderiyorum

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//ayný eposta ile hesap açýlmýþ
        { // Siteden 1 yanýtý geldiyse
            Debug.LogWarning("Bu Epostaya ait bir hesap mevcut."); //eposta inputfieldýný temizle
        }
        else if (retrievingMessage == "-2")//kullanýcý adý daha önceden alýnmýþ//kullanýcý adý inputfieldýný temizle
        {
            Debug.LogWarning("Bu kullanýcý adý daha önceden alýnmýþ."); // Siteden 1 dýþýnda bir yanýt gelirse sorun var yazýsý yazýdýrýyorum
        }
        else if (retrievingMessage == "1")
        {
            Debug.Log("Hesap oluþturma baþarýlý.");//textboxlarý temizle
        }
        else
        {
            Debug.LogError("Hesap oluþturma baþarýsýz.");//textboxlarý temizle
        }


    }

    IEnumerator _GamerLogin(string _loginEmail, string _loginPassword)
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm girisYapForm = new WWWForm(); // WWW Form oluþturuyorum
        girisYapForm.AddField("Email", _loginEmail); // Kullanýcý adý bilgisini forma ekliyorum
        girisYapForm.AddField("Password", _loginPassword); // Þifre bilgisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(girisYapURL, girisYapForm);
        yield return www.SendWebRequest();

        /*WWW veriGonder = new WWW(girisYapURL, girisYapForm); // Forma eklediðim verileri WWW ile siteye gönderiyorum
        yield return veriGonder; // Siteye verileri gönderdim*/

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//kullanýcý aranýrken hata oluþtu
        {
            Debug.LogError("Hata!!"); 
        }
        else if (retrievingMessage == "-2")
        {
            Debug.LogError("Hatalý giriþ bilgileri!!"); //
        }
        else if (retrievingMessage == "1")
        {
            int newScene = 2;
            Debug.Log("Giriþ baþarýlý"); //giriþ yapýlan sayfaya yönlendir//dashboarda, id'si 2;
            NewPage(newScene);
        }
        else
        {
            Debug.LogError("Hata!"); //
        }
    }
    IEnumerator _AdminLogin(string adminMail, string adminPassword)
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm adminLoginForm = new WWWForm(); // WWW Form oluþturuyorum
        adminLoginForm.AddField("Email", adminMail); // Email adý bilgisini forma ekliyorum
        adminLoginForm.AddField("Password", adminPassword); // Þifre bilgisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(adminLoginURL, adminLoginForm);
        yield return www.SendWebRequest();
        /*WWW veriGonder = new WWW(adminLoginURL, adminLoginForm); // Forma eklediðim verileri WWW ile siteye gönderiyorum
        yield return veriGonder; // Siteye verileri gönderdim*/

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//kullanýcý aranýrken hata oluþtu
        {
            Debug.LogError("Hata!!");
        }
        else if (retrievingMessage == "-2")
        {
            Debug.LogError("Hatalý giriþ bilgileri!!"); //
        }
        else if (retrievingMessage == "1")
        {
            int newScene = 1;
            Debug.Log("Giriþ baþarýlý"); //admin sayfasýna yönlendirme, id'si 1
            NewPage(newScene);
        }
        else
        {
            Debug.LogError("Hata!"); //
        }
    }

    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
