using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LengthText : MonoBehaviour
{
    /* Private Fields */
    private TextMeshProUGUI _textMeshProUGUI = null;
    private int _currentLength = 0;
    
    void Start()
    {
        GetComponents();
    }

    /* Update the Length Text */
    public void UpdateDisplay()
    {
        _currentLength++;
        _textMeshProUGUI.text = "Length: " + _currentLength;
    }

    /* Get any other necessary components attached to this gameobject */
    private void GetComponents()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
