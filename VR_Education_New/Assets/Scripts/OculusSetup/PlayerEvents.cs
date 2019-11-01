using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    # region Events
    public static UnityAction OnTouchPadUp = null;
    public static UnityAction OnTouchPadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;

    
    #endregion
    #region Anchors
    public GameObject m_LeftAnchor;
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSets();
    }
    private void OnDestory()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
        
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;

        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;

        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) &&
            !OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;

        //update ...
        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        //if values are the same, return ..
        if (check == previous)
        {
            return previous;
        }
        // get controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        //if no controller, set to the head
        if (controllerObject == null)
        {
            controllerObject = m_HeadAnchor;
        }

        // send out event
        if (OnControllerSource != null)
        {
            OnControllerSource(check, controllerObject);
        }
        //return new value

        return check;
    }

    private void CheckInputSource()
    {
        //left remote ...
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        //{

        //}

        ////right remote ...
        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        //{

        //}

        //if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Touchpad))
        //{

        //}

        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);

    }

    private void Input()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (OnTouchPadDown != null)
            {
                OnTouchPadDown();
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (OnTouchPadUp != null)
            {
                OnTouchPadUp();
            }
        }
    }

    private void PlayerFound()
    {
        m_InputActive = true;
    }

    private void PlayerLost()
    {
        m_InputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {  OVRInput.Controller.LTrackedRemote, m_LeftAnchor },
            {  OVRInput.Controller.RTrackedRemote, m_RightAnchor },
            {  OVRInput.Controller.Touchpad, m_HeadAnchor }
        };
        return newSets;
    }

    private void Update()
    {
        if (!m_InputActive)
        {
            return;
        }
        
        CheckInputSource();
        Input();
    }
}
