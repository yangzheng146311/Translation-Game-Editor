using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageImage : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject newSceneBtn;

    private void Awake()
    {
        newSceneBtn = GameObject.Find("New Scene");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }




    public void SwitchToPage()
    {

        string showPageName = this.gameObject.name;


        EditManager.GetEditManager().ShowPage(showPageName);

    }



    public void OnImageClickEvent()
    {


        if (Input.GetMouseButtonUp(0))
        {
           
            SwitchToPage();
        }


        if (Input.GetMouseButtonUp(1))
        {
            int pageCount = EditManager.GetEditManager().pageList.Count;
            if (pageCount > 1)
            {

                string imageName = this.name;
                EditManager.GetEditManager().DestroyPageImage(this.name);
                
            }

        }

        GameObject.Find("PageImageList").SetActive(false);
        newSceneBtn.SetActive(true);
    }



}
