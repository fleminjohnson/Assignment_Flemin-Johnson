using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCSV : MonoBehaviour
{
    public bool endOfFile = false;
    bool firstTime = true;
    private List<string> foods;
    public static List<string> colour;
    public static List<string> point;

    void Awake()
    {
        foods = new List<string>();
        point = new List<string>();
        colour = new List<string>();

        LoadCSVFile();
    }

    void LoadCSVFile()
    {
        StreamReader strReader = new StreamReader("Assets/CSVData/Food.csv");
        while (!endOfFile)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }
            if (firstTime)
            {
                firstTime = false;
            }
            else
            {
                var dataValues = dataString.Split(',');
                colour.Add(dataValues[0]);
                point.Add(dataValues[1]);
            }
        }

    }
}
