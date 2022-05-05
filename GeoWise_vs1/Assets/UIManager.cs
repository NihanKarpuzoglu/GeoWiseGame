using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class UIManager : MonoBehaviour
{
    public GameObject[] panels; //panellerin i�inde tutuldu�u dizi
    public MySQLHelper _mysqlHelper; //mysqlhelper fonksiyonlar�n� kullanmak i�in
    
    public InputField createAccountUserName; // Hesap olu�tur paneli kullan�c� ad� girdisi
    public InputField createAccountPassword; // Hesap olu�tur paneli �ifre girdisi
    public InputField createAccountPasswordAgain; // Hesap olu�tur paneli tekrar �ifre
    public InputField createAccountEmail; // Hesap olu�tur paneli e posta girdisi

    public InputField loginEmail; // Giri� paneli kullan� ad� girdisi
    public InputField loginPassword; // Giri� paneli �ifre girdisi

    //admin bilgileri
    public InputField adminMail; 
    public InputField adminPassword;

    private string _createAccountUserName; // Hesap olu�tur paneli kullan�c� ad� girdisi
    private string _createAccountPassword; // Hesap olu�tur paneli �ifre girdisi
    private string _createAccountPasswordAgain; // Hesap olu�tur paneli �ifre tekrar
    private string _createAccountEmail; // Hesap olu�tur paneli e posta girdisi

    private string _loginEmail; // Giri� paneli kullan� ad� girdisi
    private string _loginPassword; // Giri� paneli �ifre girdisi

    private string _adminMail;
    private string _adminPassword; 


    // Panellerin s�ralan���
    // 0 AnaEkran:Giri� Paneli Burda. Burdan yeni hesap olu�tur butonuyla di�er panele ge�iliyor
    // 1 Hesap Olu�turma Paneli


    /*Bu fonksiyon daima Start fonksiyonundan �nce �al��t�r�l�r, ayr�ca bir 
    prefab Instantiate edildi�i anda da �al��t�r�l�r. 
    (E�er GameObject aktif (active) de�ilse bu fonksiyon 
    obje aktif olana kadar ya da bu objedeki bir scriptte yer alan 
    bir fonksiyon d��ar�dan �a�r�lana kadar �al��t�r�lmaz.)
    obje pasif olsa bile �al���r. Her hal�karda �al���r*/
    void Awake()
    {
        if (PlayerPrefs.GetInt("UserLog") == 1)//do�ruysa giri� zaten yap�lm��
        { // Kullan�c� giri� yapm�� m� kontrol ediyorum
            LoginScreenClose(); // Giri� yapm��sa panelleri kapat,oyuncu panelini a�
        }
    }

    // Giri� ve hesap olu�tur panelini kapatmak i�in
    public void LoginScreenClose()
    {
        panels[0].SetActive(false); // ana paneli kapat�r
        panels[1].SetActive(false); // hesap olu�turma panelini kapat�r
        //paneller[2].SetActive(false); // hesap olu�turma panelini kapat�r

    }
    // Oyuncu Giri�i butonuna t�kland��� zaman �al��an fonksiyon
    public void GamerLogin()//oyuncu giri�i butonuna ba�l�
    {
        if(loginEmail.text!="" && loginPassword.text != "")
        {
            //??
            _loginEmail = loginEmail.text; // Giri� paneli kullan� ad� girdisi
            _loginPassword = loginPassword.text; // Giri� paneli �ifre girdisi

            _mysqlHelper.GamerLogin(_loginEmail, _loginPassword); // MYSQLHelper scriptindeki giri�yap fonksiyonunu �a��r�yor ve gerekli bilgileri g�nderiyor
        }
        else
        {
            Debug.LogWarning("T�m alanlar� doldurdu�unuzdan emin olun!!!");
        }
    }

    // Oyuncu Giri�i butonuna t�kland��� zaman �al��an fonksiyon
    public void AdminLogin()//y�netici giri�i butonuna ba�l�
    {
        if (adminMail.text != "" && adminPassword.text != "")
        {

            //??Private de�i�kenlere atama
            _adminMail = adminMail.text; 
            _adminPassword = adminPassword.text;

            _mysqlHelper.AdminLogin(_adminMail, _adminPassword);
            // MYSQLHelper scriptindeki admin giri�i fonksiyonu �a��r�l�yor ve gerekli bilgileri g�nderiliyor
        }
        else
        {
            Debug.LogWarning("T�m alanlar� doldurdu�unuzdan emin olun!!!");
        }
    }

    // Hesap olu�tur butonuna t�kland��� zaman �al��an fonksiyon
    public void CreateAccount()
    {
        if(createAccountUserName.text!="" && createAccountEmail.text!="" && createAccountPassword.text!="" && createAccountPasswordAgain.text!="")
        {
            if(createAccountPassword.text == createAccountPasswordAgain.text)//�ifreler e�le�iyor mu?
            {
                //??
                _createAccountUserName = createAccountUserName.text; // Hesap olu�tur paneli kullan�c� ad� girdisi
                _createAccountEmail = createAccountEmail.text; // Hesap olu�tur paneli e posta girdisi
                _createAccountPassword = createAccountPassword.text; // Hesap olu�tur paneli �ifre girdisi
                _createAccountPasswordAgain = createAccountPasswordAgain.text; // Hesap olu�tur paneli �ifre girdisi tekrar

                //parola regex denetleme//parola min 6 karakterden olu�mal�. ��inde en az 1 harf ve 1 say� i�ermeli.
                /*// Minimum eight characters, at least one letter and one number:
                 * "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"*/

                string pattern_pass = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
                string pattern_email = @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
                Regex regex = new Regex(pattern_pass);
                Regex regex2 = new Regex(pattern_email);
                if (Regex.Match(_createAccountPassword, pattern_pass).Success)//true d�nerse �ifre k�s�tlara uyuyor
                {
                    if(Regex.Match(_createAccountEmail, pattern_email).Success)//email kontrol�
                    {//her �ey do�ru ge�erli g�z�k�yor. Veri taban� i�lemlerini ba�lat
                        _mysqlHelper.CreateAccount(_createAccountUserName, _createAccountEmail, _createAccountPassword, _createAccountPasswordAgain);
                        // MySQL helper scriptinde CreateAccount olu�tur fonksiyonu �a��r�l�yor
                    }
                    else
                    {
                        Debug.LogWarning("Ge�erli bir email adresi girin!");
                    }

                }
                else
                {
                    Debug.LogWarning("�ifre min 6 karakterden olu�mal�, en az 1 harf ve 1 say� i�ermeli!");
                }



            }
            else
            {
                Debug.LogWarning("�ifreler e�le�miyor!!!");
            }

        }
        else
        {
            Debug.LogWarning("T�m alanlar� doldurdu�unuzdan emin olun!!!");
        }
        
    }


    // Giri� yap panelini a�ar veya kapat�r
    public void LoginPanelOpenClose()//Giri� Yap paneli
    {
        if (panels[0].activeSelf)
        {
            panels[0].SetActive(false);//ilk ekran
            panels[1].SetActive(true);
        }
        else if (panels[0].activeSelf == false)//giri� yap panelini a�ar.
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);

        }
    }


    // Kay�t ol panelini a�ar veya kapat�r
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

    /*Start metodu, Update fonksiyonu hen�z hi� �al��t�r�lmam��ken tek seferlik ger�ekle�ir 
     (e�er script enabled ise). Yani Update�ten �nce �al��t�r�l�r.*/

    //awake'in aksine bulunulan h�cre pasifse �al��maz
    void Start()
    {
        panels[0].SetActive(true); // ana paneli a�ar
        panels[1].SetActive(false); // hesap olu�turma panelini kapat�r
    }
    
    // Update is called once per frame
    //Update: Her frame�de tek bir kez �al��t�r�l�r.En s�k kullan�lan Update �e�ididir.
    void Update()
    {
        //bunu nerde yapmak mant�kl� ??
        /*_hesapOlusturKullaniciAdi = hesapOlusturKullaniciAdi.text; // Hesap olu�tur paneli kullan�c� ad� girdisi
        _hesapOlusturSifre = hesapOlusturSifre.text; // Hesap olu�tur paneli �ifre girdisi
        _hesapOlusturSifreAgain = hesapOlusturSifreAgain.text; // Hesap olu�tur paneli e posta girdisi
        _hesapOlusturEPosta = hesapOlusturEPosta.text; // Hesap olu�tur paneli e posta girdisi
        _girisEposta = girisEposta.text; // Giri� paneli kullan� ad� girdisi
        _girisYapSifre = girisYapSifre.text; // Giri� paneli �ifre girdisi*/

    }
}
