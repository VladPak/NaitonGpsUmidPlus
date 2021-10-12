using NaitonGps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGps.ViewModels
{
    public class RacksViewModel
    {
        public List<Rack> Racks { get; set; }

        public RacksViewModel(List<Rack> racks)
        {
            Racks = racks;
        }

    }
}
