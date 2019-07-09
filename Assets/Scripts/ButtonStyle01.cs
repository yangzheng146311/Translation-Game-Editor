using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStyle01 : MonoBehaviour
{
    bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnHover()
    {
        this.transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
    }
    public void OnExit()
    {
        if (!isSelected)
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    public void OnSelected()
    {
        this.transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
        GetComponent<UnityEngine.UI.Button>().Select();
        isSelected = true;
    }
    public void OnDeSelected()
    {
        isSelected = false;
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

}
