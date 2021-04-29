using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WADFC.Models;

namespace WADFC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllEvents()
        {
            List<Event> allEventsModel = _context.Events.ToList();
            return View(allEventsModel);
        }

        public IActionResult Event(int id)
        {
            List<EventDetails> modelList = new List<EventDetails>();
            var query = (
              from fght in _context.Fights
              join f in _context.Fighters
              on fght.FighterAID equals f.FighterID
              join f2 in _context.Fighters
              on fght.FighterBID equals f2.FighterID
              join e in _context.Events
              on fght.EventID equals e.EventID
              where e.EventID == id
              select new
              {
                  FightID = fght.FightID,
                  EventTitle = e.EventTitle,
                  Round = fght.Round,
                  Method = fght.Method,
                  WinnerName = fght.Winner,
                  FighterAName = f.FirstName + " " + f.Surname,
                  FighterBName = f2.FirstName + " " + f2.Surname
              }).ToList();

            foreach (var item in query)
            {
                modelList.Add(new EventDetails()
                {
                    FightID = item.FightID,
                    EventTitle = item.EventTitle,
                    Round = item.Round,
                    Method = item.Method,
                    WinnerName = item.WinnerName,
                    FighterAName = item.FighterAName,
                    FighterBName = item.FighterBName
                });
            }

            if (modelList.Count < 1)
            {

                Event model = _context.Events.Find(id);

                modelList.Add(new EventDetails()
                {
                    EventTitle = model.EventTitle,
                    FighterAName = "NONE",
                });
            }

            return View(modelList);
        }

        public IActionResult AllFighters()
        {
            List<Fighter> allFightersModel = _context.Fighters.ToList();
            return View(allFightersModel);
        }

        public IActionResult Fighter(int id)
        {
            Fighter fighterModel = _context.Fighters.Find(id);
            List<FighterDetails> modelList = new List<FighterDetails>();
            var query = (
                from fights in _context.Fights
                join fighter in _context.Fighters
                on fights.FighterAID equals fighter.FighterID
                join fighter2 in _context.Fighters
                on fights.FighterBID equals fighter2.FighterID
                join e in _context.Events
                on fights.EventID equals e.EventID
                where fights.FighterAID == id || fights.FighterBID == id
                select new
                {
                    FightID = fights.FightID,
                    EventTitle = e.EventTitle,
                    Round = fights.Round,
                    Method = fights.Method,
                    WinnerName = fights.Winner,
                    FighterAName = fighter.FirstName + " " + fighter.Surname,
                    FighterBName = fighter2.FirstName + " " + fighter2.Surname
            }).ToList();

            foreach (var item in query)
            {
                modelList.Add(new FighterDetails()
                {
                    FightID = item.FightID,
                    EventTitle = item.EventTitle,
                    Round = item.Round,
                    Method = item.Method,
                    WinnerName = item.WinnerName,
                    FighterAName = item.FighterAName,
                    FighterBName = item.FighterBName
                });
            }

            if (modelList.Count < 1)
            {

                Event model = _context.Events.Find(id);

                modelList.Add(new FighterDetails()
                {
                    MainFighterName = fighterModel.FirstName + " " + fighterModel.Surname,
                    Record = fighterModel.Win + "-" + fighterModel.Loss + "-" + fighterModel.Draw + "-" + fighterModel.NoContest,
                    Characteristics = "Height: " + fighterModel.Height + "cm" + " Weight: " + fighterModel.Weight + "lbs" + " Reach: " + fighterModel.Reach,
                    Stance = fighterModel.Stance
                });
            }
            else
            {
                modelList[0].MainFighterName = fighterModel.FirstName + " " + fighterModel.Surname;
                modelList[0].Record = fighterModel.Win + "-" + fighterModel.Loss + "-" + fighterModel.Draw + "-" + fighterModel.NoContest;
                modelList[0].Characteristics = "Height: " + fighterModel.Height + "cm" + " Weight: " + fighterModel.Weight + "lbs" + " Reach: " + fighterModel.Reach;
                modelList[0].Stance = fighterModel.Stance;

                return View(modelList);
            }

            modelList[0].EventTitle = "NONE";

            return View(modelList);
        }

        [HttpGet]
        public IActionResult SearchEvent(String searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var events = from e in _context.Events
                             where e.EventTitle.Contains(searchString)
                             select e;

                List<Event> model = events.ToList();


                ViewData["SearchString"] = searchString;

                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult SearchFighter(String searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                string firstWord = null;
                for (int i = 0; i < searchString.Length; i++)
                {
                    if (searchString[i] == ' ')
                    {
                        firstWord = searchString.Substring(0, i);           // TOOK OFF i-1
                    }
                }

                if (!string.IsNullOrEmpty(firstWord))
                {
                    var fighters = from f in _context.Fighters
                                   where f.FirstName.Contains(firstWord) || f.Surname.Contains(firstWord)
                                   select f;

                    List<Fighter> model = fighters.ToList();

                    ViewData["SearchString"] = searchString;

                    return View(model);
                }
                else
                {
                    var fighters = from f in _context.Fighters
                                   where f.FirstName.Contains(searchString) || f.Surname.Contains(searchString)
                                   select f;

                    List<Fighter> model = fighters.ToList();

                    ViewData["SearchString"] = searchString;

                    return View(model);
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdvancedSearch(AdvancedSearch model)
        {

            var events = from e in _context.Events
                         select e;

            var fighters = from f in _context.Fighters
                           select f;

            var fights = from fght in _context.Fights
                         select fght;

            List<string> searchFor = new List<string>();
            searchFor.Add("Event");
            searchFor.Add("Fighter");

            List<string> selectionsForEvent = new List<string>();
            selectionsForEvent.Add("Event Title");
            selectionsForEvent.Add("Fighter Name");

            List<string> selectionsForFighter = new List<string>();
            selectionsForFighter.Add("Fighter Name");
            selectionsForFighter.Add("Record");

            ViewData["SearchForSelection"] = searchFor;
            ViewData["SelectionForEvent"] = selectionsForEvent;
            ViewData["SelectionForFighter"] = selectionsForFighter;

            Search searchModel = new Search();

            if (!String.IsNullOrEmpty(model.EventTitleString))
            {
                /*Events based on events name*/
                events = events.Where(e => e.EventTitle.Contains(model.EventTitleString));

                List<Event> returnModel = events.ToList();


                ViewData["SearchString"] = model.EventTitleString;

                searchModel.eventsModel = returnModel;

            }
            else if (!String.IsNullOrEmpty(model.FighterNameString))
            {
                /*All events based on fighters name*/
                string firstWord = null;
                for (int i = 0; i < model.FighterNameString.Length; i++)
                {
                    if (model.FighterNameString[i] == ' ')
                    {
                        firstWord = model.FighterNameString.Substring(0, i);                    // TOOK OFF i - 1
                    }
                }

                if (!string.IsNullOrEmpty(firstWord))
                {

                    fighters = fighters.Where(f => f.FirstName.Contains(firstWord) || f.Surname.Contains(firstWord));

                    List<int> fighterIDs = new List<int>();
                    foreach (var item in fighters)
                    {
                        fighterIDs.Add(item.FighterID);
                    }

                    List<List<Fight>> listOFFightLists = new List<List<Fight>>();
                    foreach (int id in fighterIDs)
                    {
                        var newFights = fights.Where(f => f.FighterAID.Equals(id) || f.FighterBID.Equals(id));
                        listOFFightLists.Add(newFights.ToList());
                    }

                    List<int> eventList = new List<int>();
                    foreach (List<Fight> fightList in listOFFightLists)
                    {
                        foreach (Fight fight in fightList)
                        {
                            eventList.Add(fight.EventID.Value);
                        }
                    }

                    List<List<Event>> listOFEvents = new List<List<Event>>();
                    foreach (int eventID in eventList)
                    {
                        var newEvents = events.Where(e => e.EventID.Equals(eventID));
                        listOFEvents.Add(newEvents.ToList());

                    }


                    List<Event> returnModel = new List<Event>();

                    foreach (List<Event> listEvent in listOFEvents)
                    {
                        foreach (Event ev in listEvent)
                        {
                            returnModel.Add(ev);
                        }
                    }

                    ViewData["SearchString"] = model.FighterNameStringFighter;

                    searchModel.eventsModel = returnModel;

                }
                else
                {
                    fighters = fighters.Where(f => f.FirstName.Contains(model.FighterNameString) || f.Surname.Contains(model.FighterNameString));

                    List<int> fighterIDs = new List<int>();
                    foreach (var item in fighters)
                    {
                        fighterIDs.Add(item.FighterID);
                    }

                    List<List<Fight>> listOFFightLists = new List<List<Fight>>();
                    foreach (int id in fighterIDs)
                    {
                        var newFights = fights.Where(f => f.FighterAID.Equals(id) || f.FighterBID.Equals(id));
                        listOFFightLists.Add(newFights.ToList());
                    }

                    List<int> eventList = new List<int>();
                    foreach (List<Fight> fightList in listOFFightLists)
                    {
                        foreach (Fight fight in fightList)
                        {
                            eventList.Add(fight.EventID.Value);
                        }
                    }

                    List<List<Event>> listOFEvents = new List<List<Event>>();
                    foreach (int eventID in eventList)
                    {
                        var newEvents = events.Where(e => e.EventID.Equals(eventID));
                        listOFEvents.Add(newEvents.ToList());

                    }


                    List<Event> returnModel = new List<Event>();

                    foreach (List<Event> listEvent in listOFEvents)
                    {
                        foreach (Event ev in listEvent)
                        {
                            returnModel.Add(ev);
                        }
                    }

                    ViewData["SearchString"] = model.FighterNameStringFighter;

                    searchModel.eventsModel = returnModel;
                }
            }
            else if (!String.IsNullOrEmpty(model.FighterNameStringFighter))
            {   /*All Fighters based on fighters name*/
                string firstWord = null;
                for (int i = 0; i < model.FighterNameStringFighter.Length; i++)
                {
                    if (model.FighterNameStringFighter[i] == ' ')
                    {
                        firstWord = model.FighterNameStringFighter.Substring(0, i);                 // TOOK OFF i - 1
                    }
                }

                if (!string.IsNullOrEmpty(firstWord))
                {

                    fighters = fighters.Where(f => f.FirstName.Contains(firstWord) || f.Surname.Contains(firstWord));

                    List<Fighter> returnModel = fighters.ToList();

                    ViewData["SearchString"] = model.FighterNameStringFighter;

                    searchModel.fightersModel = returnModel;
                }
                else
                {
                    fighters = fighters.Where(f => f.FirstName.Contains(model.FighterNameStringFighter) || f.Surname.Contains(model.FighterNameStringFighter));

                    List<Fighter> returnModel = fighters.ToList();

                    ViewData["SearchString"] = model.FighterNameStringFighter;

                    searchModel.fightersModel = returnModel;
                }
            }
            /*1 - 0 - 0 - 0*/
            else if (model.Win >= 0 && model.Loss < 0 && model.Draw < 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 1 - 0 - 0*/
            else if (model.Win < 0 && model.Loss >= 0 && model.Draw < 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Loss.Equals(model.Loss));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 0 - 1 - 0*/
            else if (model.Win < 0 && model.Loss < 0 && model.Draw >= 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Draw.Equals(model.Draw));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 0 - 0 - 1*/
            else if (model.Win < 0 && model.Loss < 0 && model.Draw < 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 1 - 0 - 0*/
            else if (model.Win >= 0 && model.Loss >= 0 && model.Draw < 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.Loss.Equals(model.Loss));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 1 - 1 - 0*/
            else if (model.Win < 0 && model.Loss >= 0 && model.Draw >= 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Loss.Equals(model.Loss) && f.Draw.Equals(model.Draw));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 0 - 1 - 1*/
            else if (model.Win < 0 && model.Loss < 0 && model.Draw >= 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Draw.Equals(model.Draw) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 0 - 1 - 0*/
            else if (model.Win >= 0 && model.Loss < 0 && model.Draw >= 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.Draw.Equals(model.Draw));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 1 - 0 - 1*/
            else if (model.Win < 0 && model.Loss >= 0 && model.Draw < 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Loss.Equals(model.Loss) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 0 - 0 - 1*/
            else if (model.Win >= 0 && model.Loss < 0 && model.Draw < 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 1 - 1 - 0*/
            else if (model.Win >= 0 && model.Loss >= 0 && model.Draw >= 0 && model.NoContest < 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.Loss.Equals(model.Loss) && f.Draw.Equals(model.Draw));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 1 - 1 - 1*/
            else if (model.Win >= 0 && model.Loss >= 0 && model.Draw >= 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.Loss.Equals(model.Loss) && f.Draw.Equals(model.Draw) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 0 - 1 - 1*/
            else if (model.Win >= 0 && model.Loss < 0 && model.Draw >= 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.Draw.Equals(model.Draw) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*1 - 1 - 0 - 1*/
            else if (model.Win >= 0 && model.Loss >= 0 && model.Draw < 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Win.Equals(model.Win) && f.Loss.Equals(model.Loss) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            /*0 - 1 - 1 - 1*/
            else if (model.Win < 0 && model.Loss >= 0 && model.Draw >= 0 && model.NoContest >= 0)
            {
                fighters = fighters.Where(f => f.Loss.Equals(model.Loss) && f.Draw.Equals(model.Draw) && f.NoContest.Equals(model.NoContest));

                List<Fighter> returnModel = fighters.ToList();

                ViewData["SearchString"] = model.FighterNameStringFighter;

                searchModel.fightersModel = returnModel;
            }
            
            return View(searchModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
