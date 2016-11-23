using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indsatser_app.Model
{
    class Medarbejder
    {
        public string navn { get; set; }
        public int ID {get; set; }
        public string funktion { get; set; }




        public override string ToString()
        {
            return "Navn: " + navn + ". " + "Medarbejdernummer: " + ID + ". " + "Funktion: " + funktion;
        }
    }
}
