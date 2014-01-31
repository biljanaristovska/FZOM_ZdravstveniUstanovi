using System;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using FZOM_Apteki.Models;
using System.Collections.Generic;

namespace FZOM_Apteki.Controllers
{
    /// <summary>
    /// Класа која опфаќа два методи за серијализција и десеријализција од и во XML
    /// </summary>
    
    /// <summary>
    /// Оваа класа ги содражи сите методи со кој е имплементирана целосната логика на серверска страна 
    /// </summary>
    public class HomeController : Controller
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



        public ActionResult GettExistingPharmacyJson(string dejnost)
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
                return Json((Dogovori)aptColl.Deserialize(document));
            }
            catch (Exception)
            {
                return Json((new Dogovori()));
            }
        }

       
    }
}
