using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public enum States { Load, Start, End }

    public UnityEvent loadEvent;
    public UnityEvent startEvent;
    public UnityEvent endEvent;

    
}
