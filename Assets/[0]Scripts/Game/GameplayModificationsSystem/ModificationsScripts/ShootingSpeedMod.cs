using Game.Components;
using UnityEngine;
using VContainer;


namespace Game
{
    [CreateAssetMenu(fileName = "ShootingSpeedMod", menuName = "Modifications/ShootingSpeedMod", order = 51)]
    internal sealed class ShootingSpeedMod : GameplayModification
    {
        [SerializeField] private Team team;
        [SerializeField] private float boltsSpeedModificator;
        [SerializeField] private float rateOfFireModificator;


        [Inject]
        private void Construct(ModificationsBlackboard modificationsBlackboard, PlayerProvider playerProvider)
        {
            if (team == Team.Player)
            {
                var key = BlackboardConstants.BulletSpeedMod_Player;
                var speedMod = modificationsBlackboard.GetVariable<float>(key);
                speedMod += boltsSpeedModificator;
                modificationsBlackboard.SetVariable(key, speedMod);

                key = BlackboardConstants.RateOfFireMod_Player;
                var timerMod = modificationsBlackboard.GetVariable<float>(key);
                timerMod += rateOfFireModificator;
                modificationsBlackboard.SetVariable(key, timerMod);

                var shotTimer = playerProvider.Player.GetEntityComponent<ShootTimerComponent>();
                shotTimer.ShotTimeModificator = timerMod;
            }

            else
            {
                var key = BlackboardConstants.BulletSpeedMod_Enemy;
                var speedMod = modificationsBlackboard.GetVariable<float>(key);
                speedMod += boltsSpeedModificator;
                modificationsBlackboard.SetVariable(key, speedMod);

                key = BlackboardConstants.RateOfFireMod_Enemy;
                var timerMod = modificationsBlackboard.GetVariable<float>(key);
                timerMod += rateOfFireModificator;
                modificationsBlackboard.SetVariable(key, timerMod);
            }

        }
    }
}
