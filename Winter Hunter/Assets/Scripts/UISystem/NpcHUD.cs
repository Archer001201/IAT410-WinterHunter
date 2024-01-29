using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class NpcHUD : MonoBehaviour
    {        public Transform trans;
        public Vector3 offset;
        public RectTransform hudPanel;
        public Image fillImage1;
        public Image fillImage2;
        public float lerpSpeed = 5f;
        
        protected float FillPercentage1;
        protected float FillPercentage2;
        
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        protected virtual void Update()
        {
            var worldPosition = trans.position + offset;
            var screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);

            if (screenPosition.z > 0)
            {
                hudPanel.gameObject.SetActive(true);
                hudPanel.position = screenPosition;
            }
            else
            {
                hudPanel.gameObject.SetActive(false);
            }
            
            fillImage1.fillAmount = Mathf.Lerp(fillImage1.fillAmount, FillPercentage1, lerpSpeed * Time.deltaTime);
            fillImage2.fillAmount = Mathf.Lerp(fillImage2.fillAmount, FillPercentage2, lerpSpeed * Time.deltaTime);
        }
    }
}
