using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADayAtTheRaces
{
    public partial class Form1 : Form
    {
        public Greyhound[] dogs = new Greyhound[4];
        public Guy[] guys = new Guy[3];
        public Random myRandomizer = new Random();

        public Form1()
        {
            InitializeComponent();


            guys[0] = new Guy()
            {
                Name = "Joe",
                MyRadioButton = joeRadioButton,
                MyLabel = joeBetLabel,
                Cash = 50
            };

            guys[1] = new Guy()
            {
                Name = "Bob",
                MyRadioButton = bobRadioButton,
                MyLabel = bobBetLabel,
                Cash = 75
            };

            guys[2] = new Guy()
            {
                Name = "Al",
                MyRadioButton = alRadioButton,
                MyLabel = alBetLabel,
                Cash = 45

            };

            foreach (var betGuy in guys)
            {
                betGuy.UpdateLabels();
            }

            dogs[0] = new Greyhound()
            {
                dogNumber = 1,
                MyPictureBox = pictureBox2,
                RacetrackLength = 480,
                StartingPosition = 20,
                Randomizer = myRandomizer
            };

            dogs[1] = new Greyhound()
            {
                dogNumber = 2,
                MyPictureBox = pictureBox3,
                RacetrackLength = 480,
                StartingPosition = 20,
                Randomizer = myRandomizer
            };

            dogs[2] = new Greyhound()
            {
                dogNumber = 3,
                MyPictureBox = pictureBox4,
                RacetrackLength = 480,
                StartingPosition = 20,
                Randomizer = myRandomizer
            };

            dogs[3] = new Greyhound()
            {
                dogNumber = 4,
                MyPictureBox = pictureBox5,
                RacetrackLength = 480,
                StartingPosition = 20,
                Randomizer = myRandomizer
            };

            minimumBetLabel.Text = @"Minimum bet: " + betAmountControl.Minimum + @" bucks";
        }


        private void raceButton_Click(object sender, EventArgs e)
        {
            bool winner = false;
            raceButton.Enabled = false;

            while (!winner)
            {
                foreach (var dog in dogs)
                {
                    winner = dog.Run();

                    if (winner)
                    {
                        MessageBox.Show(dog.dogNumber + @" won the race");
                        dogs[0].TakeStartingPosition();
                        dogs[1].TakeStartingPosition();
                        dogs[2].TakeStartingPosition();
                        dogs[3].TakeStartingPosition();

                        foreach (Guy winningsGuy in guys)
                        {
                            winningsGuy.Collect(dog.dogNumber);
                        }

                        break;
                    }
                }
            }

            raceButton.Enabled = true;
        }

        private void betButton_Click(object sender, EventArgs e)
        {
            if (joeRadioButton.Checked)
            {
                guys[0].PlaceBet((int)betAmountControl.Value, (int)dogNumberControl.Value);
                
            }

            if (bobRadioButton.Checked)
            {
                guys[1].PlaceBet((int)betAmountControl.Value, (int)dogNumberControl.Value);
            }

            if (alRadioButton.Checked)
            {
                guys[2].PlaceBet((int)betAmountControl.Value, (int)dogNumberControl.Value);
            }

        }

        private void joeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = guys[0].Name;
        }

        private void bobRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = guys[1].Name;
        }

        private void alRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = guys[2].Name;
        }
    }
}
