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
        public Model.Medarbejder SelectedMedarbejder
        {
            get { return selectedMedarbejder; }
            set
            {
                selectedMedarbejder = value;
                OnPropertyChanged(nameof(SelectedMedarbejder));
            }
        }
        /// <summary>
        /// Dette er Buttons props
        /// </summary>
        public Model.Medarbejder NewMedarbejder { get; private set; }

        public RelayCommand Removemedarbejder { get; private set; }

        public RelayCommand SaveMedarbejderListe { get; private set; }

        public RelayCommand HentDataCommand { get; private set; }

        // opbevarings folder lavet.
        StorageFolder localfolder = null;

        public Model.Medarbejderliste Medarbejderliste { get; set; }


        private Model.Medarbejder selectedMedarbejder;

        private readonly string filnavn = "JsonText.json";

        public RelayCommand AddMemberCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        /// <summary>
        /// Konstruktor + try/catch exception
        /// </summary>
        public MedarbejderViewModel()
        {
            Medarbejderliste = new Model.Medarbejderliste();
            SelectedMedarbejder = new Model.Medarbejder();
            AddMemberCommand = new RelayCommand(AddNewMember);
            NewMedarbejder = new Model.Medarbejder();
            Removemedarbejder = new RelayCommand(RemoveMember);
            localfolder = ApplicationData.Current.LocalFolder;
            SaveMedarbejderListe = new RelayCommand(GemDataTilDiskAsync);
            HentDataCommand = new RelayCommand(HentDataFraDiskAsync);
            // Try og catch for at fange en exception for at undgå grimme fejlmeddelser - Virker ikke i nu
            try
            {
                HentDataFraDiskAsync();
            }
            catch (Exception)
            {
                //For at se noget data vise sig når den fanger fejlen
                Medarbejderliste.Add(new Model.Medarbejder() { navn="børge", funktion="brandmanden", ID=200});
                //throw;
            }
        }
        /// <summary>
        /// Buttons til databind
        /// </summary>
        public void AddNewMember()
        {
            Medarbejderliste.Add(NewMedarbejder);
        }

        public void RemoveMember()
        {
            Medarbejderliste.Remove(selectedMedarbejder);
        }
        
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
