using Game.Components;
using UnityEngine;


namespace Game
{
    internal sealed class BulletSpawnerRadial : BulletsSpawner
    {
        [SerializeField] private int bulletsCountInBurst = 8;

        protected override void TryShot()
        {
            var shotAngleDelta = 360f / bulletsCountInBurst;

            for (var i = 0; i < bulletsCountInBurst; i++)
            {
                var bullet = pool.SpawnObject();

                var tr = bullet.transform;

                tr.position = transform.position;
                tr.rotation = Quaternion.Euler(i * shotAngleDelta, 90f, 0f);
                bullet.GetEntityComponent<MoveComponent>().Move(tr.forward);
            }
        }
    }
}