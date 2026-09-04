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
}
