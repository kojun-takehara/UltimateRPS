using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rotate
{
    [RequireComponent(typeof(Camera))]
    public class RotateGizmo : MonoBehaviour
    {

        public Color xColor = new Color(1, 0, 0, 0.8f);
        public Color yColor = new Color(0, 1, 0, 0.8f);
        public Color zColor = new Color(0, 0, 1, 0.8f);

        public Color allColor = new Color(.7f, .7f, .7f, 0.8f);
		public Color selectedColor = new Color(1, 1, 0, 0.8f);
		public Color hoverColor = new Color(1, .75f, 0, 0.8f);
		public float planesOpacity = .5f;

        
		public float rotationSnap = 15f;
		

		public float handleLength = .25f;
		public float handleWidth = .003f;
		public float planeSize = .035f;
		
		
		public int circleDetail = 40;
		
		public float allRotateHandleLengthMultiplier = 1.4f;
		
		public float minSelectedDistanceCheck = .01f;
		public float moveSpeedMultiplier = 1f;
		
		public float rotateSpeedMultiplier = 1f;
		public float allRotateSpeedMultiplier = 20f;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}