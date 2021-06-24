using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheQuest.Enum;
using TheQuest.Interface;

namespace TheQuest.WeaponSubClass
{
    class BluePotion : Weapon, IPotion
    {
        public BluePotion(Game game, Point location) : base(game, location)
        {
            Used = false;
        }
        public override string Name => "Blue Potion";

        public bool Used { get; private set; }

        public override void Attack(Direction direction, Random random)
        {
           game.IncreasePlayerHealth(5, random);
           Used = true;
        }

        
    }
}
