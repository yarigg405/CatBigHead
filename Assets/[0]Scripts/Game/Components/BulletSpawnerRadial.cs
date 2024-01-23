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

            for (int i = 0; i < bulletsCountInBurst; i++)
            {
                var bullet = pool.SpawnObject();

                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(i * shotAngleDelta, 90f, 0f);
                bullet.Get<MoveComponent>().Move(bullet.transform.forward);
            }
        }
    }
}
