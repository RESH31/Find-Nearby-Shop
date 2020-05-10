using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TSL__Quiz.Models;
using PagedList;
using PagedList.Mvc;
using TSL__Quiz.Filter;

namespace TSL__Quiz.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }




        [Access]
        public ActionResult Service(string option, double latitude, double longitude, int? page, double searchkey=-1)
        {
            if(latitude==-1&&longitude==-1)
            {
                ViewData["errormsg"] = "Geolocation is not supported by this browser.";
                return View();
            }
            else
            {
                LocationDetails location = new LocationDetails();
                location.Latitude = Math.Round((latitude), 6);
                location.Longitude = Math.Round((longitude), 6);
                IEnumerable<ProductDetail> lists = null;
                IEnumerable<ProductDetail> querylist = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://appsbytsl.com/API/V1/Nearby/");
                    if (searchkey == -1)
                    {
                        var responseTask = client.GetAsync(option + "/" + location.Key + "/" + location.Latitude + "/" + location.Longitude + "/" + location.Distance);
                        responseTask.Wait();
                        //To store result of web api response.  
                        var result = responseTask.Result;
                        //If success received  
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<IList<ProductDetail>>();
                            readTask.Wait();

                            lists = readTask.Result;
                            querylist = lists;
                            

                        }
                        else
                        {
                            //Error response received   
                            querylist = Enumerable.Empty<ProductDetail>();
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }

                    }

                    else
                    {
                        var responseTask = client.GetAsync(option + "/" + location.Key + "/" + location.Latitude + "/" + location.Longitude + "/" + searchkey);
                        responseTask.Wait();
                        //To store result of web api response.  
                        var result = responseTask.Result;

                        //If success received  
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<IList<ProductDetail>>();
                            readTask.Wait();

                            lists = readTask.Result;
                            querylist = lists.Where(list => list.Distance <= searchkey);
                        }
                        else
                        {
                            //Error response received   
                            querylist = Enumerable.Empty<ProductDetail>();
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }

                    if(querylist.Count()==0)
                    {
                        ViewData["msg"] = "No List to display. NearBy API not found.";
                    }

                }
                ViewData["search"] = searchkey;
                ViewData["option"] = option;
                ViewData["latitude"] = location.Latitude;
                ViewData["longitude"] = location.Longitude;
                return View(querylist.ToPagedList(page ?? 1, 3));
            }
            
        }
    }
}