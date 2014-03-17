using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spelprojekt.Entities;
using Spelprojekt.Worlds;
using Spelprojekt.Graphics;
using Microsoft.Xna.Framework;
using Spelprojekt.Util;
using Microsoft.Xna.Framework.Input;

namespace Spelprojekt.Items
{
    class ItemWeapon : ItemHoldable
    {
        public int damage;
        public Bullet.DamageType damageType;
        private bool canShoot = true;
        private int timer = 0;
        public int delay;
        public ItemWeapon(Spritesheet sprite, string name, Point iconIndex, int damage, Bullet.DamageType damageType, int delay)
            : base(sprite, name, iconIndex)
        {
            this.damage = damage;
            this.damageType = damageType;
            this.delay = delay;
        }
        public void Fire(World world, Entity entity, Camera2D camera)
        {
            if (canShoot)
            {
                canShoot = false;   
                world.SpawnEntity(new Bullet(false,
                    false,
                    (entity.location + new Vector2(32)),
                    "bullet",
                    entity,
                    damage,
                    damageType,
                    (VectorUtil.GetStraightPath(entity.location, camera.GetPosition() + new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 20f))));
            }
        }
        public override void Update()
        {
            if (!canShoot)
            {
                timer++;
            }
            if (timer >= delay && !canShoot)
            {
                canShoot = true;
                timer = 0;
            }
        }
    }
}
