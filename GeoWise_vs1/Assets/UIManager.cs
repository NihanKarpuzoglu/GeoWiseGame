using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class UIManager : MonoBehaviour
{
    public GameObject[] panels; //panellerin içinde tutulduðu dizi
    public MySQLHelper _mysqlHelper; //mysqlhelper fonksiyonlarýný kullanmak için
    
    public InputField createAccountUserName; // Hesap oluþtur paneli kullanýcý adý girdisi
    public InputField createAccountPassword; // Hesap oluþtur paneli þifre girdisi
    public InputField createAccountPasswordAgain; // Hesap oluþtur paneli tekrar þifre
    public InputField createAccountEmail; // Hesap oluþtur paneli e posta girdisi

    public InputField loginEmail; // Giriþ paneli kullaný adý girdisi
    public InputField loginPassword; // Giriþ paneli þifre girdisi

    //admin bilgileri
    public InputField adminMail; 
    public InputField adminPassword;

    private string _createAccountUserName; // Hesap oluþtur paneli kullanýcý adý girdisi
    private string _createAccountPassword; // Hesap oluþtur paneli þifre girdisi
    private string _createAccountPasswordAgain; // Hesap oluþtur paneli þifre tekrar
    private string _createAccountEmail; // Hesap oluþtur paneli e posta girdisi

    private string _loginEmail; // Giriþ paneli kullaný adý girdisi
    private string _loginPassword; // Giriþ paneli þifre girdisi

    private string _adminMail;
    private string _adminPassword; 


    // Panellerin sýralanýþý
    // 0 AnaEkran:Giriþ Paneli Burda. Burdan yeni hesap oluþtur butonuyla diðer panele geçiliyor
    // 1 Hesap Oluþturma Paneli


    /*Bu fonksiyon daima Start fonksiyonundan önce çalýþtýrýlýr, ayrýca bir 
    prefab Instantiate edildiði anda da çalýþtýrýlýr. 
    (Eðer GameObject aktif (active) deðilse bu fonksiyon 
    obje aktif olana kadar ya da bu objedeki bir scriptte yer alan 
    bir fonksiyon dýþarýdan çaðrýlana kadar çalýþtýrýlmaz.)
    obje pasif olsa bile çalýþýr. Her halükarda çalýþýr*/
    void Awake()
    {
        if (PlayerPrefs.GetInt("UserLog") == 1)//doðruysa giriþ zaten yapýlmýþ
        { // Kullanýcý giriþ yapmýþ mý kontrol ediyorum
            LoginScreenClose(); // Giriþ yapmýþsa panelleri kapat,oyuncu panelini aç
        }
    }

    // Giriþ ve hesap oluþtur panelini kapatmak için
    public void LoginScreenClose()
    {
        panels[0].SetActive(false); // ana paneli kapatýr
        panels[1].SetActive(false); // hesap oluþturma panelini kapatýr
        //paneller[2].SetActive(false); // hesap oluþturma panelini kapatýr

    }
    // Oyuncu Giriþi butonuna týklandýðý zaman çalýþan fonksiyon
    public void GamerLogin()//oyuncu giriþi butonuna baðlý
    {
        if(loginEmail.text!="" && loginPassword.text != "")
        {
            //??
            _loginEmail = loginEmail.text; // Giriþ paneli kullaný adý girdisi
            _loginPassword = loginPassword.text; // Giriþ paneli þifre girdisi

            _mysqlHelper.GamerLogin(_loginEmail, _loginPassword); // MYSQLHelper scriptindeki giriþyap fonksiyonunu çaðýrýyor ve gerekli bilgileri gönderiyor
        }
        else
        {
            Debug.LogWarning("Tüm alanlarý doldurduðunuzdan emin olun!!!");
        }
    }

    // Oyuncu Giriþi butonuna týklandýðý zaman çalýþan fonksiyon
    public void AdminLogin()//yönetici giriþi butonuna baðlý
    {
        if (adminMail.text != "" && adminPassword.text != "")
        {

            //??Private deðiþkenlere atama
            _adminMail = adminMail.text; 
            _adminPassword = adminPassword.text;

            _mysqlHelper.AdminLogin(_adminMail, _adminPassword);
            // MYSQLHelper scriptindeki admin giriþi fonksiyonu çaðýrýlýyor ve gerekli bilgileri gönderiliyor
        }
        else
        {
            Debug.LogWarning("Tüm alanlarý doldurduðunuzdan emin olun!!!");
        }
    }

    // Hesap oluþtur butonuna týklandýðý zaman çalýþan fonksiyon
    public void CreateAccount()
    {
        if(createAccountUserName.text!="" && createAccountEmail.text!="" && createAccountPassword.text!="" && createAccountPasswordAgain.text!="")
        {
            if(createAccountPassword.text == createAccountPasswordAgain.text)//þifreler eþleþiyor mu?
            {
                //??
                _createAccountUserName = createAccountUserName.text; // Hesap oluþtur paneli kullanýcý adý girdisi
                _createAccountEmail = createAccountEmail.text; // Hesap oluþtur paneli e posta girdisi
                _createAccountPassword = createAccountPassword.text; // Hesap oluþtur paneli þifre girdisi
                _createAccountPasswordAgain = createAccountPasswordAgain.text; // Hesap oluþtur paneli þifre girdisi tekrar

                //parola regex denetleme//parola min 6 karakterden oluþmalý. Ýçinde en az 1 harf ve 1 sayý içermeli.
                /*// Minimum eight characters, at least one letter and one number:
                 * "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"*/

                string pattern_pass = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
                string pattern_email = @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
                Regex regex = new Regex(pattern_pass);
                Regex regex2 = new Regex(pattern_email);
                if (Regex.Match(_createAccountPassword, pattern_pass).Success)//true dönerse þifre kýsýtlara uyuyor
                {
                    if(Regex.Match(_createAccountEmail, pattern_email).Success)//email kontrolü
                    {//her þey doðru geçerli gözüküyor. Veri tabaný iþlemlerini baþlat
                        _mysqlHelper.CreateAccount(_createAccountUserName, _createAccountEmail, _createAccountPassword, _createAccountPasswordAgain);
                        // MySQL helper scriptinde CreateAccount oluþtur fonksiyonu çaðýrýlýyor
                    }
                    else
                    {
                        Debug.LogWarning("Geçerli bir email adresi girin!");
                    }

                }
                else
                {
                    Debug.LogWarning("Þifre min 6 karakterden oluþmalý, en az 1 harf ve 1 sayý içermeli!");
                }



            }
            else
            {
                Debug.LogWarning("Þifreler eþleþmiyor!!!");
            }

        }
        else
        {
            Debug.LogWarning("Tüm alanlarý doldurduðunuzdan emin olun!!!");
        }
        
    }


    // Giriþ yap panelini açar veya kapatýr
    public void LoginPanelOpenClose()//Giriþ Yap paneli
    {
        if (panels[0].activeSelf)
        {
            panels[0].SetActive(false);//ilk ekran
            panels[1].SetActive(true);
        }
        else if (panels[0].activeSelf == false)//giriþ yap panelini açar.
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);

        }
    }


    // Kayýt ol panelini açar veya kapatýr
    public void RegPanelOpenClose()
    {
        if (panels[0].activeSelf)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
        }
        else if (panels[0].activeSelf == false)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
        }
    }

    /*Start metodu, Update fonksiyonu henüz hiç çalýþtýrýlmamýþken tek seferlik gerçekleþir 
     (eðer script enabled ise). Yani Update’ten önce çalýþtýrýlýr.*/

    //awake'in aksine bulunulan hücre pasifse çalýþmaz
    void Start()
    {
        panels[0].SetActive(true); // ana paneli açar
        panels[1].SetActive(false); // hesap oluþturma panelini kapatýr
    }
    
    // Update is called once per frame
    //Update: Her frame’de tek bir kez çalýþtýrýlýr.En sýk kullanýlan Update çeþididir.
    void Update()
    {
        //bunu nerde yapmak mantýklý ??
        /*_hesapOlusturKullaniciAdi = hesapOlusturKullaniciAdi.text; // Hesap oluþtur paneli kullanýcý adý girdisi
        _hesapOlusturSifre = hesapOlusturSifre.text; // Hesap oluþtur paneli þifre girdisi
        _hesapOlusturSifreAgain = hesapOlusturSifreAgain.text; // Hesap oluþtur paneli e posta girdisi
        _hesapOlusturEPosta = hesapOlusturEPosta.text; // Hesap oluþtur paneli e posta girdisi
        _girisEposta = girisEposta.text; // Giriþ paneli kullaný adý girdisi
        _girisYapSifre = girisYapSifre.text; // Giriþ paneli þifre girdisi*/

    }
}
