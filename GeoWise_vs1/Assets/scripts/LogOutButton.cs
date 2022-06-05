using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOutButton : MonoBehaviour
{
    //public MySQLHelper mySQLHelper;
    public void LogOut()
    {
        /*int SceneId = 0;
        mySQLHelper.NewPage(SceneId);//giriş sayfasına dönüş için sahne id:0*/

        PlayerPrefs.SetInt("adminLog", 0);
        PlayerPrefs.SetInt("userLog", 0);

        SceneManager.LoadScene("Scene2"); // Sahneyi yeniden yüklüyorum
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
