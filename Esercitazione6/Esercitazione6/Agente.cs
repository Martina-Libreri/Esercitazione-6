using System;
using System.Collections.Generic;
using System.Text;

namespace Esercitazione6
{
    public class Agente : Persona
    {
        public string AnniServizio { get; set; }

        public Agente (string nome, string cognome, string cf, string anni) : base(nome, cognome, cf)
        {
            AnniServizio = anni;
        }

        public virtual void Visualizzazione()
        {
            Console.WriteLine(GetPersona() + " AnniServizio: " + AnniServizio); 
        }

        public override string GetPersona()
        {
             return base.GetPersona();
        }
    }
}
