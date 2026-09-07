using UnityEngine;

public class U_PoliceAI : U_BaseAI
{
    protected override void UpdateAI()
    {
        U_ThiefAI actorToSeek = SingletonActors.Instance.TraceActors<U_ThiefAI>(this);

        if (actorToSeek != null)
        {
            base.Seek(actorToSeek);
        }
        else
        {
            base.Wander();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            U_ThiefAI thief = other.GetComponent<U_ThiefAI>();
            if (thief != null)
            {
                SingletonActors.Instance.DestroyActor(thief);
            }
        }
    }
}
