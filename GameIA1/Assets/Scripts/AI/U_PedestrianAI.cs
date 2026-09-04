using UnityEngine;

public class U_PedestrianAI : U_BaseAI
{
    protected override void UpdateAI()
    {
        U_ThiefAI actorToSeek = SingletonActors.Instance.TraceActors<U_ThiefAI>(this);

        if (actorToSeek != null)
        {
            base.Flee(actorToSeek);
        }
        else
        {
            base.Wander();
        }
    }
}
