using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NANB_Enjin;
using System.Net;
using TaalToets;


namespace Snuif
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<DC_Artikel> a = Datalayer.KryOuArtikels();
            //string s = "Die Oos-Kaapse hooggeregshof sal hierdie week ? dringende aansoek aanhoor rakende die R699-motorverkoopskema, het die Business Times Sondag berig.";
            //string s = "The remains of up to 196 people from the MH17 crash in east Ukraine are loaded on to sealed refrigerated rail wagons, but their destination is unclear. ";
            //string s = "n though the duck weighs 1 tonne, and was sitting on a 10 tonne metal platform lashed to the riverbed with steel wires, it was easily dislodged by the storm. \"The duck flopped over and was flushed away really quickly by the torrential";
            //string s = "After years of dragging its heels and it even ending up in court, it seems that government is still not going to implement the Language Act this year, Dr. Pieter Mulder, FF Plus Leader and the ";
            //string s = "Suid-Afrika se staatsbeheerde ondernemings laat veel te wense oor en dien as bewys dat die staat hom nie moet inmeng in die ekonomie nie, sê adv. Anton Alberts, die VF Plus se parlementêre";

            //char[] b = Properties.Settings.Default.Bstring.ToArray();
            //b = b;
            
            
            
            ////TaalEval taalEval = new TaalEval();
            ////TaalEval.Eval(s);

            //string url = "http://dievoorblad.co.za/index.php/afr/joomla-pages-iii/category-list/68-nuutste-nuus/1650-skotte-verdien-lof";
            //string opsies = "[{Naam:\"index.php/afr/joomla-pages-iii/category-list/68-nuutste-nuus/\", ID:4}]";
            //Dievoorblad dievoorblad = new Dievoorblad(new Uri("http://dievoorblad.co.za/index.php/afr/template/nuus-uit-die-verlede/nuutste-nuus/"), opsies);
            //DC_Artikel artikel = dievoorblad.Snuifblad(new Uri(url));

            //string url = "http://www.dievryburger.co.za/2014/09/geld-geskenk-vir-graad/";
            ////WebClient webClient = new WebClient();
            ////webClient.Encoding = System.Text.Encoding.UTF8;
            ////string html = webClient.DownloadString(url);
            ////Console.Write("Einde");

            //// int[] ids = Utils.KryBitIDs(29);
            //List<DC_Artikel> artikels = new List<DC_Artikel>();
            //string opsies = "";// @"[{Naam:""p="", ID:1}]";
            ////string opsies = @"[{Naam:""suid-afrika/nuus/"", ID:1},{Naam: ""wereld/nuus/"",ID:1},{Naam: ""sake/"",ID:2},{Naam: ""sport/"",ID:3}]";
            //string opsies = "[{Naam: \"nuus/{0}\",ID:5} ,{Naam: \"vermaak/{0}\",ID:5},{Naam: \"sake/{0}\",ID:2},{Naam: \"sport/{0}\",ID:3}]";
            //Media24 media24 = new Media24(new Uri("http://www.netwerk24.com/"), opsies);
            //DC_Artikel artikel = media24.Snuifblad(new Uri(url));
            //string[] ss = artikel.Beskrywing.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string s in ss)
            //{
            //    foreach (char c in s.ToCharArray())
            //    {

            //    }
            //}

            //TaalEval.Eval(artikel.Beskrywing);
           // string opsies = "[{Patroon:\"^/blog/nuus/.+\", ID:1}]";// ,{Naam: "vermaak",ID:5},{Naam: "sake",ID:2},{Naam: "sport",ID:3}]"
            //Maroela maroela = new Maroela(new Uri(@"http://maroelamedia.co.za/kategorie/nuus/"), opsies);
            ////    //List<Uri> urls = maroela.SnuifVoorblad();
            //Praag praag = new Praag(new Uri(@"http://praag.co.za/?cat=5"), opsies);
            //List<Uri> urls = praag.SnuifVoorblad();

            //////    //http://rugby.maroelamedia.co.za/kategorie/nuus/
            ////////http://rugby.maroelamedia.co.za/2014/05/10/brumbies-onverdiende-wenners-canberra/
            //    string opsies = @"[{Patroon:""^/20\\d\\d/\\d\\d/\\d\\d/.+"", ID:3}]";// ,{Naam: "vermaak",ID:5},{Naam: "sake",ID:2},{Naam: "sport",ID:3}]"
            //    Maroela maroela = new Maroela(new Uri(@"http://rugby.maroelamedia.co.za/kategorie/nuus/"), opsies);
            ////    //List<Uri> urls = maroela.SnuifVoorblad();

            //foreach (Uri url in urls)
            //{
            //   DC_Artikel artikel = praag.Snuifblad(url);
            //    artikels.Add(artikel);
              //  DC_Artikel artikel = maroela.Snuifblad(new Uri("http://maroelamedia.co.za/blog/nuus/suid-afrika/beskerm-zuma-teen-vernedering-vra-anc/"));
            //}

            //######  Media24   #######

            string url = "http://www.netwerk24.com/sport/";
            string opsies = @"[{Patroon:""^/netwerk24/sport/[a-z]+/{0}.+"", ID:3}]";
                //"[{Naam: \"nuus/{0}\",ID:5} ,{Naam: \"vermaak/{0}\",ID:5},{Naam: \"sake/{0}\",ID:2},{Naam: \"sport/{0}\",ID:3}]";
            Media24 media24 = new Media24(new Uri(url), opsies);
            List<Uri> uris = media24.SnuifVoorblad();

            //######  vryburger  #######

            //http://www.dievryburger.co.za/2015/01/nuwighede-in-elektronika-bekendgestel/

            //string url = "http://www.dievryburger.co.za/category/nuus/";
            //string opsies = "[{Naam:\"category-nuus\", ID:1},{Naam:\"category-buitelandse-nuus\", ID:1},{Naam:\"category-ekonomie\", ID:2}]";
            //Vryburger vryburger = new Vryburger(new Uri(url),opsies);

            //DC_Artikel artikel = vryburger.Snuifblad(new Uri("http://www.dievryburger.co.za/2015/01/nuwighede-in-elektronika-bekendgestel/"));
            //List<Uri> uris = vryburger.SnuifVoorblad();
            //List<DC_Artikel> a = new List<DC_Artikel>();
            //foreach (Uri uri in uris)
            //{
            //    DC_Artikel artikel = vryburger.Snuifblad(uri);
            //    a.Add(artikel);
            //}
            
            //###### Maroela ######
            //List<DC_Bron> bronne = Datalayer.KryBronneSnif();
            //bronne = bronne.Where(b => b.ID == 5).ToList();
            //bronne[0].BronUrls = bronne[0].BronUrls.Where(b => b.ID == 17).ToList();

            //Maroela maroela = new Maroela(new Uri(bronne[0].BronUrls[0].Url),bronne[0].BronUrls[0].Opsies);

            //DC_Artikel artikel = maroela.Snuifblad(new Uri(@"http://rugby.maroelamedia.co.za/2014/09/28/9099/"));


            //string opsies = "[{Naam:\"category-nuus\", ID:1},{Naam:\"category-buitelandse-nuus\", ID:1}]";
            //Vryburger vryBurger = new Vryburger(new Uri("http://www.dievryburger.co.za/category/nuus/"), opsies);
            //DC_Artikel artikel = vryBurger.Snuifblad(new Uri(url));

            //####### Koerant ######
            //KoerantCoZa koerant = new KoerantCoZa(new Uri("http://www.koerant.co.za/nuus/"), null);
            //List<Uri> uris = koerant.SnuifVoorblad();
            //List<DC_Artikel> a = new List<DC_Artikel>();
            //foreach (Uri uri in uris)
            //{
            //    DC_Artikel artikel = koerant.Snuifblad(uri);
            //    a.Add(artikel);
            //}

            ////##### Republkein #######
            //string url = "http://republikein.com.na/argief/daagliks/{0:yyyyMMdd}";
            //string opsies = "[{Naam:\"nuus\", ID:1},{Naam:\"internasionaal\", ID:1},{Naam:\"sakenuus\", ID:2},{Naam:\"afrika\", ID:1},{Naam:\"ekonomie\", ID:2},{Naam:\"sport\", ID:3},{Naam:\"landbou\",ID:6}]";

            //Republikein republikein = new Republikein(url, opsies);
            //List<Uri> uris = republikein.SnuifVoorblad();
            //List<DC_Artikel> a = new List<DC_Artikel>();

            //foreach (Uri uri in uris)
            //{
            //    DC_Artikel artikel = republikein.Snuifblad(uri);
            //    a.Add(artikel);
            //}

            ////###### Landbou ######
            //string url = "http://landbou.com/nuus/";
            //Landbou landbou = new Landbou(new Uri(url));
            //List<Uri> uris = landbou.SnuifVoorblad();
            //List<DC_Artikel> a = new List<DC_Artikel>();
            //foreach (Uri uri in uris)
            //{
            //    DC_Artikel artikel = landbou.Snuifblad(uri);
            //    a.Add(artikel);
            //}

            ////###### Praag ######
            //string url = @"http://praag.co.za/?cat=5";
            //Praag praag = new Praag(new Uri(url),"");
            //List<Uri> uris = praag.SnuifVoorblad();
            //List<DC_Artikel> a = new List<DC_Artikel>();
            //foreach (Uri uri in uris)
            //{
            //    DC_Artikel artikel = praag.Snuifblad(uri);
            //    a.Add(artikel);
            //}

        }
    }
}

