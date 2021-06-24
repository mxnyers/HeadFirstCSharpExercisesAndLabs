using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheQuest.Enum;
using TheQuest.Interface;

namespace TheQuest
{
    class Player : Mover
    {
        private const int radius = 10;

        private Weapon _equippedWeapon;
        private List<Weapon> inventory = new List<Weapon>();

        public int HitPoints { get; private set; }
        public IEnumerable<string> Weapons
        { get
            {
                List<string> names = new List<string>();
                foreach (Weapon weapon in inventory)
                    names.Add(weapon.Name);
                return names;
            }
        }

        public Player(Game game, Point location) : base(game, location)
        {
            HitPoints = 1000;
        }

        public bool IsWeaponEquipped(string weaponName)
        {
            if(_equippedWeapon != null)
                if (weaponName.Equals(_equippedWeapon.Name))
                    return true;
            return false;
        }

        public void Hit(int maxDamage, Random random)
        {
            HitPoints -= random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            HitPoints += random.Next(1, health);
        }

        public void Equip(string weaponName)
        {
            foreach (Weapon weapon in inventory)
                if (weaponName == weapon.Name)
                    _equippedWeapon = weapon;
        }
        
        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);
            if (!game.WeaponInRoom.PickedUp)
            {
               if (NearBy(game.WeaponInRoom.Location, radius))
                {
                    game.WeaponInRoom.PickUpWeapon();
                    inventory.Add(game.WeaponInRoom);
                    Equip(game.WeaponInRoom.Name);
                }
            }
        }

        public void Attack(Direction direction, Random random)
        {
            if(_equippedWeapon != null)
                _equippedWeapon.Attack(direction, random);
        }

        public bool CheckPotionUsed(string potionName)
        {
            IPotion potion;
            bool potionUsed = true;

            foreach (Weapon weapon in inventory)
            {
                if (weapon.Name == potionName && weapon is IPotion)
                {
                    potion = weapon as IPotion;
                    potionUsed = potion.Used;
                }
            }

            return potionUsed;
        }
    }
}
