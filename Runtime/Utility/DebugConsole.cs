using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VanceUtility.Util {
    public class DebugConsole : MonoBehaviour
    {
        private TextMeshProUGUI outputText;
        private TextMeshProUGUI fpsText;
        private List<string> logs = new List<string>();
        private Dictionary<string, object> variableValues = new Dictionary<string, object>();

        void Awake()
        {
            SetupUI();
            StartCoroutine(ShowFPS());
        }

        void Update()
        {
            foreach (var key in variableValues.Keys)
            {
                logs.Add($"{key}: {variableValues[key]}");
            }

            outputText.text = string.Join("\n", logs);
            logs.Clear();
        }

        public void Log(string message)
        {
            logs.Add(message);
        }

        public void WatchVariable(string name, object value)
        {
            variableValues[name] = value;
        }

        private IEnumerator ShowFPS()
        {
            while (true)
            {
                float fps = 1.0f / Time.deltaTime;
                fpsText.text = $"FPS: {Mathf.Ceil(fps)}";
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void SetupUI()
        {
            // Create Canvas
            GameObject canvasObj = new GameObject("DebugConsoleCanvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasObj.AddComponent<GraphicRaycaster>();

            // Create Output Text
            GameObject outputTextObj = new GameObject("OutputText");
            outputTextObj.transform.SetParent(canvasObj.transform);
            outputText = outputTextObj.AddComponent<TextMeshProUGUI>();
            outputText.fontSize = 16;
            outputText.alignment = TextAlignmentOptions.TopLeft;
            outputText.rectTransform.anchorMin = new Vector2(0, 1);
            outputText.rectTransform.anchorMax = new Vector2(0, 1);
            outputText.rectTransform.pivot = new Vector2(0, 1);
            outputText.rectTransform.anchoredPosition = new Vector2(10, -10);
            outputText.rectTransform.sizeDelta = new Vector2(500, 300);

            // Create FPS Text
            GameObject fpsTextObj = new GameObject("FPSText");
            fpsTextObj.transform.SetParent(canvasObj.transform);
            fpsText = fpsTextObj.AddComponent<TextMeshProUGUI>();
            fpsText.fontSize = 16;
            fpsText.alignment = TextAlignmentOptions.TopRight;
            fpsText.rectTransform.anchorMin = new Vector2(1, 1);
            fpsText.rectTransform.anchorMax = new Vector2(1, 1);
            fpsText.rectTransform.pivot = new Vector2(1, 1);
            fpsText.rectTransform.anchoredPosition = new Vector2(-10, -10);
            fpsText.rectTransform.sizeDelta = new Vector2(100, 30);
        }
    }
}