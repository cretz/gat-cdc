using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Project.Model;
using System.Collections;

namespace Gat.Common.Project
{
    public class Optimizer
    {
        protected project _proj;

        public Optimizer(project proj)
        {
            _proj = proj;
        }

        public virtual void Optimize()
        {
            //do nothing
        }
    }
}
