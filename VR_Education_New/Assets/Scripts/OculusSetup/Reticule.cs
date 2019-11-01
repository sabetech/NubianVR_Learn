using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OculusSetup
{
    public class Reticule : MonoBehaviour
    {

        public Pointer m_Pointer;
        public SpriteRenderer m_CirculeRenderer;

        public Sprite m_OpenSprite;
        public Sprite m_ClosedSprite;

        private Camera m_Camera = null;

        private void Awake()
        {
            m_Pointer.OnPointerUpdate += UpdateSprite;
            m_Camera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(m_Camera.gameObject.transform);
        }

        private void UpdateSprite(Vector3 point, GameObject hitObject)
        {
            transform.position = point;

            if (hitObject)
            {
                m_CirculeRenderer.sprite = m_ClosedSprite;
            }
            else
            {
                m_CirculeRenderer.sprite = m_OpenSprite;
            }
        }

        private void OnDestroy()
        {
            m_Pointer.OnPointerUpdate -= UpdateSprite;
        }
    }

}
