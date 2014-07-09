using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge170Intermediate.Cards
{
    public class Hand : List<Card>
    {
        public static Func<Card, int> OrderByRank = c => ((int)c.Suit - 0x2660) * 100 + (int)c.Rank;
        public static Func<Card, int> OrderBySuit = c => (int)c.Suit + (int)c.Rank * 15;

        public Hand()
            : base()
        {

        }

        public Hand(IEnumerable<Card> cards, Func<Card, int> sortBy, bool reverse = false)
            : base(reverse ? cards.OrderByDescending(sortBy) : cards.OrderBy(sortBy))
        {

        }

        public Hand(int initialCapacity)
            : base(initialCapacity)
        {

        }

        public IEnumerable<Card> GetFromHand(HandPosition position, int number)
        {
            if (position == HandPosition.Start)
            {
                return this.Take(number);
            }
            else
            {
                return this.Reverse<Card>().Take(number).Reverse<Card>();
            }
        }


        public override string ToString()
        {
            return String.Join(", ", this);
        }

        public enum HandPosition
        {
            Start,
            End
        }
    }
}
