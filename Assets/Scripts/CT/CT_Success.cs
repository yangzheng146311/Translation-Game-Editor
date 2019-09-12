using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CT_Success : MonoBehaviour
{

    public GameObject successTextShow;
    public GameObject successTranslationInput;

    public GameObject errorPanel;

    static public string successSource;
    static public string successTrans;
    // Start is called before the first frame update
    void Start()
    {
        successSource = successTextShow.GetComponent<Text>().text;
        Debug.Log(successSource);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SubmitSuccessTrans()
    {

        if (successTranslationInput.GetComponent<InputField>().text.Trim()=="")
        {
            errorPanel.SetActive(true);
            return;
            
        }

        successTrans = successTranslationInput.GetComponent<InputField>().text.Trim();
        Debug.Log(successTrans);

        ExportText();

        Application.Quit();
    }

    public void ExportText()
    {

        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/" + "CT_GAME" + "_Translation.csv";
        Debug.Log(fileName);

        //string fileName = Application.persistentDataPath + "/GameData/" + loadGameName + "_Translation.csv";
        File.WriteAllText(@fileName, string.Empty);

        File.AppendAllText(@fileName, "Traditional Chinese" + ',' + "Translation Text"+ Environment.NewLine, System.Text.Encoding.UTF8);
        try
        {
            File.AppendAllText(@fileName, CT_S1.sourceRollingText.Replace(Environment.NewLine," ") + ',' + CT_S1.transRollingText.Replace(",","/").Replace(Environment.NewLine, " ") + Environment.NewLine, System.Text.Encoding.UTF8);
            File.AppendAllText(@fileName, CT_S1.sourceStartText.Replace(Environment.NewLine, "/") + ',' + CT_S1.transStartText.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);
            File.AppendAllText(@fileName, CT_S1.sourceCourageText.Replace(Environment.NewLine, "/") + ',' + CT_S1.transCourageText.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);

            File.AppendAllText(@fileName, CT_S2.sourcePlayWay.Replace(Environment.NewLine, "/") + ',' + CT_S2.transPlayWay.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);
            File.AppendAllText(@fileName, CT_S2.sourceTips.Replace(Environment.NewLine, "/") + ',' + CT_S2.transTips.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);
            File.AppendAllText(@fileName, CT_S2.sourceEnd.Replace(Environment.NewLine, "/") + ',' + CT_S2.transEnd.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);
            File.AppendAllText(@fileName, CT_S2.sourcePlayWayTxt + ',' + CT_S2.transPlayWayTxt.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);
            File.AppendAllText(@fileName, CT_S2.sourceTipsTxt.Replace(Environment.NewLine, "/") + ',' + CT_S2.transTipsTxt.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);

            
            File.AppendAllText(@fileName,( CT_Success.successSource).Replace(Environment.NewLine, "/") + ',' + CT_Success.successTrans.Replace(",", "/") + Environment.NewLine, System.Text.Encoding.UTF8);

            File.AppendAllText(@fileName, CT_Fail.failSource + ',' + CT_Fail.failTrans.Replace(",", "/"), System.Text.Encoding.UTF8);
        }

        catch (Exception err)
        {
            Debug.Log(err);
        }


        //try
        //{
        //    using (System.IO.StreamWriter file = new StreamWriter(@fileName, true))
        //    {
        //        file.WriteLine("Traditional Chinese" + ',' + "Translation Text" ); ;



        //        file.WriteLine( CT_S1.sourceRollingText + ',' +CT_S1.transRollingText) ;
        //        file.WriteLine(CT_S1.sourceStartText + ',' + CT_S1.transStartText);
        //        file.WriteLine(CT_S1.sourceCourageText+ ',' + CT_S1.transCourageText);

        //        file.WriteLine(CT_S2.sourcePlayWay + ',' + CT_S2.transPlayWay);
        //        file.WriteLine(CT_S2.sourceTips + ',' + CT_S2.transTips);
        //        file.WriteLine(CT_S2.sourceEnd + ',' + CT_S2.transEnd);
        //        file.WriteLine(CT_S2.sourcePlayWayTxt + ',' + CT_S2.transPlayWayTxt);
        //        file.WriteLine(CT_S2.sourceTipsTxt + ',' + CT_S2.transTipsTxt);

        //        file.WriteLine(CT_Success.successSource + ',' + CT_Success.successTrans);

        //        file.WriteLine(CT_Fail.failSource + ',' + CT_Fail.failTrans);


        //    }
        //}
        //catch (Exception ex)
        //{

        //    throw new ApplicationException("error:", ex);

        //}

        Debug.Log("Export Finished");

        System.Diagnostics.Process.Start(fileName);




    }


}
