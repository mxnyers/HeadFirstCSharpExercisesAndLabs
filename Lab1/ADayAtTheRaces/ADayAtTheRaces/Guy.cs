using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADayAtTheRaces
{
    public class Guy
    {
        public string Name;
        public Bet MyBet;
        public int Cash;

        public RadioButton MyRadioButton;
        public Label MyLabel;

        public Guy()
        {
            MyBet = new Bet(this);
        }

        public void UpdateLabels()
        {
            if (MyBet != null)
            {
                MyLabel.Text = MyBet.GetDescription();
                MyRadioButton.Text = Name + @" has " + (Cash - MyBet.Amount) + @" bucks " ;
            }

            else
            {
                MyLabel.Text = Name + @" hasn't placed a bet.";
                MyRadioButton.Text = Name + @" has " + Cash + @" bucks ";
            }
            
        }

        public bool PlaceBet(int BetAmount, int DogToWin)
        {
            if(BetAmount <= Cash){
                MyBet.Amount = BetAmount;
                MyBet.Dog = DogToWin;
                UpdateLabels();
                return true;
            }
            else
            {
                MessageBox.Show(Name + @" does not have enough money to place that bet!", @"You're Broke!");
                return false;
            }

        }

        public void Collect(int Winner)
        {
            if (MyBet != null)
            {
                Cash += MyBet.PayOut(Winner);
            }
            MyBet.Amount = 0;
            MyBet.Dog = 0;
            UpdateLabels();
        }

    }
}
