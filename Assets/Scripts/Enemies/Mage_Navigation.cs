using UnityEngine;
using Common;
namespace Enemies
{
    public class MageNavigation : MobNavigation
    {
        [SerializeField] private GameObject spell;
        [SerializeField] private Transform spellSpawnPoint;
        
        public override void PlayerInCloseRange()
        {
            base.PlayerInCloseRange();
            
            Utils.RotateToTarget(transform, player, 10);
            InstantiateBullet();
            
        }
        
        #region SpellShoot
        public void InstantiateBullet()
        {
            GameObject bullet = Instantiate(spell, spellSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(true);
        }

        #endregion
    }
}
