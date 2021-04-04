using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class MoveToScreenSpace : MonoBehaviour
    {
        private RectTransform target;

        public Vector3 screenPos;
        // Start is called before the first frame update
        void Start()
        {
            target = GUIManager.Instance.PointsText.rectTransform;
        }

        // Update is called once per frame
        void Update()
        {
            screenPos = Camera.main.WorldToScreenPoint(target.position);
            this.transform.position = (screenPos);
            Debug.Log(Camera.main.WorldToScreenPoint(target.position));
            
        }
    }
}
