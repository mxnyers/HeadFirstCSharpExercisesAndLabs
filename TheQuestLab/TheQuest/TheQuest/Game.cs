using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using TheQuest.EnemySubClass;
using TheQuest.Enum;
using TheQuest.WeaponSubClass;

namespace TheQuest
{
     class Game
    {
        private Player player;
        private int level = 0;
        private Rectangle boundaries;

        public Rectangle Boundaries => boundaries;
        public List<Enemy> Enemies;
        public Weapon WeaponInRoom { get; private set; }
        public Point PlayerLocation => player.Location;
        public int PlayerHitPoints => player.HitPoints;
        public IEnumerable<string> PlayerWeapons => player.Weapons;
        public int Level => level;

        public Game(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            player = new Player(this, new Point(boundaries.Left + 10, boundaries.Top + 70));
        }

        public void Move(Direction direction, Random random)
        {
            player.Move(direction);
            foreach (Enemy enemy in Enemies)
                enemy.Move(random);
        }

        public bool CheckPotionUsed(string potionName)
        {
            return player.CheckPotionUsed(potionName);
        }
        public bool IsWeaponEquipped(string weaponName)
        {
            return player.IsWeaponEquipped(weaponName);
        }
        public void Equip(string weaponName)
        {
            player.Equip(weaponName);
        }
        public bool CheckPlayerInventory(string weaponName)
        {
            return player.Weapons.Contains(weaponName);
        }
        public void HitPlayer(int maxDamage, Random random)
        {
            player.Hit(maxDamage, random);
        }
        public void IncreasePlayerHealth(int health, Random random)
        {
            player.IncreaseHealth(health, random);
        }
        public void Attack(Direction direction, Random random)
        {
            player.Attack(direction, random);
            foreach (Enemy enemy in Enemies)
                enemy.Move(random);
        }

        public void NewLevel(Random random)
        {
            level++;
            switch (level)
            {
                case 1:
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Sword(this, GetRandomLocation(random));
                    break;
                case 2:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Ghost(this, GetRandomLocation(random)) };
                    WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    break;
                case 3:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    break;
                case 4:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)), new Ghost(this, GetRandomLocation(random)) };
                    if (CheckPlayerInventory("Bow"))
                    {
                        if (!CheckPlayerInventory("Blue Potion")|| (CheckPlayerInventory("Blue Potion") && player.CheckPotionUsed("Blue Potion")))
                            WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    }
                    else
                        WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    break;
                case 5:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)), new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    break;
                case 6:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Ghost(this, GetRandomLocation(random)), new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    break;
                case 7:
                    Enemies.Clear();
                    Enemies = new List<Enemy>() { new Bat(this, GetRandomLocation(random)), new Ghost(this, GetRandomLocation(random)), new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = null;
                    if (CheckPlayerInventory("Mace"))
                    {
                        if (!CheckPlayerInventory("Red Potion") || (CheckPlayerInventory("Red Potion") && player.CheckPotionUsed("Red Potion")))
                            WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    }
                    else
                        WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    break;
                case 8:
                    Enemies.Clear();
                    Application.Exit();
                    break;
            }
        }

        private Point GetRandomLocation(Random random)
        {
            int x = boundaries.Left + random.Next(boundaries.Right / 10 - boundaries.Left / 10) * 10;
            int y = boundaries.Top + random.Next(boundaries.Bottom / 10 - boundaries.Top / 10) * 10;
            return new Point(x, y);
        }
    }
}
