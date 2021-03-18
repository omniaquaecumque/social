using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignHidden : MonoBehaviour
{
    private ArrayList Hides = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            Hides.Add(child);
            // Debug.Log(child.gameObject.name);
        }
        int select = Random.Range(0, Hides.Count - 1);
        GameObject choosen = ((Transform)Hides[select]).gameObject;
        choosen.tag = "Hidden";
        choosen.AddComponent<hiddenBeh>();
        Debug.Log(choosen.name + "is hidden.");
    }
}
