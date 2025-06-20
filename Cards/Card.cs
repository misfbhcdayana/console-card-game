using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    public class Card
    {
        public string Value { get; set; }
        public bool Revealed { get; set; }

        //Each card has two properties
        //the value of it's string(Value) and the conditon whether it is revealed or not(Revealed)
        public Card(string value)
        {
            Value = value;
            Revealed = false;
        }
    }
}
