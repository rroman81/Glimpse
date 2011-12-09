﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Glimpse.Core.Extensibility;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class Test
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public partial class HomeController : Controller
    {
        //
        // GET: /Home/
        MusicStoreEntities storeDB = new MusicStoreEntities();

        [HttpPost]
        public virtual ActionResult JsonTest(Test test)
        {
            return Json(new {Message = test.Name + " is number " + test.Number, Time = DateTime.Now});
        }

        [Authorize]
        public virtual ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);

            Trace.Write("Got top 5 albums");

            GlimpseTrace.Info("This is info from Glimpse");
            GlimpseTrace.Warn("This is warn from Glimpse at {0}", DateTime.Now);
            GlimpseTrace.Error("This is error from {0}", GetType());
            GlimpseTrace.Fail("This is Fail from Glimpse");

            GlimpseTimer.Moment("Custom timming event from HomeController", "Custom");

            Trace.TraceWarning("Test TraceWarning;");
            Trace.TraceError("Test TraceError;");
            Trace.TraceInformation("Test TraceInformation;"); 



            //GlimpseTimer.Moment("A Moment", "Other", "This is just a moment in time.");


            TempData["Test"] = "A bit of temp";
            
            return View(albums);
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        public string FilterTest()
        {
            return
                "Simple Page to test Glimpse without html tags";
        }

        public virtual ActionResult News()
        {
            var views = new[]{"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight"};

            var randomIndex = new Random().Next(0, views.Count());

            Trace.Write("Randomly selected story number " + randomIndex);

            return PartialView(views[randomIndex]);
        }

        public virtual ActionResult Test()
        {
            return View();
        }
    }
}