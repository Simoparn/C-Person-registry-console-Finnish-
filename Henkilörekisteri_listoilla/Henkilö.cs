using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Henkilörekisteri_taulukoilla
{




    class Henkilo
    {


        string henkilotunnus;
        string etunimi;
        string sukunimi;
        DateTime syntymaaika;
        DateTime luontiaika;
        string sukupuoli;
        string osoite;
        string postinumero;
        string postitoimipaikka;
        string tiedostopolku = @"../../henkilorekisterintiedot.txt";
        
        

        //TODO: oma metodi henkilötiedon luontiajalle?
        // kellonajat: " vuotta vanha, tänään on " + tanaan.ToString("d") + " klo " + kellonaika.ToString("HHmmss")));

        public Henkilo(bool luetaantiedostosta, bool kirjoitetaantiedostoon, String[] rivintiedot)
        {
            
            

            if (luetaantiedostosta)
            {



                using (StreamReader lukija = File.OpenText(tiedostopolku))
                {
                    string rivi;
                    while ((rivi = lukija.ReadLine()) != null)
                    {







                        AsetaTunnus(rivintiedot[0], true);
                        AsetaNimi(rivintiedot[1], rivintiedot[2], true);
                        AsetaSyntymaaika(rivintiedot[3], true);
                        AsetaSukupuoli(rivintiedot[4], true);
                        AsetaOsoite(rivintiedot[5], true);
                        AsetaPostinumero(rivintiedot[6], true);
                        AsetaPostitoimipaikka(rivintiedot[7], true);
                        this.luontiaika = DateTime.Parse(rivintiedot[8]);

                    }

                    lukija.Close();
                }


            }


            //TODO: Turha koska jo oma konstruktori syötteiden lukemiselle?

            if (!luetaantiedostosta)
            {

                //Tarvitaan, jos tallennetaan käyttäjän antamat henkilötiedot väliaikaisesti ohjelmaan

                if (!kirjoitetaantiedostoon)
                {
                    AsetaTunnus(null, false);
                    AsetaNimi(null, null, false);
                    AsetaSyntymaaika(null, false);
                    AsetaSukupuoli(null, false);
                    AsetaOsoite(null, false);
                    AsetaPostinumero(null, false);
                    AsetaPostitoimipaikka(null, false);

                    this.luontiaika = DateTime.Now;




                }

                //Kirjoitetaan käyttäjän syöte tiedostoon luonnin yhteydessä

                else
                {
                    String[] syotarivintiedot = new String[9];

                    AsetaTunnus(null, false);
                    AsetaNimi(null, null, false);
                    AsetaSyntymaaika(null, false);
                    AsetaSukupuoli(null, false);
                    AsetaOsoite(null, false);
                    AsetaPostinumero(null, false);
                    AsetaPostitoimipaikka(null, false);

                    this.luontiaika = DateTime.Now;

                    syotarivintiedot[0] = KerroTunnus();
                    syotarivintiedot[1] = KerroEtuNimi();
                    syotarivintiedot[2] = KerroSukuNimi();
                    syotarivintiedot[3] = KerroSyntymaaika().ToString();
                    syotarivintiedot[4] = KerroSukupuoli();
                    syotarivintiedot[5] = KerroOsoite();
                    syotarivintiedot[6] = KerroPostinumero();
                    syotarivintiedot[7] = KerroPostitoimipaikka();
                    syotarivintiedot[8] = this.luontiaika.ToString();

                    TallennaTiedostoon(syotarivintiedot);
                }



            }


        }





        public Henkilo(bool luetaantiedostosta, bool kirjoitetaantiedostoon)
        {

           

            if (luetaantiedostosta)
            {
                throw new Exception("Virheellinen parametri");
                //Ei ole tarkoitus käyttää

            }



            if (!luetaantiedostosta)
            {

                //Tarvitaan, jos tallennetaan käyttäjän antamat henkilötiedot väliaikaisesti ohjelmaan

                if (!kirjoitetaantiedostoon)
                {
                    AsetaTunnus(null, false);
                    AsetaNimi(null, null, false);
                    AsetaSyntymaaika(null, false);
                    AsetaSukupuoli(null, false);
                    AsetaOsoite(null, false);
                    AsetaPostinumero(null, false);
                    AsetaPostitoimipaikka(null, false);

                    this.luontiaika = DateTime.Now;




                }

                //Kirjoitetaan käyttäjän syöte tiedostoon luonnin yhteydessä

                else
                {
                    String[] syotarivintiedot = new String[9];

                    AsetaTunnus(null, false);
                    AsetaNimi(null, null, false);
                    AsetaSyntymaaika(null, false);
                    AsetaSukupuoli(null, false);
                    AsetaOsoite(null, false);
                    AsetaPostinumero(null, false);
                    AsetaPostitoimipaikka(null, false);

                    this.luontiaika = DateTime.Now;

                    syotarivintiedot[0] = KerroTunnus();
                    syotarivintiedot[1] = KerroEtuNimi();
                    syotarivintiedot[2] = KerroSukuNimi();
                    syotarivintiedot[3] = KerroSyntymaaika().ToString();
                    syotarivintiedot[4] = KerroSukupuoli();
                    syotarivintiedot[5] = KerroOsoite();
                    syotarivintiedot[6] = KerroPostinumero();
                    syotarivintiedot[7] = KerroPostitoimipaikka();
                    syotarivintiedot[8] = this.luontiaika.ToString();

                   TallennaTiedostoon(syotarivintiedot);

                }



            }


        }



        //TODO: tarkistukset henkilötunnuksen muodolle
        public void AsetaTunnus(string tunnus, bool tiedostosta)
        {
            if (tiedostosta)
            {

                this.henkilotunnus = tunnus;

            }
            else if (!tiedostosta)
            {

                Console.WriteLine("\nAnna uuden henkilön henkilötunnus:");
                string syotatunnus = Console.ReadLine();
                this.henkilotunnus = syotatunnus;

            }




        }
        public void AsetaNimi(string etunimi, string sukunimi, bool tiedostosta)
        {

            if (tiedostosta)
            {
                this.etunimi = etunimi;
                this.sukunimi = sukunimi;
            }

            else if (!tiedostosta)
            {
                Console.WriteLine("\nAnna uuden henkilön etunimi");
                string syotaetunimi = Console.ReadLine();
                Console.WriteLine("Anna uuden henkilön sukunimi");
                string syotasukunimi = Console.ReadLine();
                this.etunimi = syotaetunimi;
                this.sukunimi = syotasukunimi;

            }


        }

        //TODO: Ei saa antaa nollia kuukausien ja päivien alussa, lisää kyselyt kellonajalle.

        public void AsetaSyntymaaika(string syntymaaika, bool tiedostosta)
        {

            if (tiedostosta)
            {
                this.syntymaaika = DateTime.Parse(syntymaaika);


            }

            else if (!tiedostosta)
            {
                Console.WriteLine("\nAnna uuden henkilön syntymävuosi muodossa (XXXX) ");
                string asetasyntymavuosi = Console.ReadLine();
                Console.WriteLine("Anna uuden henkilön syntymäkuukausi muodossa (XX) tai (X), nollia ei sallita");
                string asetasyntymakuukausi = Console.ReadLine();
                Console.WriteLine("Anna uuden henkilön syntymäpäivä muodossa (XX) tai (X), nollia ei sallita");
                string asetasyntymapaiva = Console.ReadLine();

                if (!asetasyntymakuukausi.Contains("0") || !asetasyntymapaiva.Contains("0"))
                    this.syntymaaika = new DateTime(int.Parse(asetasyntymavuosi), int.Parse(asetasyntymakuukausi), int.Parse(asetasyntymapaiva));
                else
                    throw new FormatException("Virheellinen päivämäärä, henkilötietoja ei voida tallentaa.");

            }




        }




        public void AsetaSukupuoli(string sukupuoli, bool tiedostosta)
        {

            if (tiedostosta)
            {

                this.sukupuoli = sukupuoli;


            }

            else if (!tiedostosta)
            {


                string syotasukupuoli = "";
                Console.WriteLine("\nAnna uuden henkilön sukupuoli");
                syotasukupuoli = Console.ReadLine();
                if (syotasukupuoli != "n" && syotasukupuoli != "m" && syotasukupuoli != "nainen" && syotasukupuoli != "mies" && syotasukupuoli != "Nainen" && syotasukupuoli != "Mies")
                {
                    Console.WriteLine("\nEt ole mies tai nainen. Sukupuoleksesi asetettiin 'muu'");
                    syotasukupuoli = "muu";

                }
                else if (syotasukupuoli == "nainen" || syotasukupuoli == "n")
                    this.sukupuoli = "nainen";
                else if (syotasukupuoli == "mies" || syotasukupuoli == "m")
                    this.sukupuoli = "mies";



            }


        }


        public void AsetaOsoite(string osoite, bool tiedostosta)
        {

            if (tiedostosta)
            {

                this.osoite = osoite;

            }

            else if (!tiedostosta)
            {
                Console.WriteLine("\nAnna uuden henkilön osoite");
                string syotaosoite = Console.ReadLine();
                this.osoite = syotaosoite;

            }



        }


        public void AsetaPostinumero(string postinumero, bool tiedostosta)
        {
            if (tiedostosta)
            {
                this.postinumero = postinumero;


            }

            else if (!tiedostosta)
            {
                Console.WriteLine("\nAnna uuden henkilön postinumero");
                string syotapostinumero = Console.ReadLine();
                this.postinumero = syotapostinumero;

            }



        }


        public void AsetaPostitoimipaikka(string postitoimipaikka, bool tiedostosta)
        {

            if (tiedostosta)
            {

                this.postitoimipaikka = postitoimipaikka;

            }

            else if (!tiedostosta)
            {
                Console.WriteLine("\nAnna uuden henkilön postitoimipaikka");
                string syotapostitoimipaikka = Console.ReadLine();
                this.postitoimipaikka = syotapostitoimipaikka;

            }




        }


        //TODO: tarkistukset henkilötunnuksen muodolle
        public string KerroTunnus()
        {

            return henkilotunnus;

        }

        public string KerroEtuNimi()
        {

            return etunimi;


        }

        public string KerroSukuNimi()
        {

            return sukunimi;


        }

        public DateTime KerroSyntymaaika()
        {
            return syntymaaika;


        }



        public string KerroSukupuoli()
        {

            return sukupuoli;

        }







        public string KerroIkaluokitus(int ika)
        {
            string ikaluokitus;


            if (ika >= 0 && ika < 18) ikaluokitus = "Henkilö on ala-ikäinen";
            else ikaluokitus = "Henkilö on täysi-ikäinen";

            return ikaluokitus;

        }


        public string KerroOsoite()
        {


            return osoite;
        }

        public string KerroPostinumero()
        {

            return postinumero;
        }

        public string KerroPostitoimipaikka()
        {

            return postitoimipaikka;
        }



        public void TallennaTiedostoon(string[] rivintiedot)
        {


            //Kirjoitetaan tiedostoon 



            using (StreamReader lukija = File.OpenText(tiedostopolku))
            {

                string rivi;
                bool henkiloolemassa = false;

                while ((rivi = lukija.ReadLine()) != null)
                {

                    //Tarkistetaan, onko tietue jo olemassa.


                    if (rivi.Contains(henkilotunnus))
                    {

                        henkiloolemassa = true;
                        break;
                    }


                }


                if (!henkiloolemassa)
                {
                    lukija.Close();
                    Console.WriteLine("\nHenkilön tietoja ei löytynyt, tallennus tiedostoon sallitaan.\n:");

                    //Tarkistetaan, onko rekisteriin tallennettavassa tietuerivissä oikea määrä kenttiä

                    if (rivintiedot.Length == 9)
                    {




                        try
                        {
                            using (StreamWriter kirjoittaja = File.AppendText(tiedostopolku))
                            {
                                kirjoittaja.WriteLine(KerroTunnus() + ";" + KerroEtuNimi() + ";" + KerroSukuNimi() + ";" + KerroSyntymaaika() + ";" + KerroSukupuoli() + ";" + KerroOsoite() + ";" + KerroPostinumero() + ";" + KerroPostitoimipaikka() + ";" + this.luontiaika);
                                kirjoittaja.Close();
                            }

                        }
                        catch (IOException) { Console.WriteLine("Ei voitu tallentaa uutta henkilöä rekisteriin, koska rekisteritiedostoa käytetään muualta."); }



                    }
                    else Console.WriteLine("Henkilötietokenttiä on väärä määrä, henkilötietuetta  ei voitu tallentaa.");
                    

                }
                else
                {
                    Console.WriteLine("Henkilö on jo olemassa, tietoja ei voitu tallentaa.");
                    
                }
               
            }
        }




        //TODO
        public void PoistaHenkiloTiedostosta()
        {



            try
            {

                
                    string rivi;
                    string kopioitavarivi;
                    string korjattutiedosto = "";
                    bool loytyipoistettava = false;


                using (StreamReader lukija = File.OpenText(tiedostopolku))
                {

                    while ((rivi = lukija.ReadLine()) != null)
                    {



                        if (rivi.Contains(KerroTunnus()) && rivi.Contains(KerroEtuNimi()) && rivi.Contains(KerroSukuNimi()))
                        {



                            loytyipoistettava = true;
                        }

                    }


                 

                }

                if (loytyipoistettava)
                {
                    using (StreamReader kopiolukija = File.OpenText(tiedostopolku))
                    {

                        while ((kopioitavarivi = kopiolukija.ReadLine()) != null)
                        {
                            
                            if (!kopioitavarivi.Contains(KerroTunnus()))
                            {
                                Console.WriteLine("Säilytettävä rivi: " + kopioitavarivi);
                                Console.WriteLine("\nKopioitiin säilytettävä rivi tiedostoon.");
                                korjattutiedosto = string.Concat(korjattutiedosto, kopioitavarivi+"\n");
                            }
                            


                        }
                        Console.WriteLine("Korjattu tiedosto: " + korjattutiedosto);
                       

                    }
                    
                   
                        File.WriteAllText(tiedostopolku, korjattutiedosto);
                        
                    



                }


            

                Console.WriteLine("\n\n\n");

            }

          catch (IOException) { Console.WriteLine("Virhe poistettaessa yhden henkilön tietoja, rekisteritiedostoa käytetään muualta."); }

        }

    }

}


