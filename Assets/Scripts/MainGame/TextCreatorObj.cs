using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCreatorObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteCreator()
    {

        if (gameObject.transform.parent.gameObject.name == "TextCreator_1") return;

        GameObject content = gameObject.transform.parent.parent.parent.gameObject;
        RectTransform rt = content.GetComponent<RectTransform>();
        float height = rt.rect.height;
        rt.sizeDelta = new Vector2(0, height - 200);
        Destroy(gameObject.transform.parent.gameObject);

    }
}
