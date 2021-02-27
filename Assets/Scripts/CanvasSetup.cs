/**
* Copyright (c) 2020 Vuplex Inc. All rights reserved.
*
* Licensed under the Vuplex Commercial Software Library License, you may
* not use this file except in compliance with the License. You may obtain
* a copy of the License at
*
*     https://vuplex.com/commercial-library-license
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using UnityEngine;

namespace Vuplex.WebView.Demos {

    /// <summary>
    /// Sets up the CanvasWebViewDemo scene, which displays a `CanvasWebViewPrefab`
    /// in screen space inside a canvas.
    /// </summary>
    /// <remarks>
    /// This scene includes Unity's standalone input module, so
    /// you can click and scroll the webview using your touchscreen
    /// or mouse.
    ///
    /// You can also move the camera by holding down the control key on your
    /// keyboard and moving your mouse. When running on a device
    /// with a gyroscope, the gyroscope controls the camera rotation instead.
    ///
    /// `WebViewPrefab` handles standard Unity input events, so it works with
    /// a variety of third party input modules that extend Unity's `BaseInputModule`,
    /// like the input modules from the Google VR and Oculus VR SDKs.
    ///
    /// Here are some other examples that show how to use 3D WebView with popular SDKs:
    /// • Google VR (Cardboard and Daydream): https://github.com/vuplex/google-vr-webview-example
    /// • Oculus (Oculus Quest, Go, and Gear VR): https://github.com/vuplex/oculus-webview-example
    /// • AR Foundation : https://github.com/vuplex/ar-foundation-webview-example
    /// </remarks>
    class CanvasSetup : MonoBehaviour {


        CanvasWebViewPrefab _canvasWebViewPrefab;
        HardwareKeyboardListener _hardwareKeyboardListener;

        void Start() {
            Debug.Log("Keyboard spawner");
            // The CanvasWebViewPrefab's `InitialUrl` property is set via the editor, so it
            // will automatically initialize itself with that URL.
            _canvasWebViewPrefab = GameObject.Find("CanvasWebViewPrefab").GetComponent<CanvasWebViewPrefab>();

            _hardwareKeyboardListener = HardwareKeyboardListener.Instantiate();
            _hardwareKeyboardListener.InputReceived += (sender, eventArgs) => {
                // Include key modifiers if the webview supports them.
                var webViewWithKeyModifiers =   _canvasWebViewPrefab.WebView as IWithKeyModifiers;
                if (webViewWithKeyModifiers == null) {
                    _canvasWebViewPrefab.WebView.HandleKeyboardInput(eventArgs.Value);
                } else {
                    webViewWithKeyModifiers.HandleKeyboardInput(eventArgs.Value, eventArgs.Modifiers);
                }
            };

            // Also add an on-screen keyboard under the main webview.
            var keyboard = MyKeyboard.Instantiate();
            keyboard.transform.parent = _canvasWebViewPrefab.transform;
            keyboard.transform.position = new Vector3(0f,-0.6f,0) + _canvasWebViewPrefab.transform.position;
            keyboard.transform.localScale = new Vector3(1300,1300,1);
            keyboard.transform.localEulerAngles = new Vector3(0,180,0);
            keyboard.InputReceived += (sender, eventArgs) => {
                _canvasWebViewPrefab.WebView.HandleKeyboardInput(eventArgs.Value);
            };
            // _canvasWebViewPrefab.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("UI");
            // _canvasWebViewPrefab.transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("UI");

            // keyboard.gameObject.layer = LayerMask.NameToLayer("UI");
            // keyboard.gameObject.AddComponent(typeof(XRSimpleInteractable));
            SetLayerRecursively(_canvasWebViewPrefab.gameObject);

        }
        public void SetLayerRecursively(GameObject obj)
        {

            if (null == obj)
            {
                return;
            }
        
            obj.layer = LayerMask.NameToLayer("UI");;
        
            foreach (Transform child in obj.transform)
            {
                if (null == child)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject);
            }
        }
    }
}
