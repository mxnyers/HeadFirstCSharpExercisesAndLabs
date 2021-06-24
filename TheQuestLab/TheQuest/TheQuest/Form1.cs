using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheQuest;
using TheQuest.EnemySubClass;
using TheQuest.Enum;

namespace TheQuest
{
    public partial class Form1 : Form
    {
        private Game game;
        private Random random = new Random();
        private bool isPotionNeeded = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void QuestGame_Load(object sender, EventArgs e)
        {
            game = new Game(new Rectangle(125, 94, 700, 280));
            game.NewLevel(random);
            UpdateCharacters();
            SetTheLevel();
        }

        private void moveButtonUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
        }

        private void moveButtonLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
        }

        private void moveButtonRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
        }

        private void moveButtonDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
        }

        private void inventoryItem1_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(inventoryItem1, "Sword", "Weapon");
            UpdateCharacters();
        }

        private void inventoryItem2_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(inventoryItem2, "Bow", "Weapon");
            UpdateCharacters();
        }

        private void inventoryItem3_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(inventoryItem3, "Mace", "Weapon");
            UpdateCharacters();
        }

        private void inventoryItem4_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(inventoryItem4, "Red Potion", "Potion");
            UpdateCharacters();
        }

        private void inventoryItem5_Click(object sender, EventArgs e)
        {
            SelectInventoryItem(inventoryItem5, "Blue Potion", "Potion");
            UpdateCharacters();
            
        }

        private void attackButtonUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
        }

        private void attackButtonRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
        }

        private void attackButtonDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
        }

        private void attackButtonLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
        }

        private void UpdateCharacters()
        {
            playerHitPoints.Text = game.PlayerHitPoints.ToString();
            player.Location = game.PlayerLocation;
            int enemiesShown = 0;
            enemiesShown = CountEnemies();

            Control weaponControl = null;
            SetPictureBoxVisibility();
            weaponControl = SetVisibilityToWeaponInRoom(weaponControl);
            weaponControl.Visible = true;
            CheckPlayerInventory();
            weaponControl.Location = game.WeaponInRoom.Location;

            if (game.WeaponInRoom.PickedUp)
                weaponControl.Visible = false;
            else
            {
                weaponControl.Visible = true;
                weaponControl.Location = game.WeaponInRoom.Location;
            }

            if(game.PlayerHitPoints <= 0)
            {
                MessageBox.Show("You died", "System...");
                Application.Exit();
            }

            if (enemiesShown < 1)
            {
                MessageBox.Show("You have defeated the enemies on this level");
                game.NewLevel(random);
                UpdateCharacters();
            }
        }

        private void SelectInventoryItem(PictureBox item, string itemName, string weaponType)
        {
            if (game.CheckPlayerInventory(itemName))
            {
                game.Equip(itemName);
                RemoveInventoryBorders();
                item.BorderStyle = BorderStyle.FixedSingle;
                SetupAttackButtons(weaponType);
                RemoveInventoryBorders();
            }
        }
        private void RemoveInventoryBorders()
        {
            inventoryItem1.BorderStyle = BorderStyle.None;
            inventoryItem2.BorderStyle = BorderStyle.None;
            inventoryItem3.BorderStyle = BorderStyle.None;
            inventoryItem4.BorderStyle = BorderStyle.None;
            inventoryItem5.BorderStyle = BorderStyle.None;
        }
        private void SetupAttackButtons(string weaponType)
        {
            if ("potion".Equals(weaponType.ToLower()))
            {
                attackButtonUp.Text = "Drink";
                attackButtonDown.Visible = false;
                attackButtonLeft.Visible = false;
                attackButtonRight.Visible = false;
            }
            if ("weapon".Equals(weaponType.ToLower()))
            {
                    attackButtonUp.Text = "↑";
                    attackButtonDown.Visible = true;
                    attackButtonLeft.Visible = true;
                    attackButtonRight.Visible = true;
            }
        }
        private void SetPictureBoxVisibility()
        {
            sword.Visible = false;
            bow.Visible = false;
            mace.Visible = false;
            bluePotion.Visible = false;
            redPotion.Visible = false;
        }
       private void CheckPlayerInventory()
        {
            CheckPlayerWeapon("Sword", "Weapon", inventoryItem1);
            CheckPlayerWeapon("Bow", "Weapon", inventoryItem2);
            CheckPlayerWeapon("Mace", "Weapon", inventoryItem3);
            CheckPlayerPotion("Red Potion", "potion", inventoryItem4);
            CheckPlayerPotion("Blue Potion", "potion", inventoryItem5);
        }

        private void CheckPlayerWeapon(string weaponName, string weaponTyp, PictureBox weaponPictureBox)
        {
            weaponPictureBox.BorderStyle = BorderStyle.None;
            if (game.CheckPlayerInventory(weaponName))
            {
                weaponPictureBox.Visible = true;
                if (game.IsWeaponEquipped(weaponName))
                {
                    weaponPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    SetupAttackButtons(weaponTyp);
                }
            }
        }

        private void CheckPlayerPotion(string potionName, string weaponTyp, PictureBox weaponPictureBox)
        {
            weaponPictureBox.BorderStyle = BorderStyle.None;
            if (game.CheckPlayerInventory(potionName))
            {
                if (!game.CheckPotionUsed(potionName))
                {
                    weaponPictureBox.Visible = true;
                    if (game.IsWeaponEquipped(potionName))
                    {
                        weaponPictureBox.BorderStyle = BorderStyle.FixedSingle;
                        SetupAttackButtons(weaponTyp);
                        isPotionNeeded = true;
                    }
                }
                else
                {
                    weaponPictureBox.BorderStyle = BorderStyle.None;
                    weaponPictureBox.Visible = false;
                    if (isPotionNeeded)
                    {
                        game.Equip("Sword");
                        CheckPlayerWeapon("Sword", "Weapon", inventoryItem1);
                        SetupAttackButtons("weapon");
                        isPotionNeeded = false;
                    }
                }
            }

        }
        private bool UpdateEnemy(Enemy enemy, PictureBox pictureBoxEnemy, Label labelEnemyHitPoints)
        {
            bool isEnemyUpdated = false;

            labelEnemyHitPoints.Text = enemy.HitPoints.ToString();
            labelEnemyHitPoints.Visible = true;
            if (enemy.HitPoints > 0)
            {
                pictureBoxEnemy.Location = enemy.Location;
                pictureBoxEnemy.Visible = true;
                isEnemyUpdated = true;
            }
            else
            {
                pictureBoxEnemy.Visible = false;
                labelEnemyHitPoints.Visible = false;
            }

            return isEnemyUpdated;
        }
        private int CountEnemies()
        {
            int enemiesShown = 0;

            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    if (UpdateEnemy(enemy, bat, batHitPoints))
                        enemiesShown++;
                }
                if (enemy is Ghost)
                {
                    if (UpdateEnemy(enemy, ghost, ghostHitPoint))
                        enemiesShown++;
                }
                if (enemy is Ghoul)
                {
                    if (UpdateEnemy(enemy, ghoul, ghoulHitPoint))
                        enemiesShown++;
                }
            }
            return enemiesShown;
        }
        private Control SetVisibilityToWeaponInRoom(Control weaponControl)
        {
            switch (game.WeaponInRoom.Name)
            {
                case "Sword":
                    weaponControl = sword;
                    break;
                case "Bow":
                    weaponControl = bow;
                    break;
                case "Mace":
                    weaponControl = mace;
                    break;
                case "Red Potion":
                    weaponControl = redPotion;
                    break;
                case "Blue Potion":
                    weaponControl = bluePotion;
                    break;
            }
            return weaponControl;
        }
        private void SetTheLevel()
        {
            player.BringToFront();
            bat.SendToBack();
            ghost.SendToBack();
            ghoul.SendToBack();
            bow.SendToBack();
            mace.SendToBack();
            sword.SendToBack();
            bluePotion.SendToBack();
            redPotion.SendToBack();
        }
    }
}