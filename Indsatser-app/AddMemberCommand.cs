using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Indsatser_app
{
    class AddMemberCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action execute;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute();
        }
        public AddMemberCommand(Action execute)
        {
            this.execute = execute;
        }
    }
}
