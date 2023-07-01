using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterExamples : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Examples = new();

    [SerializeField]
    private TMP_Text exampleValue;

    private int example = 0;
    private bool start = false;


    public void ResetPosition() {
        SetExample(0);

        foreach (GameObject e in Examples) {
            e.SetActive(false);
        }

        start = false;
    }
    public void Trigger() { 
        if (example <= 0) {
            return;
        }
        
        start = true;

        GameObject currentExample = Examples[example-1];
        currentExample.SetActive(true);
    }
    
    public void SetExample(int value) {
        example = value;
        String text = example.ToString();
        switch(example) {
            case 1:
                text += " - creating a spring movement";
            break;
            case 2:
                text += " - damping movement";
            break;
            case 3:
                text += " - spreading movement between springs on impact";
            break;
            case 4:
                text += " - creating a shape with sprite shape";
            break;
            case 5:
                text += " - adding points dynamically to the sprite shape";
            break;
            case 6:
                text += " - moving the sprite shape waves with the springs";
            break;
            case 7:
                text += " - smooth waves";
            break;
            case 8:
                text += " - update wave count";
            break;
            case 9:
                text += " - create wave on impact";
            break;
            case 10:
                text += " - Add buoyancy 2d";
            break;
        }
        exampleValue.text = text;
    }

    void Update() { 
        if (!start) {
            return;
        }
    }

    void FixedUpdate() { 
        if (!start) {
            return;
        }
    }


}
