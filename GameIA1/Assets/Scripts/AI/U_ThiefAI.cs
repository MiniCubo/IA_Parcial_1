using Unity.VisualScripting;
using UnityEngine;

public class U_ThiefAI : U_BaseAI
{
    protected override void UpdateAI()
    {
        U_PedestrianAI actorToSeek = SingletonActors.Instance.TraceActors<U_PedestrianAI>(this);

        if (actorToSeek != null)
        {
            base.Seek(actorToSeek);
        }
        else
        {
            U_PoliceAI police = SingletonActors.Instance.GetClosestActor<U_PoliceAI>(this);
            U_Player player = SingletonActors.Instance.GetClosestActor<U_Player>(this);
            IDescription closest = ClosestActor(police, player);
            if (closest != null)
            {
                base.Evade(closest);
            }
        }
    }

    private IDescription ClosestActor(U_PoliceAI police, U_Player player)
    {
        if (police == null && player == null) return null;
        if (police == null) return player;
        if (player == null) return police;

        float distanceToPolice = (police.Transform.position - transform.position).sqrMagnitude;
        float distanceToPlayer = (player.Transform.position - transform.position).sqrMagnitude;

        return distanceToPlayer < distanceToPolice ? player : police;
    }
}
