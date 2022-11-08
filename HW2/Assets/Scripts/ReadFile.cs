using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Scripting.Python;
using UnityEditor;

public class ReadFile : MonoBehaviour
{
    [SerializeField] TextAsset file1;
    [SerializeField] TextAsset file2;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath.ToString());
        PythonRunner.RunFile($"{Application.dataPath}/RigidTransform.py");
        string text1 = file1.text;
        string text2 = file2.text;
        //print(text);
        string[] Lines1 = text1.Split('\n');
        string[] Lines2 = text2.Split('\n');

        List<Vector3> coordinates1 = new List<Vector3>();
        List<Vector3> coordinates2 = new List<Vector3>();
        List<GameObject> points1 = new List<GameObject>();
        List<GameObject> points2 = new List<GameObject>();
        Color32 color1 = new Color32(255, 0, 0, 255);
        Color32 color2 = new Color32(255, 239, 0, 255);
        Parser(Lines1,coordinates1,points1,color1);
        Parser(Lines2, coordinates2, points2,color2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Parser(string[] Lines, List<Vector3> coordinates, List<GameObject> points,Color32 color)
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            string[] temp = Lines[i].Split(' ');
            if (i != 0)
            {
                float x = float.Parse(temp[0], System.Globalization.CultureInfo.InvariantCulture);
                float y = float.Parse(temp[1], System.Globalization.CultureInfo.InvariantCulture);
                float z = float.Parse(temp[2], System.Globalization.CultureInfo.InvariantCulture);
                coordinates.Add(new Vector3(x, y, z));
            }
        }

        foreach (Vector3 element in coordinates)
        {
            Debug.Log(element.ToString());
            points.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            points.Last().transform.position = element;
            points.Last().GetComponent<Renderer>().material.color = color;
        }
    }
}
