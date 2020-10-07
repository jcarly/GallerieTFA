using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XInputDotNetPure;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private Transform[] _panels;
    private Transform _currentPanel;
    //private PlayerIndex singlePlayer;
    //private GamePadState controller;
    public CameraAnchor cameraAnchor;
    // Start is called before the first frame update
    void Start()
    {
        //singlePlayer = (PlayerIndex)0;
        int i = 0;
        _panels = new Transform[transform.childCount];
        foreach(Transform child in transform)
        {
            _panels[i] = child;
            i++;
        }
        _currentPanel = _panels[0];
    }

    // Update is called once per frame
    void Update()
    {
        /*controller = GamePad.GetState(singlePlayer);
        if (controller.IsConnected && _panels[0].gameObject.activeSelf)
        {
            _panels[0].gameObject.SetActive(false);
            _panels[1].gameObject.SetActive(true);
            _currentPanel = _panels[1];
            cameraAnchor.hasMoved = true;
        }

        if (!controller.IsConnected && _panels[1].gameObject.activeSelf)
        {
            _panels[1].gameObject.SetActive(false);
            _panels[0].gameObject.SetActive(true);
            _currentPanel = _panels[0];
            cameraAnchor.hasMoved = true;
        }*/

        if (cameraAnchor.hasMoved) {
            int buttonsEnabled = 0;
            if (cameraAnchor.GetCurrentAnchorPoint().gameObject.transform.parent.name == "Cell") { 
                switch (int.Parse(cameraAnchor.GetCurrentAnchorPoint().gameObject.transform.parent.transform.parent.transform.parent.name))
                {
                    case 2:
                    case 3:                   
                    case 11:
                        buttonsEnabled = 1;
                        break;
                    case 4:
                        buttonsEnabled = 2;
                        break;
                    case 6:
                        buttonsEnabled = 3;
                        break;
                    default:
                        break;                    
                }
            
            }
            Button[] buttons = _currentPanel.GetComponentsInChildren<Button>();
            for (int i = 0; i < 3; i++)
            {
                buttons[i].interactable = i < buttonsEnabled;
            }
            cameraAnchor.hasMoved = false;
        }
    }
}
