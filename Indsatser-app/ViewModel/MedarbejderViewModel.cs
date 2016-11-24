using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;
using Newtonsoft.Json;

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
        public Model.Medarbejder NewMedarbejder { get; private set; }
        public AddMemberCommand Removemedarbejder { get; private set; }
        public AddMemberCommand SaveMedarbejderListe { get; private set; }
        public AddMemberCommand HentDataCommand { get; private set; }

        StorageFolder localfolder = null;

        public MedarbejderViewModel()
        {
            Medarbejderliste = new Model.Medarbejderliste();
            SelectedMedarbejder = new Model.Medarbejder();
            AddMemberCommand = new AddMemberCommand(AddNewMember);
            NewMedarbejder = new Model.Medarbejder();
            Removemedarbejder = new AddMemberCommand(RemoveMember);
            localfolder = ApplicationData.Current.LocalFolder;
            SaveMedarbejderListe = new AddMemberCommand(GemDataTilDiskAsync);
            HentDataCommand = new AddMemberCommand(HentDataFraDiskAsync);
        }
        public void AddNewMember()
        {
            Medarbejderliste.Add(NewMedarbejder);
        }
        public void RemoveMember()
        {
            Medarbejderliste.Remove(selectedMedarbejder);
        }
        
        

        private readonly string filnavn = "JsonText.json";
        
        public async void GemDataTilDiskAsync()
        {
            string jsonText = this.Medarbejderliste.GetJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        //FileIO er en statisk klasse og det tager vi til næste semester.
        public async void HentDataFraDiskAsync()
        {
            this.Medarbejderliste.Clear();
            StorageFile file = await localfolder.GetFileAsync(filnavn);
            string jsonText = await FileIO.ReadTextAsync(file);

            // nu skal vi kalde metoden på medarbejderlisten
            Medarbejderliste.IndsætJson(jsonText);
        }





    }
}
