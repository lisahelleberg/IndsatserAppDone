using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indsatser_app.Model
{
    class Medarbejderliste : ObservableCollection<Medarbejder>
    {
        public Medarbejderliste()
        {
            // Ny medarbejder
            Medarbejder m1 = new Medarbejder();
            m1.navn = "Lisa";
            m1.ID = 390;
            m1.funktion = "Brandmand";

            Medarbejder m2 = new Medarbejder();
            m2.navn = "Kasper";
            m2.ID = 211;
            m2.funktion = "Brandmand";

            Medarbejder m3 = new Medarbejder();
            m3.navn = "Martin";
            m3.ID = 370;
            m3.funktion = "Brandmand";

            // Tilføj til liste
            this.Add(m1);
            this.Add(m2);
            this.Add(m3);
        }
    }
}
