using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CityGenerator))]
public class AN_CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CityGenerator cityGenerator = (CityGenerator)target;
        if (GUILayout.Button("Generate City"))
        {
            //delete the previus city
            for(int i = cityGenerator.transform.childCount-1;i>=0;i--)
                DestroyImmediate(cityGenerator.transform.GetChild(i).gameObject);
            cityGenerator.GenerateCity();
        }
    }
}