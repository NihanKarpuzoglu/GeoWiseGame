using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminUIManager : MonoBehaviour
{

    public GameObject[] panels;//panellerin dizisi 0-> admin genel,1->gamer panel,2->question panel






    // Start is called before the first frame update
    void Start()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
    }

    //Update is called once per frame
    void Update()
    {

    }

    public void GamerPanelOpenClose()//Gamer Paneli Açma Kapatma
    {
        
        if (panels[1].activeSelf)
        {
            panels[0].SetActive(true);//ilk ekran
            panels[1].SetActive(false);
            panels[2].SetActive(false);
        }
        else if (panels[1].activeSelf == false)//Gamel Paneli açar
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            panels[2].SetActive(false);

        }
    }


    public void QuestionPanelOpenClose()//Question Paneli Açma Kapatma
    {
        if (panels[2].activeSelf)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
        }
        else if (panels[2].activeSelf == false) //Question Paneli açar
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(true);
        }
    }

    public void Back()//Admin Panele Dön
    {
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
    }



}
