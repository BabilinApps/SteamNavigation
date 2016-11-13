using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Navigation : MonoBehaviour
{
    public float speed = 1;

    public enum NavigationTypes {
        COLLISION,
        NONCOLLISION
    }
    public NavigationTypes navigationType;
    ControllerModule controllerModule;
    INavigationStrategy navigationStrategy;


   
    void Start()
    {
        controllerModule = GetComponent<ControllerModule>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        switch (navigationType)
        {
            case NavigationTypes.COLLISION:
                navigationStrategy = new CollisionNavigation(rigidbody);
                break;
            case NavigationTypes.NONCOLLISION:
                navigationStrategy = new NonCollisionNavigation(rigidbody);
                break;
            default:
                navigationType = NavigationTypes.COLLISION;
                break;
        }

        controllerModule.OnControllerTriggerDown.AddListener(Navigate);
        controllerModule.OnControllerTriggerUp.AddListener(ActionStopped);

    }


    void ShowGrid(bool on)
    {
        Valve.VR.OpenVR.Chaperone.ForceBoundsVisible(on);
    }

    void Navigate(Transform controller)
    {
        ShowGrid(true);
        var direction = controller.forward;
        navigationStrategy.Navigate(this.transform, direction, speed);
    }

    void ActionStopped(Transform controller)
    {
        navigationStrategy.Stop();
        ShowGrid(false);

    }
}
