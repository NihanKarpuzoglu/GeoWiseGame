using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; // Sahne değiştirmek için kullandığımız kütüphane
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine.UI;

public class MySQLHelper : MonoBehaviour
{
    public NotificationHelper _notificationHelper;//notification kullanmak için

    [SerializeField] // Değişkene Inspector penceresinden erişilmesini sağlıyoruz.
    private string hesapOlusturURL = ""; // signUp.php

    [SerializeField] // Değişkene Inspector penceresinden erişilmesini sağlıyoruz.
    private string girisYapURL = ""; // login.php

    [SerializeField] // Değişkene Inspector penceresinden erişilmesini sağlıyoruz.
    private string adminLoginURL = "";//adminLogin.Php

    [SerializeField] // Değişkene Inspector penceresinden erişilmesini sağlıyoruz.
    private string passwordUpdateUrl = ""; // passUpdate.php

    [SerializeField] // Değişkene Inspector penceresinden erişilmesini sağlıyoruz.
    private string emailExistUrl = "";  //emailExistorNot.php//şifre güncelleme yaparken emailin sistemde kayıtlı olup olmadığını kontrol eder

    // Her yerden ulaşabilmek için HesapOlustur ve GirisYap adında iki adet değişken oluşturuyor ve içerisine 
    // StartCoroutine içinde kullanacağımız asıl fonksiyonları çağırıyoruz.
    public void CreateAccount(string _createAccountUserName, string _createAccountEmail, string _createAccountPassword, string _createAccountPasswordAgain)
    {
        /*: İçine girilen coroutine fonksiyonu çalıştırır ve onun bitmesini bekler. 
         Ardından mevcut kodu çalıştırmaya kaldığı yerden devam eder.*/
        StartCoroutine(_CreateAccount(_createAccountUserName, _createAccountEmail, _createAccountPassword, _createAccountPasswordAgain));
    }
    public void GamerLogin(string _loginEmail, string _loginPassword)
    {
        StartCoroutine(_GamerLogin(_loginEmail, _loginPassword));
    }
    public void EmailExist(string _email)
    {
        StartCoroutine(_EmailExist(_email));
    }

    public void GamerUpdatePassword(string _updatePassEmail, string _newPassword)
    {
        StartCoroutine(_GamerUpdatePassword(_updatePassEmail, _newPassword));
    }
    public void AdminLogin(string adminMail, string adminPassword)
    {
        StartCoroutine(_AdminLogin(adminMail, adminPassword));
    }
    // Giriş yapıldıysa geçerli sahneyi yeniden yüklemesi için bir fonksiyon yazdım, 
    public void NewPage(int sceneID)//Login Scene için scene id:0 - Admin scene icin sceneId:1
    {
        if (sceneID == 0)//login page'e dönülmüş
        {
            PlayerPrefs.SetInt("adminLog", 0);
            PlayerPrefs.SetInt("userLog", 0);

            SceneManager.LoadScene("Scene2"); // Sahneyi yeniden yüklüyorum
        }
        if (sceneID == 1)//admin sahnesine geçiş
        {
            PlayerPrefs.SetInt("adminLog", 1); // 
                                               // yukarıdaki playerprefs 1 ise giriş yapılmış anlamına geliyor.
            SceneManager.LoadScene("AdminScene"); // Sahneyi yeniden yüklüyorum
        }
        if(sceneID == 2)//dashboard sahnesi: oyuna geçiş
        {
            PlayerPrefs.SetInt("userLog", 1);
            SceneManager.LoadScene("DashboardScene"); // Sahneyi yeniden yüklüyorum
        }
        
        
    }
    /*public void AdminNewPage(int sceneID, GameObject[] panels)
    {
        panels[0].SetActive(false); // ana paneli açar
        panels[1].SetActive(false); // hesap oluşturma panelini kapatır
        panels[2].SetActive(true); // admin giriş panelini açar
        /*PlayerPrefs.SetInt("logAdmin", 1); // 
        // yukarıdaki playerprefs 1 ise admin giriş yapılmış anlamına geliyor.
        SceneManager.LoadScene(sceneID, LoadSceneMode.Single); // Sahneyi yeniden yüklüyorum
    }*/

    IEnumerator _CreateAccount(string _createAccountUserName, string _createAccountEmail, string _createAccountPassword, string _createAccountPasswordAgain)
    {

        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm hesapOlusturmaForm = new WWWForm(); // WWW form oluşturuyorum

        hesapOlusturmaForm.AddField("username", _createAccountUserName); // Kullanıcı adı verisini forma ekliyorum
        hesapOlusturmaForm.AddField("mail", _createAccountEmail); // Mail verisini forma ekliyorum
        hesapOlusturmaForm.AddField("password", _createAccountPassword); // Şifre verisini forma ekliyorum
        hesapOlusturmaForm.AddField("repeatpassword", _createAccountPasswordAgain); // Şifre verisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(hesapOlusturURL, hesapOlusturmaForm);
        yield return www.SendWebRequest();
        //WWW veriGonder = new WWW(hesapOlusturURL, hesapOlusturmaForm); // WWW ile veri gönderilmesi için siteye bağlanıyorum
        //yield return veriGonder; // Veriyi gönderiyorum

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//aynı eposta ile hesap açılmış
        { // Siteden 1 yanıtı geldiyse
            Debug.LogWarning("Bu Epostaya ait bir hesap mevcut."); //eposta inputfieldını temizle
            string notification = "Bu Epostaya ait bir hesap mevcut!!!";
            _notificationHelper.showNotification(notification, 3);
        }
        else if (retrievingMessage == "-2")//kullanıcı adı daha önceden alınmış//kullanıcı adı inputfieldını temizle
        {
            Debug.LogWarning("Bu kullanıcı adı daha önceden alınmış."); // Siteden 1 dışında bir yanıt gelirse sorun var yazısı yazıdırıyorum
            string notification = "Bu kullanıcı adı daha önceden alınmış!!!";
            _notificationHelper.showNotification(notification, 3);
        }
        else if (retrievingMessage == "1")
        {
            Debug.Log("Hesap oluşturma başarılı.");//textboxları temizle
            string notification = "Hesap oluşturma başarılı";
            _notificationHelper.showNotification(notification, 3);
        }
        else
        {
            Debug.LogError("Hesap oluşturma başarısız.");//textboxları temizle
            string notification = "Hesap oluşturma başarısız!!";
            _notificationHelper.showNotification(notification, 3);
        }


    }

    IEnumerator _GamerLogin(string _loginEmail, string _loginPassword)
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm girisYapForm = new WWWForm(); // WWW Form oluşturuyorum
        girisYapForm.AddField("Email", _loginEmail); // Kullanıcı adı bilgisini forma ekliyorum
        girisYapForm.AddField("Password", _loginPassword); // Şifre bilgisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(girisYapURL, girisYapForm);
        yield return www.SendWebRequest();

        /*WWW veriGonder = new WWW(girisYapURL, girisYapForm); // Forma eklediğim verileri WWW ile siteye gönderiyorum
        yield return veriGonder; // Siteye verileri gönderdim*/

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//kullanıcı aranırken hata oluştu
        {
            Debug.LogError("Hata!!");
            string notification = "Hata!!";
            _notificationHelper.showNotification(notification, 3);
        }
        else if (retrievingMessage == "-2")
        {
            Debug.LogError("Hatalı giriş bilgileri!!"); //
            string notification = "Hatalı giriş bilgileri!!";
            _notificationHelper.showNotification(notification, 3);
        }
        else if (retrievingMessage == "1")
        {
            int newScene = 2;
            Debug.Log("Giriş başarılı"); //giriş yapılan sayfaya yönlendir//dashboarda, id'si 2;
            string notification = "Giriş başarılı";
            _notificationHelper.showNotification(notification, 3);
            NewPage(newScene);
        }
        else
        {
            Debug.LogError("Hata!"); //
            string notification = "Hata";
            _notificationHelper.showNotification(notification, 3);    
        }
    }
    IEnumerator _EmailExist(string _email)//şifre değiştirme işleminde o hesaba kayıtlı kullanıcı var mı diye bakılıyor
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm emailExistForm = new WWWForm(); // WWW form oluşturuyorum

        emailExistForm.AddField("mail", _email); // Mail verisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(emailExistUrl, emailExistForm);
        yield return www.SendWebRequest();
        //WWW veriGonder = new WWW(hesapOlusturURL, hesapOlusturmaForm); // WWW ile veri gönderilmesi için siteye bağlanıyorum
        //yield return veriGonder; // Veriyi gönderiyorum

        string retrievingMessage = www.downloadHandler.text;

        if (retrievingMessage == "-1")
        { // Siteden -1 yanıtı geldiyse
            Debug.LogWarning("Bu email adresine kayıtlı kullanıcı bulunamadı!!"); //eposta inputfieldını temizle
            string notification = "Bu email adresine kayıtlı kullanıcı bulunamadı!!";
            _notificationHelper.showNotification(notification, 3);
        }
        else
        {
            Debug.Log("Yeni şifreniz gönderiliyor..");//textboxları temizle
            string notification = "Yeni şifreniz gönderiliyor..";
            _notificationHelper.showNotification(notification, 3);
            SendUpdatedPassword(_email);//mail gönder
        }

    }
    IEnumerator _GamerUpdatePassword(string _updatePassEmail, string _newPassword)
    {

        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm updatePassForm = new WWWForm(); // WWW form oluşturuyorum

        updatePassForm.AddField("mail", _updatePassEmail); // Mail verisini forma ekliyorum
        updatePassForm.AddField("password", _newPassword); // Şifre verisini forma ekliyorum
       
        UnityWebRequest www = UnityWebRequest.Post(passwordUpdateUrl, updatePassForm);
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
            string notification = "Yeni şifrenizle sisteme girişi yapabilirsiniz.";
            _notificationHelper.showNotification(notification, 3);
        }

    }
    
    IEnumerator _AdminLogin(string adminMail, string adminPassword)
    {
        yield return new WaitForEndOfFrame(); // Son karenin gelmesi bekleniyor
        WWWForm adminLoginForm = new WWWForm(); // WWW Form oluşturuyorum
        adminLoginForm.AddField("Email", adminMail); // Email adı bilgisini forma ekliyorum
        adminLoginForm.AddField("Password", adminPassword); // Şifre bilgisini forma ekliyorum

        UnityWebRequest www = UnityWebRequest.Post(adminLoginURL, adminLoginForm);
        yield return www.SendWebRequest();
        /*WWW veriGonder = new WWW(adminLoginURL, adminLoginForm); // Forma eklediğim verileri WWW ile siteye gönderiyorum
        yield return veriGonder; // Siteye verileri gönderdim*/

        string retrievingMessage = www.downloadHandler.text;
        if (retrievingMessage == "-1")//kullanıcı aranırken hata oluştu
        {
            //Debug.LogError("Hata!!");

        }
        else if (retrievingMessage == "-2")
        {
            Debug.LogError("Hatalı giriş bilgileri!!"); //
            string notification = "Hatalı giriş bilgileri!!";
            _notificationHelper.showNotification(notification, 3);
        }
        else if (retrievingMessage == "1")
        {
            int newScene = 1;
            //Debug.Log("Giriş başarılı"); //admin sayfasına yönlendirme, id'si 1
            NewPage(newScene);
        }
        else
        {
            //Debug.LogError("Hata!"); //
        }
    }

    private void SendUpdatedPassword(string _loginEmail)
    {
        try
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Timeout = 10000;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;

            mail.From = new MailAddress("geowiseunitygame@gmail.com");
            mail.To.Add(new MailAddress(_loginEmail));

            /*new password*/
            /*string pattern_pass = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
             * fare kütüphanesi
            var xeger = new Xeger(pattern_pass);
            var generatedString = xeger.Generate();*/
            string newPassword = "yyyy";

            mail.Subject = "GeoWise New Password";
            mail.Body = "Yeni parolanız: " + newPassword + "\nOyuna bu şifreyle giriş yapabilirsiniz";

            SmtpServer.Credentials = new System.Net.NetworkCredential("geowiseunitygame@gmail.com", "geowiseunitygame22") as ICredentialsByHost; ;
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            SmtpServer.Send(mail);//bundan sonra bu kullanıcıya ait şifre sistemden güncellenmeli*/
            GamerUpdatePassword(_loginEmail, newPassword);

        }
        catch (Exception)
        {
            /*Error handling*/
        }
    }

}



