using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;

class TxtController
{
    private static TxtController txt = new TxtController();

    public TxtController()
    {
        
    }

    public void updateText()
    {
        string path = "Assets/Resources/TextController.txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(MapStatus.getInstance().getChoosenMap());
        writer.WriteLine(MapStatus.getInstance().getStatus());
        writer.Close();

        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load<TextAsset>("TextController.txt");
    }

    public void readTxt()
    {
        int value = 0, value2 = 0;
        try
        {
            string temp;
            string path = "Assets/Resources/TextController.txt";
            StreamReader theReader = new StreamReader(path, Encoding.Default);

            using (theReader)
            {
                temp = theReader.ReadLine();
                //Debug.Log(temp);
                value = int.Parse(temp);
                //Debug.Log(value);
                MapStatus.getInstance().setChoosenMap(value);
                temp = theReader.ReadLine();
                value2 = int.Parse(temp);
                //Debug.Log(value2);
                MapStatus.getInstance().setAllStatus(value2);
                //Debug.Log("MS : " + MapStatus.getInstance().getStatus());
                theReader.Close();
            }
        }catch (Exception e){
            Console.WriteLine("No Txt");
            value = 0;
            MapStatus.getInstance().setChoosenMap(value);
            value2 = 0;
            MapStatus.getInstance().setAllStatus(value2);
        }



        //string temp;
        //int value=0,value2=0;
        //string path = "Assets/Resources/TextController.txt";
        //StreamReader theReader = new StreamReader(path, Encoding.Default);
        ////StreamReader theReader = new StreamReader(path);

        //using (theReader)
        //{
        //    temp = theReader.ReadLine();
        //    //Debug.Log(temp);
        //    value = int.Parse(temp);
        //    //Debug.Log(value);
        //    MapStatus.getInstance().setChoosenMap(value);
        //    temp = theReader.ReadLine();
        //    value2 = int.Parse(temp);
        //    //Debug.Log(value2);
        //    MapStatus.getInstance().setAllStatus(value2);
        //    //Debug.Log("MS : " + MapStatus.getInstance().getStatus());
        //    theReader.Close();
        //}
    }


    public static TxtController getInstance() {
        return txt;
    }

}
