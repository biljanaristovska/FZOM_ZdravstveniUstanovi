using System;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using FZOM_Apteki.Models;
using System.Collections.Generic;

namespace FZOM_Apteki.Controllers
{
    public static class XMLSerializer
    {
        /// <summary>
        /// Метод кој врши серијализација во XML документ
        /// </summary>
        /// <param name="o"> Објектот кој треба да се серијализира</param>
        /// <returns></returns>
        public static XmlDocument Serialize(this object o)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter((Stream)memoryStream);
            xmlSerializer.Serialize((TextWriter)streamWriter, o, namespaces);
            XmlDocument xmlDocument = new XmlDocument();
            memoryStream.Position = 0L;
            xmlDocument.Load((Stream)memoryStream);
            streamWriter.Close();
            return xmlDocument;
        }

        /// <summary>
        /// Метод кој врши десеријализација од XML документ
        /// </summary>
        /// <param name="o"> Објект во кој се зачувуваат повлечените податоци</param>
        /// <param name="document"> Документот од кој се повлекуваат податоците</param>
        /// <returns></returns>
        public static object Deserialize(this object o, XmlDocument document)
        {
            XmlParserContext context = new XmlParserContext((XmlNameTable)null, (XmlNamespaceManager)null, (string)null, XmlSpace.None);
            XmlTextReader xmlTextReader = new XmlTextReader(document.OuterXml, XmlNodeType.Document, context);
            return new XmlSerializer(o.GetType()).Deserialize((XmlReader)xmlTextReader);
        }

    }
    /// <summary>
    /// Оваа класа ги содражи сите методи со кој е имплементирана целосната логика на серверска страна 
    /// </summary>
    public class AdvancedSearchController : Controller
    {
        /// <summary>
        /// Метод кој по дифолт се креира при креирање на самата класа
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Dogovori model = GettExistingPharmacy("");
            return View(model);
        }
        /// <summary>
        /// Овој метод ги враќа како резултат сите аптеки кои се совпаѓаат со критериумите за барање т.е. аргументите кои поставил корисникот.
        /// </summary>
        /// <param name="grad"> Градот кој корисникот го одбрал</param>
        /// <param name="ime"> Фраза од името на аптеката</param>
        /// <returns></returns>
        public ActionResult GetUstanovi(string grad, string ime, string dejnost)
        {
            try
            {

                //Декларираме променлива аптеки во која ги сместуваме резулатите од методот GettExistingPharmacy т.е. сите аптеки од XML-от за манипулирање со нив
                var dogovori = GettExistingPharmacy(dejnost);
                //Оваа променлива ќе ги содражи сите резулатати кои ги исполнуваат условите
                Dogovori rezDog = new Dogovori();
                foreach (var d in dogovori)
                {

                    //Ги бришеме празните места(ако ги има) од почетокот на името(краток и долг назив)
                    d.NazivNaUstanova = d.NazivNaUstanova.Trim();

                    //Со следниот for циклус проверувам дали некаде во името има повеќе од едно празно место. Ако има сите ги заменувам само со едно
                    for (int i = 0; i < d.NazivNaUstanova.Length; i++)
                    {
                        var zbor = d.NazivNaUstanova.ToCharArray();
                        if (zbor[i] == ' ' && zbor[i + 1] == ' ')
                        {
                            int j = i;
                            while (j < d.NazivNaUstanova.Length - 1)
                            {
                                zbor[j] = zbor[j + 1];
                                j++;
                            }
                            zbor[d.NazivNaUstanova.Length - 1] = ' ';
                            string str = new string(zbor);
                            str = str.Trim();
                            d.NazivNaUstanova = str;

                        }
                    }
                    //Со следната низа од if-else услови проверуваме како започува целиот назив на аптеката за да избришам соодветниот дел однапред како би го изолирале само 
                    //името на аптеката
                    if (d.NazivNaUstanova.StartsWith("ПЗУ АПТЕКА "))
                    {
                        d.NazivNaUstanova = d.NazivNaUstanova.Substring("ПЗУ АПТЕКА ".Length);
                        d.NazivNaUstanova = d.NazivNaUstanova.Trim();

                    }
                    else if (d.NazivNaUstanova.StartsWith("ПЗУ-"))
                    {
                        d.NazivNaUstanova = d.NazivNaUstanova.Substring("ПЗУ-".Length);
                        d.NazivNaUstanova = d.NazivNaUstanova.Trim();

                    }
                    else if (d.NazivNaUstanova.StartsWith("ПЗУ "))
                    {
                        d.NazivNaUstanova = d.NazivNaUstanova.Substring("ПЗУ ".Length);
                        d.NazivNaUstanova = d.NazivNaUstanova.Trim();

                    }
                    else if (d.NazivNaUstanova.StartsWith("ЈЗУ "))
                    {
                        d.NazivNaUstanova = d.NazivNaUstanova.Substring("ЈЗУ ".Length);
                        d.NazivNaUstanova = d.NazivNaUstanova.Trim();

                    }

                    if (d.NaselenoMesto.ToUpper().Contains(grad.ToUpper()) || grad == "")
                    {
                        if (ime.Length > 0)
                        {
                            if (d.NazivNaUstanova.ToLower().Contains(ime.ToLower()))
                            {
                                rezDog.Add(d);
                            }

                        }

                        else rezDog.Add(d);
                    }

                }
                //Ако нема ниту еден резулат тогаш правиме објект кој како име ќе има "Не се пронајдени резултати." и шифра="".
                if (rezDog.Count == 0)
                {
                    var newApt = new Dogovor();
                    newApt.NazivNaUstanova = "Не се пронајдени резултати.";
                    rezDog.Add(newApt);
                }
                //Враќаме json објект
                return Json(rezDog);
            }
            catch (Exception)
            {
                return Json(new object());
            }
        }



        /// <summary>
        /// Овој метод враќа резулатати кога корисникот пребарува аптека по буква.
        /// </summary>
        /// <param name="bukva">Буквата која корисникот ја одбрал при пребарување</param>
        /// <returns></returns>
        public ActionResult GetUstanovaPoBukva(string bukva)
        {

            try
            {
                //Декларираме променлива аптеки во која ги сместуваме резулатите од методот GettExistingPharmacy т.е. сите аптеки од XML-от за манипулирање со нив
                var apteki = GettExistingPharmacy("");
                //Оваа променлива ќе ги содражи сите резулатати кои ги исполнуваат условите                
                Dogovori rezApt = new Dogovori();
                foreach (var l in apteki)
                {
                    //Ако името на аптеката започнува со дадената буква
                    if (l.NazivNaUstanova.StartsWith("ПЗУ АПТЕКА "))
                    {
                        l.NazivNaUstanova = l.NazivNaUstanova.Substring("ПЗУ АПТЕКА ".Length);
                        // l.POGON_NAZIV = l.POGON_NAZIV.Substring("ПЗУ АПТЕКА ".Length);
                        l.NazivNaUstanova = l.NazivNaUstanova.Trim();
                        // l.POGON_NAZIV = l.POGON_NAZIV.Trim();
                    }
                    else if (l.NazivNaUstanova.StartsWith("ПЗУ "))
                    {
                        l.NazivNaUstanova = l.NazivNaUstanova.Substring("ПЗУ ".Length);
                        // l.POGON_NAZIV = l.POGON_NAZIV.Substring("ПЗУ ".Length);
                        l.NazivNaUstanova = l.NazivNaUstanova.Trim();
                        // l.POGON_NAZIV = l.POGON_NAZIV.Trim();
                    }
                    if (l.NazivNaUstanova.StartsWith(bukva))
                    {
                        //Бидејќи претставувањето е името заедно со градот проверуваме дали во името евентуално да не постои веќе и градот. 

                        rezApt.Add(l);

                    }

                }
                //Ако нема ниту еден резулат тогаш правиме објект кој како име ќе има "Не се пронајдени резултати." и шифра="".
                if (rezApt.Count == 0)
                {
                    var newApt = new Dogovor();
                    newApt.NazivNaUstanova = "Не се пронајдени резултати.";
                    newApt.ShifraZU = "";
                    rezApt.Add(newApt);
                }
                //Враќаме json објект
                return Json(rezApt);
            }
            catch (Exception)
            {
                return Json(new object());
            }
        }
        /// <summary>
        /// Овој метод ги враќа сите аптеки  од Xml документот како колекција од објекти
        /// </summary>
        /// <returns></returns>
        public Dogovori GettExistingPharmacy(string dejnost)
        {
            try
            {
                ViewBag.Message = "Get All Pharmacy";
                //Променлива која претстваува колекција од објекти AptekiTable
                var aptColl = new Dogovori();
                var document = new XmlDocument();

                if (dejnost == "ПЗЗ - Општа Медицина")
                {
                    document.Load(Server.MapPath("~/XML/1List.xml"));
                }
                else if (dejnost == "ПЗЗ - Стоматологија")
                {
                    document.Load(Server.MapPath("~/XML/2List.xml"));
                }
                else if (dejnost == "ПЗЗ -  Гинеколог")
                {
                    document.Load(Server.MapPath("~/XML/3List.xml"));
                }
                else if (dejnost == "Аптеки")
                {
                    document.Load(Server.MapPath("~/XML/4List.xml"));
                }
                else if (dejnost == "Специјалистичко - консултативна  заштита")
                {
                    document.Load(Server.MapPath("~/XML/5List.xml"));
                }
                else if (dejnost == "Специјалистичко - консултативна  заштита  Стоматологија ")
                {
                    document.Load(Server.MapPath("~/XML/6List.xml"));
                }
                else if (dejnost == "Лабораториски услуги (ЛУ1-1)")
                {
                    document.Load(Server.MapPath("~/XML/7List.xml"));
                }
                else if (dejnost == "Лабораториски услуги (ЛУ1-2)")
                {
                    document.Load(Server.MapPath("~/XML/8List.xml"));
                }
                else if (dejnost == "Биомедицинско потпомогнато оплодување (БПО)")
                {
                    document.Load(Server.MapPath("~/XML/9List.xml"));
                }
                else if (dejnost == "СКЗЗ-Здравствени домови (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/10List.xml"));
                }
                else if (dejnost == "СКЗЗ-Центари за јавно здравје (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/11List.xml"));
                }
                else if (dejnost == "СКЗЗ-Институти (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/12List.xml"));
                }
                else if (dejnost == "СКЗЗ-Заводи (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/13List.xml"));
                }
                else if (dejnost == "Општи болници (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/13List.xml"));
                }

                else if (dejnost == "Клинички болници (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/15List.xml"));
                }
                else if (dejnost == "Специјални болници (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/16List.xml"));
                }
                else if (dejnost == "Универзитетски клиники (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/17List.xml"));
                }
                else if (dejnost == "БЗЗ-Институти (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/18List.xml"));
                }
                else if (dejnost == "БЗЗ-Заводи (ЈЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/19List.xml"));
                }
                else if (dejnost == "БЗЗ-Кардиоваскуларна хирургија (ПЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/20List.xml"));
                }
                else if (dejnost == "БЗЗ-Офталмологија (ПЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/21List.xml"));
                }
                else if (dejnost == "БЗЗ-Природни лекувалишта (ПЗУ)")
                {
                    document.Load(Server.MapPath("~/XML/22List.xml"));
                }
                else if (dejnost == "Специјалистичко - консултативна  заштита  Дијализа ")
                {
                    document.Load(Server.MapPath("~/XML/23List.xml"));
                }
                else
                {
                    document.Load(Server.MapPath("~/XML/0List.xml"));
                }






                return (Dogovori)aptColl.Deserialize(document);
            }
            catch (Exception)
            {
                return (new Dogovori());
            }
        }



        public ActionResult GettExistingPharmacyJson()
        {
            try
            {
                ViewBag.Message = "Get All Pharmacy";
                //Променлива која претстваува колекција од објекти AptekiTable
                var aptColl = new Dogovori();
                var document = new XmlDocument();

                document.Load(Server.MapPath("~/XML/0List.xml"));
                return Json((Dogovori)aptColl.Deserialize(document));
            }
            catch (Exception)
            {
                return Json((new Dogovori()));
            }
        }



        /// <summary>
        /// Метод кој како резултат ги враќа сите информации за дадена аптека
        /// </summary>
        /// <param name="ShifraZU">Аргумент  кој е шифрата на аптеката</param>
        /// <returns></returns>
        public ActionResult GetUstanovaInfo(string ShifraZU)
        {
            try
            {
                ViewBag.Message = "Get Info";
                //Сите постоечки аптеки ги сместуваме во променлива која во циклус една по една ќе ги испитаме за да ја пронајдеме вистинската
                var newlekColl = GettExistingPharmacy("");
                var rezLek = new Dogovori();
                foreach (var l in newlekColl)
                {
                    if (l.ShifraZU == ShifraZU)
                    {
                        //Слично како што беше кажано погоре. Ги бришеме празните места на почетокот на името и повеќето(ако ги има) во средината на името на аптеката
                        l.NazivNaUstanova = l.NazivNaUstanova.Trim();
                        for (int i = 0; i < l.NazivNaUstanova.Length; i++)
                        {
                            var zbor = l.NazivNaUstanova.ToCharArray();
                            if (zbor[i] == ' ' && zbor[i + 1] == ' ')
                            {
                                int j = i;
                                while (j < l.NazivNaUstanova.Length - 1)
                                {
                                    zbor[j] = zbor[j + 1];
                                    j++;
                                }
                                zbor[l.NazivNaUstanova.Length - 1] = ' ';
                                string str = new string(zbor);
                                str = str.Trim();
                                l.NazivNaUstanova = str;

                            }
                        }
                        //Сакаме само да го претставиме името аптеката па ги бришеме ПЗУ АПТЕКА или ПЗУ од почетокот на името на аптеката
                        if (l.NazivNaUstanova.StartsWith("ПЗУ АПТЕКА "))
                        {
                            l.NazivNaUstanova = l.NazivNaUstanova.Substring("ПЗУ АПТЕКА ".Length);
                            l.NazivNaUstanova = l.NazivNaUstanova.Trim();
                        }
                        else if (l.NazivNaUstanova.StartsWith("ПЗУ "))
                        {
                            l.NazivNaUstanova = l.NazivNaUstanova.Substring("ПЗУ ".Length);
                            l.NazivNaUstanova = l.NazivNaUstanova.Trim();
                        }
                        rezLek.Add(l);
                        return Json(rezLek);
                    }
                }
                //Враќаме резултат
                return Json(new object());

            }
            catch (Exception)
            {
                return Json(new object());
            }
        }
    }
}
