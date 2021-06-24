using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheQuest.Enum;

namespace TheQuest.WeaponSubClass
{
    class BattleAxe : Weapon
    {
        private const int attackRadius = 20;
        private const int damage = 6;

        public BattleAxe(Game game, Point location) : base(game, location)
        {
        }

        public override string Name
        {
            get
            {
                return "Battle Axe";
            }
        }

        public override void Attack(Direction direction, Random random)
        {
            if (!DamageEnemy(direction, attackRadius, damage, random))
            {
                if (!DamageEnemy(ClockwiseDirection(direction), attackRadius, damage, random))
                {
                    DamageEnemy(CounterClockwiseDirection(direction), attackRadius, damage, random);
                }
            }
        }
    }
}
