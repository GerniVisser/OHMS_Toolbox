using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borehole.Commands
{
    public class DelegateCommand : CommandBase
    {
        private readonly Action<object> _executeAction;

        public DelegateCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
        }

        public override void Execute(object parameter) => _executeAction(parameter);
    }
}
