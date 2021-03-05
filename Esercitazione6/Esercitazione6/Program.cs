using System;

namespace Esercitazione6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Gestione MENU
            int n = 1;
            do
            {
                Console.WriteLine();

                Console.WriteLine("Inserisci il num per selezionare l'operazione: ");
                Console.WriteLine("1- Mostrare tutti gli agenti");
                Console.WriteLine("2- Mostrare gli agenti assegnati ad una determinata area");
                Console.WriteLine("3- Mostrare gli agenti con anni di servizio maggiori o uguali rispetto ad un input dato ");
                Console.WriteLine("4- inserire un nuovo agente");
                Console.WriteLine("5- uscire dal menu");

                Console.WriteLine();

                int x = Int32.Parse(Console.ReadLine());

                switch (x)
                {
                    case 1:
                        Dati.ConnectedPoliz();
                        break;
                    case 2:
                        Dati.ConnectedAreaAgente();
                        break;
                    case 3:
                        Dati.ConnectedAnniServizio();
                        break;
                    case 4:
                        Dati.DisconnectedNuovoAgente();
                        break;
                    default:
                        n = 0;
                        break;
                }

            } while (n != 0);
        }
    }
}

    
