using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADayAtTheRaces
{
    public class Bet
    {
        public int Amount;
        public int Dog;
        public Guy Bettor;

        public Bet(Guy bettor)
        {
            Bettor = bettor;
        }

        public string GetDescription()
        {
            if (Bettor.MyBet.Amount > 0)
            {
                return Bettor.Name + " placed a " + Amount + " dollar bet on dog number " + Dog;
            }

            else
            {
                return Bettor.Name + " hasn't placed a bet...";
            }
            
        }

        public int PayOut(int Winner)
        {
            if (Winner == Dog)
            {
                return Amount;
            }
            else
            {
                return -Amount;
            }
        }
    }
}
