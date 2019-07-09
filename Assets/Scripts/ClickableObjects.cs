using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickableObjects : MonoBehaviour
{
    public GameObject panelScene;
    private Sprite background;
    // Start is called before the first frame update
    void Start()
    {
        background = Resources.Load<Sprite>("Art assets/Background art/" + this.name);
        this.GetComponent<Image>().sprite = background;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackGroundSwitch()
    {
        Debug.Log(this.name);

       
        panelScene.GetComponent<Image>().sprite= background;
    }
}
