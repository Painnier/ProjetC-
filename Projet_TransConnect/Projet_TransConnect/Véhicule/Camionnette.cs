﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Camionnette : Vehicule
    {
        String usage;
        public Camionnette(int immat, string usage) : base(immat) 
        {
            this.usage = usage;
        }

        public String Usage {get { return usage; }}
        public override string ToString()
        {
            return base.ToString() + ' ' + usage;
        }
    }
}
