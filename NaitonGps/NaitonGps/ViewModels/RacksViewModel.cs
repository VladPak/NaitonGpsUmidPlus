using NaitonGps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGps.ViewModels
{
    public class RacksViewModel
    {
        public List<Rack> rack { get; set; }

        public RacksViewModel()
        {
            rack = new List<Rack>
            {
                new Rack
                {
                    rackId = 1, rackItemQuantity = 12, rackTitle = "A1.034.12"
                },
                new Rack
                {
                    rackId = 2, rackItemQuantity = 35, rackTitle = "A1.034.13"
                },
                new Rack
                {
                    rackId = 3, rackItemQuantity = 5, rackTitle = "A1.034.14"
                },
                new Rack
                {
                    rackId = 4, rackItemQuantity = 1, rackTitle = "A1.034.15"
                },
                new Rack
                {
                    rackId = 5, rackItemQuantity = 0, rackTitle = "A1.036.10"
                },
                new Rack
                {
                    rackId = 6, rackItemQuantity = 88, rackTitle = "A1.036.11"
                },
                new Rack
                {
                    rackId = 7, rackItemQuantity = 25, rackTitle = "A1.055.14"
                },
                new Rack
                {
                    rackId = 8, rackItemQuantity = 47, rackTitle = "A1.055.15"
                },
                new Rack
                {
                    rackId = 9, rackItemQuantity = 55, rackTitle = "A1.033.14"
                },
                new Rack
                {
                    rackId = 10, rackItemQuantity = 2, rackTitle = "A1.055.15"
                },
                new Rack
                {
                    rackId = 11, rackItemQuantity = 64, rackTitle = "A1.054.08"
                },
                new Rack
                {
                    rackId = 12, rackItemQuantity = 28, rackTitle = "A1.054.01"
                },
                new Rack
                {
                    rackId = 13, rackItemQuantity = 13, rackTitle = "A1.054.04"
                },
            };
        }

    }
}
