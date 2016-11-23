using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Indsatser_app.ViewModel
{
    class MedarbejderViewModel : INotifyPropertyChanged
    {
        public Model.Medarbejderliste Medarbejderliste { get; set; }

        private Model.Medarbejder selectedMedarbejder;
        public AddMemberCommand AddMemberCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public Model.Medarbejder SelectedMedarbejder
        {
            get { return selectedMedarbejder; }
            set
            {
                selectedMedarbejder = value;
                OnPropertyChanged(nameof(SelectedMedarbejder));
            }
        }
        public Model.Medarbejder NewMedarbejder { get; set; }
        public AddMemberCommand Removemedarbejder { get; private set; }

        public MedarbejderViewModel()
        {
            Medarbejderliste = new Model.Medarbejderliste();
            SelectedMedarbejder = new Model.Medarbejder();
            AddMemberCommand = new AddMemberCommand(AddNewMember);
            NewMedarbejder = new Model.Medarbejder();
            Removemedarbejder = new AddMemberCommand(RemoveMember);
        }
        public void AddNewMember()
        {
            Medarbejderliste.Add(NewMedarbejder);
        }
        public void RemoveMember()
        {
            Medarbejderliste.Remove(selectedMedarbejder);
        }


    }
}
