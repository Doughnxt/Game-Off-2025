using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractInputTextChanger : MonoBehaviour
{
    [SerializeField] private string interactionText;
    [SerializeField] private string keyToPress;
    private TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = interactionText + "   " + keyToPress;

    }

}
