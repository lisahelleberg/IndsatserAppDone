using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;
using Newtonsoft.Json;
using Windows.UI.Popups;

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
        /// Konstruktor
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

        }
        /// <summary>
        /// Buttons til databind
        /// </summary>
        public void AddNewMember()
        {
            //If kommando der nægter at oprette ny medarbejder hvis ID allerede eksistere.
            bool findesID = this.findes(NewMedarbejder.ID);
            if (!findesID)
            {
                Model.Medarbejder TempMedarbejder = new Model.Medarbejder();
                TempMedarbejder.funktion = NewMedarbejder.funktion;
                TempMedarbejder.ID = NewMedarbejder.ID;
                TempMedarbejder.navn = NewMedarbejder.navn;
                Medarbejderliste.Add(TempMedarbejder);
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("ID findes allerede");
                messageDialog.ShowAsync().AsTask();
            }
            //opdateret denne så funktionen virker ordentligt og ikke overskriver når der laves nye objekter
           
        }

        //Potentiel funktion til at afvise medarbejder med samme/ens ID
        public bool findes(int ID)
        {
            foreach (var medarbejder in Medarbejderliste)
            {
                if (ID == medarbejder.ID)
                {
                    return true;
                }
            }
            return false;
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
            
            try
            {
            StorageFile file = await localfolder.GetFileAsync(filnavn);

            string jsonText = await FileIO.ReadTextAsync(file);

            this.Medarbejderliste.Clear();

            // nu skal vi kalde metoden på medarbejderlisten
            Medarbejderliste.IndsætJson(jsonText);

            // Try og catch for at fange en exception for at undgå grimme fejlmeddelser
            }
            catch (Exception)
            {
                //For at se noget data vise sig når den fanger fejlen
                MessageDialog messageDialog = new MessageDialog("Filen ikke fundet. Har du fjernet den?");
                await messageDialog.ShowAsync();
                //throw;
            }
        }





    }
}
