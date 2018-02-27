// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class BallKeywords : MonoBehaviour, ISpeechHandler
    {

        private void Awake()
        {
        }

        public void ChangeColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    break;
                case "blue":
                    break;
                case "green":
                    break;
            }
        }

        public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
        {
            ChangeColor(eventData.RecognizedText);
        }

        private void OnDestroy()
        {
        }
    }
}