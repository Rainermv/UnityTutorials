using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

    public UnityEngine.UI.Text highestFPSLabel;
    public UnityEngine.UI.Text averageFPSLabel;
    public UnityEngine.UI.Text lowestFPSLabel;

    [SerializeField]
    private FPSColor[] coloring;

    private FPSCounter fpsCounter;

    [System.Serializable]
    private struct FPSColor {
        public Color color;
        public int minimumFPS;
    }

    static string[] numberStrings;

    void Awake() {
        fpsCounter = GetComponent<FPSCounter>();

        numberStrings = new string[100];

        for (int i = 0; i < 100; i++) {

            numberStrings[i] = i.ToString();
        }
    }

    void Update() {

        Display(highestFPSLabel, fpsCounter.HighestFPS);
        Display(averageFPSLabel, fpsCounter.AverageFPS);
        Display(lowestFPSLabel, fpsCounter.LowestFPS);
    }

    private void Display(Text label, int value) {

        label.text = numberStrings[Mathf.Clamp(value, 0, 99)];

        for (int i = 0; i < coloring.Length; i++) {

            if (value >= coloring[i].minimumFPS) {
                label.color = coloring[i].color;
                break;
            }

        }
    }
}
