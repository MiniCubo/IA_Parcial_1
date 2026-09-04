using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonActors : MonoBehaviour
{
    private static SingletonActors instance;
    public static SingletonActors Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<SingletonActors>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonActors");
                    instance = singletonObject.AddComponent<SingletonActors>();
                }
            }
            return instance;
        }
    }

    private List<U_ThiefAI> thieves;
    private List<U_PoliceAI> polices;
    private List<U_PedestrianAI> pedestrians;
    private U_Player player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        thieves = new List<U_ThiefAI>(
            GameObject.FindObjectsByType<U_ThiefAI>()
        );

        polices = new List<U_PoliceAI>(
            GameObject.FindObjectsByType<U_PoliceAI>()
        );

        pedestrians = new List<U_PedestrianAI>(
            GameObject.FindObjectsByType<U_PedestrianAI>()
        );

        player = GameObject.FindAnyObjectByType<U_Player>();
    }

    public T GetClosestActor<T>(IDescription currentActor) where T : MonoBehaviour, IDescription
    {
        IEnumerable<T> actors;
        if (typeof(T) == typeof(U_ThiefAI))
        {
            actors = thieves as IEnumerable<T>;
        }
        else if (typeof(T) == typeof(U_PoliceAI))
        {
            actors = polices as IEnumerable<T>;
        }
        else if (typeof(T) == typeof(U_PedestrianAI))
        {
            actors = pedestrians as IEnumerable<T>;
        }
        else if (typeof(T) == typeof(U_Player))
        {
            return player as T;
        }
        else
        {
            throw new ArgumentException($"Unsupported actor type: {typeof(T).Name}");
        }


        T closest = default(T);
        float closestDistance = Mathf.Infinity;

        foreach (T actor in actors)
        {
            if (actor == null)
            {
                continue;
            }
            float distanceToCurrentActor = Vector3.Distance(currentActor.Transform.position, actor.transform.position);

            if (distanceToCurrentActor < closestDistance)
            {
                closestDistance = distanceToCurrentActor;
                closest = actor;
            }
        }

        return closest;
    }

    public void DestroyActor<T>(T actor) where T : MonoBehaviour, IDescription
    {
        if (typeof(T) == typeof(U_ThiefAI))
        {
            U_ThiefAI t = thieves.Find(obj => obj == actor as U_ThiefAI);
            if (t != null)
            {
                Destroy(t);
                thieves.Remove(t);
            }
        }
        else if (typeof(T) == typeof(U_PoliceAI))
        {
            U_PoliceAI t = polices.Find(obj => obj == actor as U_PoliceAI);
            if (t != null)
            {
                Destroy(t);
                polices.Remove(t);
            }
        }
        else if (typeof(T) == typeof(U_PedestrianAI))
        {
            U_PedestrianAI t = pedestrians.Find(obj => obj == actor as U_PedestrianAI);
            if (t != null)
            {
                Destroy(t);
                pedestrians.Remove(t);
            }
        }
        else if (typeof(T) == typeof(U_Player))
        {
            throw new Exception($"Can't Destroy the Player");
        }
        else
        {
            throw new ArgumentException($"Unsupported actor type: {typeof(T).Name}");
        }
    }

    public T TraceActors<T>(IDescription currentActor) where T : MonoBehaviour, IDescription
    {
        IEnumerable<T> actors;
        if (typeof(T) == typeof(U_ThiefAI))
        {
            actors = thieves as IEnumerable<T>;
        }
        else if (typeof(T) == typeof(U_PoliceAI))
        {
            actors = polices as IEnumerable<T>;
        }
        else if (typeof(T) == typeof(U_PedestrianAI))
        {
            actors = pedestrians as IEnumerable<T>;
        }
        else if (typeof(T) == typeof(U_Player))
        {
            return player as T;
        }
        else
        {
            throw new ArgumentException($"Unsupported actor type: {typeof(T).Name}");
        }


        T closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (T actor in actors)
        {
            if (actor == null)
            {
                continue;
            }

            Vector3 traceToActor = (actor.Transform.position - currentActor.Transform.position).normalized;
            float distanceToCurrentActor = Vector3.Distance(currentActor.Transform.position, actor.transform.position);
            RaycastHit hit;


            if (!Physics.Raycast(
                    currentActor.Transform.position,
                    traceToActor,
                    out hit,
                    distanceToCurrentActor) || hit.collider.GetComponentInParent<T>() != actor)
            {
                continue;
            }



            if (distanceToCurrentActor < closestDistance)
            {
                closestDistance = distanceToCurrentActor;
                closest = actor;
            }
        }

        return closest;
    }
}
