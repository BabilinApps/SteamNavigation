using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class ControllerTriggerDownEvent : UnityEvent<Transform> { }
public class ControllerModule : MonoBehaviour {
    [SerializeField]
    SteamVR_TrackedObject controllerR;
    [SerializeField]
    SteamVR_TrackedObject controllerL;

    KeyValuePair<SteamVR_Controller.Device, Transform> deviceR {
        get {
            if (controllerR.isActiveAndEnabled)
            {
                var input = SteamVR_Controller.Input((int)controllerR.index);
                return new KeyValuePair<SteamVR_Controller.Device, Transform>(input, controllerR.transform);
            }
            else
                return new KeyValuePair<SteamVR_Controller.Device, Transform>(null,null);
        } }
    KeyValuePair<SteamVR_Controller.Device, Transform> deviceL
    {
        get
        {
            if (controllerL.isActiveAndEnabled)
            {
                var input = SteamVR_Controller.Input((int)controllerL.index);
                return new KeyValuePair<SteamVR_Controller.Device, Transform>(input, controllerL.transform);
            }
            else
                return new KeyValuePair<SteamVR_Controller.Device, Transform>(null, null);
        }
    }

   public ControllerTriggerDownEvent OnControllerTriggerDown = new ControllerTriggerDownEvent();
   public ControllerTriggerDownEvent OnControllerTriggerUp = new ControllerTriggerDownEvent();
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(deviceR.Key != null)
        {
            TryInteract(deviceR);
        }

        if (deviceL.Key != null)
        {
            TryInteract(deviceL);
        }
    }

    void TryInteract(KeyValuePair<SteamVR_Controller.Device, Transform> device) {
        if (device.Key.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (OnControllerTriggerDown != null)
                OnControllerTriggerDown.Invoke(device.Value);
        }

        if (device.Key.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)&& OnControllerTriggerUp != null)
            OnControllerTriggerUp.Invoke(device.Value);
    }
}
