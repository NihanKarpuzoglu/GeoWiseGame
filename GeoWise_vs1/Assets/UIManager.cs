using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class UIManager : MonoBehaviour
{
    public Text text_forget;
    public GameObject toggler;//sifremi unuttum
    //public GameObject userLogin;//kullanıcı girişi butonu
    private GameObject login;
    public GameObject[] panels; //panellerin içinde tutulduğu dizi
    public MySQLHelper _mysqlHelper; //mysqlhelper fonksiyonlarını kullanmak için

    public InputField createAccountUserName; // Hesap oluştur paneli kullanıcı adı girdisi
    public InputField createAccountPassword; // Hesap oluştur paneli şifre girdisi
    public InputField createAccountPasswordAgain; // Hesap oluştur paneli tekrar şifre
    public InputField createAccountEmail; // Hesap oluştur paneli e posta girdisi

    public InputField loginEmail; // Giriş paneli kullanı adı girdisi
    public InputField loginPassword; // Giriş paneli şifre girdisi

    

    //admin bilgileri
    public InputField adminMail;
    public InputField adminPassword;

    private string _createAccountUserName; // Hesap oluştur paneli kullanıcı adı girdisi
    private string _createAccountPassword; // Hesap oluştur paneli şifre girdisi
    private string _createAccountPasswordAgain; // Hesap oluştur paneli şifre tekrar
    private string _createAccountEmail; // Hesap oluştur paneli e posta girdisi

    private string _loginEmail; // Giriş paneli kullanı adı girdisi
    private string _loginPassword; // Giriş paneli şifre girdisi

    private string _adminMail;
    private string _adminPassword;


    // Panellerin sıralanışı
    // 0 AnaEkran:Giriş Paneli Burda. Burdan yeni hesap oluştur butonuyla diğer panele geçiliyor
    // 1 Hesap Oluşturma Paneli


    /*Bu fonksiyon daima Start fonksiyonundan önce çalıştırılır, ayrıca bir 
    prefab Instantiate edildiği anda da çalıştırılır. 
    (Eğer GameObject aktif (active) değilse bu fonksiyon 
    obje aktif olana kadar ya da bu objedeki bir scriptte yer alan 
    bir fonksiyon dışarıdan çağrılana kadar çalıştırılmaz.)
    obje pasif olsa bile çalışır. Her halükarda çalışır*/
    void Awake()
    {
        if (PlayerPrefs.GetInt("UserLog") == 1)//doğruysa giriş zaten yapılmış
        { // Kullanıcı giriş yapmış mı kontrol ediyorum
            LoginScreenClose(); // Giriş yapmışsa panelleri kapat,oyuncu panelini aç
        }
    }

    // Giriş ve hesap oluştur panelini kapatmak için
    public void LoginScreenClose()
    {
        panels[0].SetActive(false); // ana paneli kapatır
        panels[1].SetActive(false); // hesap oluşturma panelini kapatır
        //paneller[2].SetActive(false); // hesap oluşturma panelini kapatır

    }
    // Oyuncu Girişi butonuna tıklandığı zaman çalışan fonksiyon
    public void GamerLogin()//oyuncu girişi butonuna bağlı
    {
        if (loginPassword.enabled == false)//şifre yenileme
        {
            _loginEmail = loginEmail.text;
            if (loginEmail.text != "")
            {
                //önce email adresi valid mi kontrol et
                string pattern_email = @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
                Regex regex2 = new Regex(pattern_email);

                if (Regex.Match(_loginEmail, pattern_email).Success)//email kontrolü
                {//her şey doğru geçerli gözüküyor. Veri tabanında hesabı arat ve kullanıcıya mail at
                    _mysqlHelper.EmailExist(_loginEmail);
                } 
                else
                {
                    Debug.LogWarning("Uygun bir email adresi girin!!!");
                }
            }
            else
            {
                Debug.LogWarning("Tüm alanları doldurduğunuzdan emin olun!!!");
            }
        }
        else//oyuncu girişi
        {
            if (loginEmail.text != "" && loginPassword.text != "")
            {
                //??
                _loginEmail = loginEmail.text; // Giriş paneli kullanı adı girdisi
                _loginPassword = loginPassword.text; // Giriş paneli şifre girdisi

                _mysqlHelper.GamerLogin(_loginEmail, _loginPassword); // MYSQLHelper scriptindeki girişyap fonksiyonunu çağırıyor ve gerekli bilgileri gönderiyor
            }
            else
            {
                Debug.LogWarning("Tüm alanları doldurduğunuzdan emin olun!!!");
            }
        }
    }



    // Oyuncu Girişi butonuna tıklandığı zaman çalışan fonksiyon
    public void AdminLogin()//yönetici girişi butonuna bağlı
    {
        if (adminMail.text != "" && adminPassword.text != "")
        {

            //??Private değişkenlere atama
            _adminMail = adminMail.text;
            _adminPassword = adminPassword.text;

            _mysqlHelper.AdminLogin(_adminMail, _adminPassword);
            // MYSQLHelper scriptindeki admin girişi fonksiyonu çağırılıyor ve gerekli bilgileri gönderiliyor
        }
        else
        {
            Debug.LogWarning("Tüm alanları doldurduğunuzdan emin olun!!!");
        }
    }

    // Hesap oluştur butonuna tıklandığı zaman çalışan fonksiyon
    public void CreateAccount()
    {
        if (createAccountUserName.text != "" && createAccountEmail.text != "" && createAccountPassword.text != "" && createAccountPasswordAgain.text != "")
        {
            if (createAccountPassword.text == createAccountPasswordAgain.text)//şifreler eşleşiyor mu?
            {
                //??
                _createAccountUserName = createAccountUserName.text; // Hesap oluştur paneli kullanıcı adı girdisi
                _createAccountEmail = createAccountEmail.text; // Hesap oluştur paneli e posta girdisi
                _createAccountPassword = createAccountPassword.text; // Hesap oluştur paneli şifre girdisi
                _createAccountPasswordAgain = createAccountPasswordAgain.text; // Hesap oluştur paneli şifre girdisi tekrar

                //parola regex denetleme//parola min 6 karakterden oluşmalı. İçinde en az 1 harf ve 1 sayı içermeli.
                /*// Minimum eight characters, at least one letter and one number:
                 * "^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"*/

                string pattern_pass = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
                string pattern_email = @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
                Regex regex = new Regex(pattern_pass);
                Regex regex2 = new Regex(pattern_email);
                if (Regex.Match(_createAccountPassword, pattern_pass).Success)//true dönerse şifre kısıtlara uyuyor
                {
                    if (Regex.Match(_createAccountEmail, pattern_email).Success)//email kontrolü
                    {//her şey doğru geçerli gözüküyor. Veri tabanı işlemlerini başlat
                        _mysqlHelper.CreateAccount(_createAccountUserName, _createAccountEmail, _createAccountPassword, _createAccountPasswordAgain);
                        // MySQL helper scriptinde CreateAccount oluştur fonksiyonu çağırılıyor
                    }
                    else
                    {
                        Debug.LogWarning("Geçerli bir email adresi girin!");
                    }

                }
                else
                {
                    Debug.LogWarning("Şifre min 6 karakterden oluşmalı, en az 1 harf ve 1 sayı içermeli!");
                }



            }
            else
            {
                Debug.LogWarning("Şifreler eşleşmiyor!!!");
            }

        }
        else
        {
            Debug.LogWarning("Tüm alanları doldurduğunuzdan emin olun!!!");
        }

    }


    // Giriş yap panelini açar veya kapatır
    public void LoginPanelOpenClose()//Giriş Yap paneli
    {
        if (panels[0].activeSelf)
        {
            panels[0].SetActive(false);//ilk ekran
            panels[1].SetActive(true);
        }
        else if (panels[0].activeSelf == false)//giriş yap panelini açar.
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);

        }
    }


    // Kayıt ol panelini açar veya kapatır
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

    /*Start metodu, Update fonksiyonu henüz hiç çalıştırılmamışken tek seferlik gerçekleşir 
     (eğer script enabled ise). Yani Update’ten önce çalıştırılır.*/

    //awake'in aksine bulunulan hücre pasifse çalışmaz
    void Start()
    {
        panels[0].SetActive(true); // ana paneli açar
        panels[1].SetActive(false); // hesap oluşturma panelini kapatır
    }

    // Update is called once per frame
    //Update: Her frame’de tek bir kez çalıştırılır.En sık kullanılan Update çeşididir.
    void Update()
    {
        //bunu nerde yapmak mantıklı ??
        /*_hesapOlusturKullaniciAdi = hesapOlusturKullaniciAdi.text; // Hesap oluştur paneli kullanıcı adı girdisi
        _hesapOlusturSifre = hesapOlusturSifre.text; // Hesap oluştur paneli şifre girdisi
        _hesapOlusturSifreAgain = hesapOlusturSifreAgain.text; // Hesap oluştur paneli e posta girdisi
        _hesapOlusturEPosta = hesapOlusturEPosta.text; // Hesap oluştur paneli e posta girdisi
        _girisEposta = girisEposta.text; // Giriş paneli kullanı adı girdisi
        _girisYapSifre = girisYapSifre.text; // Giriş paneli şifre girdisi*/


        if (toggler.GetComponent<Toggle>().isOn)//tıklandıysa loginpassword disable olmalı
                                                //oyuncu girişi yazısı şifremi unuttuma dönmeli
        {
            loginPassword.enabled = false;
            //text_forget.text = "Şifremi Unuttum";

        }
        else
        {
            loginPassword.enabled = true;
            //text_forget.text = "Oyuncu Girişi";
        }

    }


}
