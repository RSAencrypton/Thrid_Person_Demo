using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class newPlayerController : MonoBehaviour
    {
        public keyProjection inputDevice;
        inputSingleHandle input;
        // Start is called before the first frame update
        void Start()
        {
            input = new inputSingleHandle(inputDevice);
        }

        // Update is called once per frame
        void Update()
        {
            input.handleSingle();

            Debug.Log("vertical : " + input.vertiValue);
            Debug.Log("horizontal : " + input.horiValue);
        }
    }

    public class inputSingleHandle{

        private keyProjection inputDevice;
        private float vertiSignle;
        private float HoriSingle;
        private float velocityVertical;
        private float velocityHorizon;
        public float vertiValue;
        public float horiValue;

        public inputSingleHandle(keyProjection _inputDevice) {
            inputDevice = _inputDevice;
        }

        public void handleSingle()
        {
            vertiSignle = (Input.GetKeyDown(inputDevice.UP) ? 1.0f : 0f) - (Input.GetKeyDown(inputDevice.DOWN) ? 1f : 0);
            HoriSingle = (Input.GetKeyDown(inputDevice.LEFT) ? 1.0f : 0f) - (Input.GetKeyDown(inputDevice.RIGHT) ? 1f : 0);

            vertiValue = Mathf.SmoothDamp(vertiValue, vertiSignle, ref velocityVertical, 0.1f);
            horiValue = Mathf.SmoothDamp(horiValue, HoriSingle, ref velocityHorizon, 0.1f);
        }
    }
}
