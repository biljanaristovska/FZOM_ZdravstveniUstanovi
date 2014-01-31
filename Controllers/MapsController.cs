using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FZOM_Apteki.Models;
using System.Xml;

namespace FZOM_Apteki.Controllers
{
    public class MapsController : Controller
    {
        //
        // GET: /Maps/
        public ActionResult Index()
        {
            return View();
        }

        public Dogovori GetDogovoriByDejnost(string dejnost)
        {
            Dogovori dogovori = new Dogovori();
            var doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XML/" + dejnost + "List.xml"));
            return (Dogovori)dogovori.Deserialize(doc);
        }

        public ActionResult SetStatus(string latitude, string longitude, string dejnost, string id, string status)
        {
            bool isSaved = true;
            try
            {
            Dogovori dogovori = GetDogovoriByDejnost(dejnost);

            foreach (Dogovor d in dogovori)
            {
                if (d.ShifraZU == id)
                {
                    d.Latitude = latitude;
                    d.Longitude = longitude;
                    d.Status = status;
                }

            }
            XmlDocument doc = dogovori.Serialize(); // се серијализибира објектот во документ 
            
                doc.Save(Server.MapPath("~/XML/" + dejnost + "List.xml"));
            }
            catch
            {
                isSaved = false;
            }

            return Json(isSaved);

        }


        public ActionResult ChangeCoordinates(string latitude, string longitude, string dejnost, string id, string status)
        {

            Dogovori dogovori = GetDogovoriByDejnost(dejnost);
            foreach (Dogovor d in dogovori)
            {
                if (d.ShifraZU == id)
                {
                    d.Latitude = latitude;
                    d.Longitude = longitude;
                    d.Status = status;
                }

            }
            XmlDocument doc = dogovori.Serialize(); // се серијализибира објектот во документ 


            doc.Save(Server.MapPath("~/XML/" + dejnost + "List.xml"));


            return Json(true);
        }


        public ActionResult MergeXML() {

            Dogovori result = new Dogovori();

            for (int i = 1; i <= 23; i++)
            {
                                
                Dogovori temp = GetDogovoriByDejnost(i.ToString());

                foreach (Dogovor d in temp)
                {
                    result.Add(d);
                }
            }
            XmlDocument doc = result.Serialize(); // од објекти во XML
            doc.Save(Server.MapPath("~/XML/0List.xml"));
            
            return View();
        }


        public ActionResult ValidateAll(string dejnost) {

            Dogovori dogovori = GetDogovoriByDejnost(dejnost);
            foreach (Dogovor d in dogovori) {
                if (d.Status == "1") {
                    d.Status = "2"; 
                }
            }
            XmlDocument doc = dogovori.Serialize();
            doc.Save(Server.MapPath("~/XML/" + dejnost + "List.xml"));


            return Json(true);
        }


        public ActionResult ShowFacilities(string vrednost)
        {
            try
            {
                Dogovori facilities = GetDogovoriByDejnost(vrednost);

                foreach (var f in facilities)
                {

                    if (f.Adresa.StartsWith("УЛ."))
                    {
                        f.Adresa = f.Adresa.Substring("УЛ.".Length);

                    }
                    else if (f.Adresa.StartsWith("УЛ "))
                    {
                        f.Adresa = f.Adresa.Substring("УЛ ".Length);

                    }
                    else if (f.Adresa.StartsWith("УЛ. "))
                    {
                        f.Adresa = f.Adresa.Substring("УЛ. ".Length);

                    }
                    else if (f.Adresa.StartsWith("Ул. "))
                    {
                        f.Adresa = f.Adresa.Substring("Ул. ".Length);

                    }
                    else if (f.Adresa.StartsWith("Ул "))
                    {
                        f.Adresa = f.Adresa.Substring("Ул ".Length);

                    }

                    if (f.Adresa.Contains("БР "))
                    {
                        int position = f.Adresa.IndexOf("БР ");
                        f.Adresa = f.Adresa.Substring(0, position - 1);
                    }

                    else if (f.Adresa.Contains("БР."))
                    {
                        int position = f.Adresa.IndexOf("БР.");
                        f.Adresa = f.Adresa.Substring(0, position - 1);
                    }

                    if (f.NaselenoMesto.Contains("СКОПЈЕ")) f.NaselenoMesto = "СКОПЈЕ";

                    f.Adresa = f.Adresa + " " + f.NaselenoMesto + ", Македонија";
                }

                return Json(facilities);
            }
            catch
            {
                return Json(new object());
            }


        }

    }
}
