using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class inputSingleHandle : MonoBehaviour
    {
        #region Attribute Area
        public keyProjection inputDevice;
        private float vertiSignle;
        private float HoriSingle;
        private float velocityVertical;
        private float velocityHorizon;
        public bool fall;
        public float vertiValue = 0;
        public float horiValue = 0;
        public Vector3 targetVector;
        public float targetMagtitue;
        public Transform detectPosition;
        public float detectRadius;
        public LayerMask checkLayer;

        #endregion

        #region Can Be Access Attribute
        #endregion

        private void Start()
        {
        }

        private void Update()
        {
            vertiSignle = (Input.GetKey(inputDevice.UP) ? 1.0f : 0f) - (Input.GetKey(inputDevice.DOWN) ? 1f : 0);
            HoriSingle = (Input.GetKey(inputDevice.RIGHT) ? 1.0f : 0f) - (Input.GetKey(inputDevice.LEFT) ? 1f : 0);

            vertiValue = Mathf.SmoothDamp(vertiValue, vertiSignle, ref velocityVertical, 0.1f);
            horiValue = Mathf.SmoothDamp(horiValue, HoriSingle, ref velocityHorizon, 0.1f);

            Vector2 tmpVec = SquareToCircle(new Vector2(horiValue, vertiValue));

            targetMagtitue = Mathf.Sqrt(Mathf.Pow(tmpVec.y, 2) + Mathf.Pow(tmpVec.x, 2));
            targetVector = tmpVec.x * transform.right + tmpVec.y * transform.forward;

            fall = Physics.CheckSphere(detectPosition.position, detectRadius, checkLayer);

        }

        public Vector2 SquareToCircle(Vector2 _input) {
            Vector2 output = Vector2.zero;

            output.x = _input.x * Mathf.Sqrt(1 - Mathf.Pow(_input.y, 2) / 2f);
            output.y = _input.y * Mathf.Sqrt(1 - Mathf.Pow(_input.x, 2) / 2f);

            return output;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(detectPosition.position, detectRadius);
        }
    }
}
