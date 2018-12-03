using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTracker
{
    public class Command
    {
        public string Name { get; }
        public string Actions { get; set; }
        public string ResultingAction { get; }

        public Command(string name, string actions, string resultingAction)
        {
            Name = name;
            Actions = actions;
            ResultingAction = resultingAction;
        }
    }
}
