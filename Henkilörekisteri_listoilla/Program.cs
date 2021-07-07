using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;

namespace Henkilörekisteri_taulukoilla
{

    /*
    
     TODO: Olemassa olevien tietojen muuttaminen ei ole vielä mahdollista
    
     */

    class Program
    {


        static bool ohjelmajatkuu = true;
        static int valikkovalinta;
        static string etunimi = "";
        static string sukunimi = "";
        static string henkilotunnus = "";
        static string[] sarakkeidennnimet = new string[9] { "HENKILÖTUNNUS", "ETUNIMI", "SUKUNIMI", "SYNTYMÄAIKA", "SUKUPUOLI", "OSOITE", "POSTINUMERO", "POSTITOIMIPAIKKA", "TIETUEEN LUONTIAIKA" };
        static bool henkilölöytyi;
        static string tiedostopolku = @"../../henkilorekisterintiedot.txt";
        static void Main(string[] args)
        {





           
            if (!File.Exists(tiedostopolku))
            {
                File.Create(tiedostopolku);
            }





           
            List<Henkilo> Henkilorekisteri = new List<Henkilo>();
            

            
            DateTime tanaan = DateTime.Today;
            DateTime kellonaika = DateTime.Now;
            CultureInfo culture = new CultureInfo("en-US");

            
           

            Console.WriteLine("Tervetuloa henkilötietorekisteriin.");



            while (ohjelmajatkuu)
            {
                Henkilorekisteri = LataaHenkilorekisteri(tiedostopolku);
                try
                {
                    Console.WriteLine("\nMitä haluat tehdä?");
                    Console.WriteLine("1)Näyttää henkilön tiedot.");
                    Console.WriteLine("2)Näyttää kaikkien henkilöiden tiedot.");
                    Console.WriteLine("3)Syöttää uusi henkilötieto.");
                    Console.WriteLine("4)Poistaa henkilötieto.");
                    Console.WriteLine("5)Lopettaa ohjelma.");

                    valikkovalinta = int.Parse((Console.ReadLine()));


                    if (valikkovalinta == 1)
                    {


                       
                        Console.WriteLine("Syötä näytettävän henkilön numerotunnus");
                        henkilotunnus = Console.ReadLine();

                        using (StreamReader lukija = File.OpenText(tiedostopolku))
                        {
                            henkilölöytyi = false;
                            string rivi;
                            while ((rivi = lukija.ReadLine()) != null)
                            {
                                if (rivi.Contains(henkilotunnus))
                                {
                                    henkilölöytyi = true;

                                    Console.WriteLine("\nHenkilön tiedot löytyivät\n");
                                    Console.WriteLine("HENKILÖTUNNUS  ETUNIMI  SUKUNIMI     SYNTYMÄAIKA   SUKUPUOLI  OSOITE   POSTINUMERO   POSTITOIMIPAIKKA     TIETUEEN LUONTIAIKA  ");
                                    Console.WriteLine(rivi.Replace(";", "\t"));
                                }
                               
                            }
                             if(!henkilölöytyi) Console.WriteLine("\nHenkilön tietoja ei löytynyt.");

                            
                        }


                    }

                    else if (valikkovalinta == 2)
                    {
                        Console.WriteLine("\n\nKAIKKIEN HENKILÖIDEN TIEDOT:");
                        Console.WriteLine("Henkilörekisterissä on yhteensä " + Henkilorekisteri.Count + " henkilön tiedot.");
                        Console.WriteLine("\n");
                        NaytaKaikkiTiedot();

                    }
                    
                    //TODO:  kirjoitus vain väliaikaiseen muistiin puuttuu
                    else if (valikkovalinta == 3)
                    {
                     
                          
                            Henkilorekisteri.Add(new Henkilo(false, true));
                            int rekisterinindeksi = 0;
                       
                        //Poistetaan mahdollinen kahdennos listan lopusta

                            foreach(Henkilo hlo in Henkilorekisteri)
                           {
                             if (hlo.KerroTunnus().Equals(Henkilorekisteri[Henkilorekisteri.Count - 1].KerroTunnus()) &&  !(rekisterinindeksi==Henkilorekisteri.Count-1))
                              {
                                
                                Henkilorekisteri.Remove(hlo);
                               }

                            }


                    }
                    //TODO: käsittelemättä
                    else if (valikkovalinta == 4)
                    {
                        henkilölöytyi = false;

                        Console.WriteLine("\nSyötä poistettavan henkilön etunimi");
                        etunimi = Console.ReadLine();
                        Console.WriteLine("Syötä poistettavan henkilön sukunimi");
                        sukunimi = Console.ReadLine();
                        Console.WriteLine("Syötä poistettavan henkilön tunnus merkkijonona");
                        henkilotunnus = Console.ReadLine();

                        foreach (Henkilo hlo in Henkilorekisteri)
                        {
                            if (hlo.KerroTunnus().Equals(henkilotunnus) && hlo.KerroEtuNimi().Equals(etunimi) && hlo.KerroSukuNimi().Equals(sukunimi))
                            {

                                try
                                {
                                    hlo.PoistaHenkiloTiedostosta();
                                }
                                catch (InvalidOperationException) { Console.WriteLine("InvalidOperationException henkilöä poistettaessa"); }

                                Henkilorekisteri.Remove(hlo);

                                henkilölöytyi = true;
            
                            }
                            
                        }
                         if (!henkilölöytyi) Console.WriteLine("Poistettavaa henkilöä ei ole rekisterissä.");
                    }

                    else if (valikkovalinta == 5)
                    {



                        ohjelmajatkuu = false;

                    }

                    else
                    {

                        Console.WriteLine("\nVirheellinen valinta, yritä uudestaan.");
                    }




                }

                catch (Exception)
                {
                    Console.WriteLine("\nVirhe ohjelmaa suoritettaessa. Todennäköisesti syötettiin tieto virheellisessä muodossa tai poistettavaa tietoa ei ollut olemassa.");


                }


            }


            
        }

        private static List<Henkilo> LataaHenkilorekisteri(string tiedostopolku)
        {
            List<Henkilo> Henkilorekisteri = new List<Henkilo>();
            // Ladataan henkilörekisteritiedosto

            using (StreamReader lukija = File.OpenText(tiedostopolku))
            {
                string alkulatausrivi;

                while ((alkulatausrivi = lukija.ReadLine()) != null)
                {
                    string[] rivintiedot = alkulatausrivi.Split(';');
                    Henkilorekisteri.Add(new Henkilo(true, false, rivintiedot));

                }
              
              
            }
            return Henkilorekisteri;
        }

        private static void NaytaKaikkiTiedot()
        {

            
            using (StreamReader lukija = File.OpenText(tiedostopolku))
            {
                string rivi;
                int[] sarakkeidenpituudet = new int[9];
                int[] kenttienpituudet = new int[9];
                bool[] sisennasarakkeet = new bool[9];
                int indeksi = 0;
                string sisennys = "";

                foreach (string sarake in sarakkeidennnimet)
                {



                    sarakkeidenpituudet[indeksi] = sarake.Length;
                    indeksi++;
                }
                Console.WriteLine("\n");
                indeksi = 0;

                while ((rivi = lukija.ReadLine()) != null)
                {


                    string[] naytettavarivitaulukkona = rivi.Split(';');


                    foreach (string kentta in naytettavarivitaulukkona)
                    {
                        kenttienpituudet[indeksi] = kentta.Length;

                        indeksi++;
                    }

                    indeksi = 0;

                    Console.WriteLine();

                    for (int i = 0; i < sarakkeidenpituudet.Length; i++)
                    {
                        if (naytettavarivitaulukkona[i].Length > sarakkeidenpituudet[i])
                            sisennasarakkeet[i] = true;
                        else sisennasarakkeet[i] = false;

                        indeksi++;
                    }

                    indeksi = 0;
                    sisennys = "";

                    foreach (string sarake in sarakkeidennnimet)
                    {


                        if (sisennasarakkeet[indeksi])
                        {
                            for (int i = 0; i < (naytettavarivitaulukkona[i].Length - sarake.Length) / 2; i++)
                            {

                                sisennys += " ";
                            }
                            Console.Write(sisennys + " | " + sarake + " | " + sisennys);

                        }
                        else Console.Write(" | " + sarake + " | ");


                        sisennys = "";
                    }

                    Console.WriteLine();


                    indeksi = 0;
                    sisennys = "";

                    foreach (String kentta in naytettavarivitaulukkona)
                    {


                        if (sisennasarakkeet[indeksi])
                            Console.Write(" | " + kentta + " | ");
                        else
                        {
                            for (int i = 0; i < (sarakkeidenpituudet[i] - kentta.Length) / 2; i++)
                                sisennys += " ";
                            Console.Write(sisennys + " | " + kentta + " | "+ sisennys);
                        }
                        sisennys = "";
                        indeksi++;
                    }

                    indeksi = 0;
                    sisennys = "";

                    Console.WriteLine();


                }

                /* testausta varten 
                foreach (Henkilo hlo in Henkilorekisteri) Console.WriteLine(hlo.KerroTunnus());    
                */
                Console.WriteLine("\n\n\n");
                
            }


        }
              

    }

    

    class CultureInfo
    {
        private string culture;

        public CultureInfo(string culture) => this.culture = culture;
    }
}
