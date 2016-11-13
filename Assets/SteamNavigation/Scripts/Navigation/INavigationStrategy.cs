using UnityEngine;
using System.Collections;

public interface INavigationStrategy  {
    void Navigate(Transform Player, Vector3 direction, float Speed);

    void Stop();
}
