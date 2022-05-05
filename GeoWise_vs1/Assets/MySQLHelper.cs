using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; // Sahne de�i�tirmek i�in kulland���m�z k�t�phane

public class MySQLHelper : MonoBehaviour
{
    [SerializeField] // De�i�kene Inspector penceresinden eri�ilmesini sa�l�yoruz.
    private string hesapOlusturURL = ""; // signUp.php

    [SerializeField] // De�i�kene Inspector penceresinden eri�ilmesini sa�l�yoruz.
    private string girisYapURL = ""; // login.php

    [SerializeField] // De�i�kene Inspector penceresinden eri�ilmesini sa�l�yoruz.
    private string adminLoginURL = "";//adminLogin.Php

    // Her yerden ula�abilmek i�in HesapOlustur ve GirisYap ad�nda iki adet de�i�ken olu�turuyor ve i�erisine 
    // StartCoroutine i�inde kullanaca��m�z as�l fonksiyonlar� �a��r�yoruz.
    public void CreateAccount(string _createAccountUserName, string _createAccountEmail, string _createAccountPassword, string _createAccountPasswordAgain)
    {
        /*: ��ine girilen coroutine fonksiyonu �al��t�r�r ve onun bitmesini bekler. 
         Ard�ndan mevcut kodu �al��t�rmaya kald��� yerden devam eder.*/
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
    // Giri� yap�ld�ysa ge�erli sahneyi yeniden y�klemesi i�in bir fonksiyon yazd�m, 
    public void NewPage(int sceneID)//Login Scene i�in scene id:0 - Admin scene icin sceneId:1
    {
        if (sceneID == 0)//login page'e d�n�lm��
        {
            PlayerPrefs.SetInt("adminLog", 0);
            PlayerPrefs.SetInt("userLog", 0);

            SceneManager.LoadScene("Scene2"); // Sahneyi yeniden y�kl�yorum
        }
        if (sceneID == 1)//admin sahnesine ge�i�
        {
            PlayerPrefs.SetInt("adminLog", 1); // 
                                               // yukar�daki playerprefs 1 ise giri� yap�lm�� anlam�na geliyor.
            SceneManager.LoadScene("AdminScene"); // Sahneyi yeniden y�kl�yorum
        }
        if(sceneID == 2)//dashboard sahnesi: oyuna ge�i�
        {
            PlayerPrefs.SetInt("userLog", 1);
            SceneManager.LoadScene("DashboardScene"); // Sahneyi yeniden y�kl�yorum
        }
        
        
    }
    /*public void AdminNewPage(int sceneID, GameObject[] panels)
    {
        panels[0].SetActive(false); // ana paneli a�ar
        panels[1].SetActive(false); // hesap olu�turma panelini kapat�r
        panels[2].SetActive(true); // admin giri� panelini a�ar
        /*PlayerPrefs.SetInt("logAdmin", 1); // 
        // yukar�daki playerprefs 1 ise admin giri� yap�lm�� anlam�na geliyor.
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single); // Sahneyi yeniden y�kl�yorum
    }*/

    IEnumerator _CreateAccount(string _createAccountUserName, string _createAccountEmail, string _createAccountPassword, string _createAccountPasswordAgain)
    {

        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm hesapOlusturmaForm = new WWWForm(); // WWW form olu�turuyorum

        hesapOlusturmaForm.AddField("username", _createAccountUserName); // Kullan�c� ad� verisini forma ekliyorum
        hesapOlusturmaForm.AddField("mail", _createAccountEmail); // Mail verisini forma ekliyorum
        hesapOlusturmaForm.AddField("password", _createAccountPassword); // �ifre verisini forma ekliyorum
        hesapOlusturmaForm.AddField("repeatpassword", _createAccountPasswordAgain); // �ifre verisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(hesapOlusturURL, hesapOlusturmaForm);
        yield return www.SendWebRequest();
        //WWW veriGonder = new WWW(hesapOlusturURL, hesapOlusturmaForm); // WWW ile veri g�nderilmesi i�in siteye ba�lan�yorum
        //yield return veriGonder; // Veriyi g�nderiyorum

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//ayn� eposta ile hesap a��lm��
        { // Siteden 1 yan�t� geldiyse
            Debug.LogWarning("Bu Epostaya ait bir hesap mevcut."); //eposta inputfield�n� temizle
        }
        else if (retrievingMessage == "-2")//kullan�c� ad� daha �nceden al�nm��//kullan�c� ad� inputfield�n� temizle
        {
            Debug.LogWarning("Bu kullan�c� ad� daha �nceden al�nm��."); // Siteden 1 d���nda bir yan�t gelirse sorun var yaz�s� yaz�d�r�yorum
        }
        else if (retrievingMessage == "1")
        {
            Debug.Log("Hesap olu�turma ba�ar�l�.");//textboxlar� temizle
        }
        else
        {
            Debug.LogError("Hesap olu�turma ba�ar�s�z.");//textboxlar� temizle
        }


    }

    IEnumerator _GamerLogin(string _loginEmail, string _loginPassword)
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm girisYapForm = new WWWForm(); // WWW Form olu�turuyorum
        girisYapForm.AddField("Email", _loginEmail); // Kullan�c� ad� bilgisini forma ekliyorum
        girisYapForm.AddField("Password", _loginPassword); // �ifre bilgisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(girisYapURL, girisYapForm);
        yield return www.SendWebRequest();

        /*WWW veriGonder = new WWW(girisYapURL, girisYapForm); // Forma ekledi�im verileri WWW ile siteye g�nderiyorum
        yield return veriGonder; // Siteye verileri g�nderdim*/

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//kullan�c� aran�rken hata olu�tu
        {
            Debug.LogError("Hata!!"); 
        }
        else if (retrievingMessage == "-2")
        {
            Debug.LogError("Hatal� giri� bilgileri!!"); //
        }
        else if (retrievingMessage == "1")
        {
            int newScene = 2;
            Debug.Log("Giri� ba�ar�l�"); //giri� yap�lan sayfaya y�nlendir//dashboarda, id'si 2;
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
        WWWForm adminLoginForm = new WWWForm(); // WWW Form olu�turuyorum
        adminLoginForm.AddField("Email", adminMail); // Email ad� bilgisini forma ekliyorum
        adminLoginForm.AddField("Password", adminPassword); // �ifre bilgisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(adminLoginURL, adminLoginForm);
        yield return www.SendWebRequest();
        /*WWW veriGonder = new WWW(adminLoginURL, adminLoginForm); // Forma ekledi�im verileri WWW ile siteye g�nderiyorum
        yield return veriGonder; // Siteye verileri g�nderdim*/

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//kullan�c� aran�rken hata olu�tu
        {
            Debug.LogError("Hata!!");
        }
        else if (retrievingMessage == "-2")
        {
            Debug.LogError("Hatal� giri� bilgileri!!"); //
        }
        else if (retrievingMessage == "1")
        {
            int newScene = 1;
            Debug.Log("Giri� ba�ar�l�"); //admin sayfas�na y�nlendirme, id'si 1
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
