using System;
using System.Collections.Generic;
using System.Text;

namespace Esercitazione6
{
    public abstract class Persona 
    {
        //Proprietà
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CF { get; set; }

        //Costruttori
        public Persona(string nome, string cognome,string cf)
        {
            Nome = nome;  //stringa vuota
            Cognome = cognome; 
            CF = cf; 
        }


        //Metodo per ritornare Persona
        public virtual string GetPersona()
        {
            return CF + " " + " " + Nome + " " + Cognome;
        }


        public override bool Equals(object obj)  //è una condizione return true o false
        {
            return obj is Persona persona &&
                CF == persona.CF;            //se hanno CF uguale allora sono due prtsone uguali
        }

        public static bool operator == (Persona left, Persona right)
        {
            return Equals(left, right);
        }
        public static bool operator != (Persona left, Persona right)
        {
            return !(left == right);
        }

    }
}
