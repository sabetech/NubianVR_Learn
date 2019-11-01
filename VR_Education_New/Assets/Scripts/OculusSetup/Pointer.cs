using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OculusSetup
{
    public class Pointer : MonoBehaviour
    {
        public float m_Distance = 40f;
        public LineRenderer m_LineRenderer = null; //remember to get it thru child object ...
        public LayerMask m_EverythingMask = 0;
        public LayerMask m_InteractableMask = 0;
        private GameObject m_PrevObject = null;

        public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

        private Transform m_CurrentOrigin = null;
        private GameObject m_CurrentObject = null;

        private void Awake()
        {
            PlayerEvents.OnControllerSource += UpdateOrigin;
            PlayerEvents.OnTouchPadDown += ProcessTouchpadDown;
            PlayerEvents.OnTouchPadUp += ProcessTouchPadUp;
        }

        private void Start()
        {
            SetLineColor();
        }
        private void Update()
        {
            Vector3 hitPoint = UpdateLine();

            m_CurrentObject = UpdatePointerStatus();

            if (OnPointerUpdate != null)
            {
                ProcessHoverOn();
                OnPointerUpdate(hitPoint, m_CurrentObject);

                if (m_CurrentObject != m_PrevObject)
                {
                    ProcessHoverOff();
                    m_PrevObject = m_CurrentObject;
                }
            }
        }

        private Vector3 UpdateLine()
        {
            //create ray
            RaycastHit hit = CreateRaycast(m_EverythingMask);

            //default end
            Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

            //check hit
            if (hit.collider != null)
            {
                endPosition = hit.point;
            }

            //set position of line renderer
            m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
            m_LineRenderer.SetPosition(1, endPosition);

            return endPosition;
        }

        private void OnDestroy()
        {
            PlayerEvents.OnControllerSource -= UpdateOrigin;
            PlayerEvents.OnTouchPadDown -= ProcessTouchpadDown;
            PlayerEvents.OnTouchPadUp -= ProcessTouchPadUp;
        }

        private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
        {
            //set orogin of pointer
            m_CurrentOrigin = controllerObject.transform;
            
            if (controller == OVRInput.Controller.Touchpad)
            {
                m_LineRenderer.enabled = false;
            }
            else
            {
                m_LineRenderer.enabled = true;
            }
        }

        private GameObject UpdatePointerStatus()
        {
            RaycastHit hit = CreateRaycast(m_InteractableMask);

            if (hit.collider)
                return hit.collider.gameObject;

            return null;
        }

        private void ProcessHoverOn()
        {
            if (!m_CurrentObject)
                return;

            Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
            interactable.Hovered();
        }

        private RaycastHit CreateRaycast(int layer)
        {
            RaycastHit hit;
            Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
            Physics.Raycast(ray, out hit, m_Distance, layer);
            return hit;
        }

        private void SetLineColor()
        {
            if (!m_LineRenderer)
            {
                return;
            }

            Color endColor = Color.white;
            endColor.a = 0.0f;

            m_LineRenderer.endColor = endColor;

        }

        private void ProcessTouchpadDown()
        {
            if (!m_CurrentObject)
                return;

            Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
            interactable.Pressed();
        }

        private void ProcessTouchPadUp()
        {
            {
                if (!m_CurrentObject)
                    return;

                Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
                interactable.Released();
            }
        }

        private void ProcessHoverOff()
        {
            if (!m_PrevObject)
                return;

            Interactable interactable = m_PrevObject.GetComponent<Interactable>();
            interactable.HoveredOff();

        }
    }
}
