using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;
using ServiceLocator.Main;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectileController
    {
        private ProjectileView projectileView;
        private ProjectileScriptableObject projectileScriptableObject;

        private BloonController target;

        public ProjectileController(ProjectileView projectilePrefab)
        {
            projectileView = Object.Instantiate(projectilePrefab);
            projectileView.SetController(this);
        }

        public void Init(ProjectileScriptableObject projectileScriptableObject)
        {
            target = null;
            this.projectileScriptableObject = projectileScriptableObject;
            projectileView.SetSprite(projectileScriptableObject.Sprite);
            projectileView.gameObject.SetActive(true);
        }

        public void SetPosition(Vector3 spawnPosition) => projectileView.transform.position = spawnPosition;

        public void SetTarget(BloonController target)
        {
            this.target = target;
            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            Vector3 direction = target.Position - projectileView.transform.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
            projectileView.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void UpdateProjectileMotion()
        {
            if(target != null)
                projectileView.transform.Translate(Vector2.up * projectileScriptableObject.Speed * Time.deltaTime, Space.Self);
        }

        public void OnHitBloon(BloonController bloonHit)
        {
            bloonHit.TakeDamage(projectileScriptableObject.Damage);
            ResetProjectile();
        }

        public void ResetProjectile()
        {
            projectileView.gameObject.SetActive(false);
            GameService.Instance.PlayerService.ReturnProjectileToPool(this);
        }
    }
}