using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Challenge170Intermediate.Cards
{
    public struct Card
    {
        private static Regex CardName = new Regex(@"(.+) of (.+)");

        public CardSuit Suit
        {
            get;
            private set;
        }

        public CardRank Rank
        {
            get;
            private set;
        }

        public Card(CardSuit suit, CardRank rank)
            : this()
        {
            Suit = suit;
            Rank = rank;
        }

        public Card(string s)
            : this()
        {
            if (s == null) throw new ArgumentNullException("s");

            Match match = CardName.Match(s.ToLower());
            if (match.Success)
            {
                Group[] groups = new Group[match.Groups.Count];
                match.Groups.CopyTo(groups, 0);
                Rank = (CardRank)Enum.Parse(typeof(CardRank), groups[1].Value, true);
                Suit = (CardSuit)Enum.Parse(typeof(CardSuit), groups[2].Value, true);
            }
            else
            {
                throw new ArgumentException("The given string does not represent a valid playing card: " + s);
            }
        }

        public int GetValue(bool aceHigh)
        {
            if (Rank == CardRank.Ace)
            {
                return aceHigh ? 11 : 1;
            }
            else if (Rank == CardRank.Jack ||
                Rank == CardRank.Queen ||
                Rank == CardRank.King)
            {
                return 10;
            }
            else
            {
                return (int)Rank;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} of {1}", Rank.ToString(), Suit.ToString());
        }
    }

    public enum CardSuit
    {
        Clubs = '\u2663',
        Diamonds = '\u2666',
        Hearts = '\u2665',
        Spades = '\u2660'
    }

    public enum CardRank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
}
