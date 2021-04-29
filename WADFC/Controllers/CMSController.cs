using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WADFC.Models;

namespace WADFC.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CMSController : Controller
    {
        private readonly ILogger<CMSController> _logger;

        private readonly ApplicationDbContext _context;

        public CMSController(ILogger<CMSController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Event> allEventsModel = _context.Events.ToList();

            return View(allEventsModel);
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEvent(EventForm model)
        {
            if (ModelState.IsValid)
            {

                Event newEvent = new Event
                {
                    EventTitle = model.EventTitle,
                    Location = model.Location,
                    EventImage = model.EventImage,
                    Date = DateTime.Now,
                    // Date = model.Date, // TODO: Check how to implement this
                    Completed = "Y"
                };
                _context.Add(newEvent);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdateEvent(int id)
        {
            Event model = _context.Events.Find(id);
            EventForm formModel = new EventForm
            {
                EventID = model.EventID,
                EventTitle = model.EventTitle,
                Location = model.Location,
                EventImage = model.EventImage,
                Date = model.Date
            };
            return View(formModel);
        }

        [HttpPost]
        public IActionResult UpdateEvent(EventForm model)
        {
            if (ModelState.IsValid)
            {
                Event editEvent = new Event
                {
                    EventID = model.EventID,
                    EventTitle = model.EventTitle,
                    Location = model.Location,
                    EventImage = model.EventImage,
                    Date = model.Date,
                    Completed = "Y"
                };
                _context.Events.Update(editEvent);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AllFights(int id)
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
                    EventID = id,
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
                    EventID = id,
                    EventTitle = model.EventTitle,
                    FighterAName = "NONE",
                    FighterBName = "NONE"
                });
            }

            return View(modelList);
        }

        [HttpGet]
        public IActionResult AllFighters()
        {
            List<Fighter> allFightersModel = _context.Fighters.ToList();
            return View(allFightersModel);
        }

        [HttpGet]
        public IActionResult AddFighter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFighter(FighterForm model)
        {
            if (ModelState.IsValid)
            {
                Fighter newFighter = new Fighter
                {
                    FirstName = model.FirstName,
                    Surname = model.Surname,
                    DOB = DateTime.Now,
                    // DOB = model.Date, // TODO: Check how to implement this
                    Height = model.Height,
                    Weight = model.Weight,
                    Reach = model.Reach,
                    Win = model.Win,
                    Loss = model.Loss,
                    Draw = model.Draw,
                    NoContest = model.NoContest,
                    Stance = "\"" + model.Stance + "\""
                };
                _context.Add(newFighter);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditFighter(int id)
        {
            Fighter model = _context.Fighters.Find(id);
            FighterForm formModel = new FighterForm
            {
                FighterID = model.FighterID,
                FirstName = model.FirstName,
                Surname = model.Surname,
                Win = model.Win,
                Loss = model.Loss,
                Draw = model.Draw,
                NoContest = model.NoContest,
                Height = model.Height,
                Weight = model.Weight,
                Reach = model.Reach,
                Stance = model.Stance, 
                DOB = model.DOB
            };
            return View(formModel);
        }

        [HttpPost]
        public IActionResult EditFighter(FighterForm model)
        {
            if (ModelState.IsValid)
            {
                Fighter editFighter = new Fighter
                {
                    FighterID = model.FighterID,
                    FirstName = model.FirstName,
                    Surname = model.Surname,
                    Win = model.Win,
                    Loss = model.Loss,
                    Draw = model.Draw,
                    NoContest = model.NoContest,
                    Height = model.Height,
                    Weight = model.Weight,
                    Reach = model.Reach,
                    Stance = "\"" + model.Stance + "\"",
                    DOB = model.DOB
                };
                _context.Fighters.Update(editFighter);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddFight(int id)
        {
            Event model = _context.Events.Find(id);
            EventFighters eventFighters = new EventFighters
            {

                EventID = model.EventID,
                FightersList = _context.Fighters.ToList(),
                NewFightForm = new FightForm()
            };

            eventFighters.NewFightForm.EventID = id;
            return View(eventFighters);
        }

        [HttpPost]
        public IActionResult AddFight(EventFighters model, int id)
        {
            model.EventID = id;
            if (ModelState.IsValid)
            {
                if (model.NewFightForm.FighterAID != model.NewFightForm.FighterBID)
                {
                    Fight newFight = new Fight
                    {
                        EventID = model.EventID,
                        FighterAID = model.NewFightForm.FighterAID,
                        FighterBID = model.NewFightForm.FighterBID,
                        Division = model.NewFightForm.Division,
                        Winner = model.NewFightForm.Winner,
                        Method = model.NewFightForm.Method,
                        Round = model.NewFightForm.Round
                    };
                    _context.Add(newFight);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            return AddFight(id);
        }

        [HttpGet]
        public IActionResult UpdateFight(int id)
        {

            Fight fightModel = _context.Fights.Find(id);
            Event eventModel = _context.Events.Find(fightModel.EventID);

            FightDetails fightDetails = new FightDetails
            {
                FightID = id,
                EventID = eventModel.EventID,
                Winner = fightModel.Winner,
                Round = fightModel.Round,
                Method = fightModel.Method,
                Divisions = fightModel.Division,
                FighterA = _context.Fighters.Find(fightModel.FighterAID),
                FighterB = _context.Fighters.Find(fightModel.FighterBID),
                FightersList = _context.Fighters.ToList(),
                NewFightForm = new FightForm()
            };

            fightDetails.NewFightForm.EventID = eventModel.EventID;
            fightDetails.NewFightForm.FighterAID = fightModel.FighterA.FighterID;
            fightDetails.NewFightForm.FighterBID = fightModel.FighterB.FighterID;
            fightDetails.NewFightForm.FightID = id;
            fightDetails.NewFightForm.Method = fightModel.Method;
            fightDetails.NewFightForm.Round = fightModel.Round;
            fightDetails.NewFightForm.Winner = fightModel.Winner;
            fightDetails.NewFightForm.Division = fightModel.Division;

            return View(fightDetails);
        }

        [HttpPost]
        public IActionResult UpdateFight(FightDetails model, int id, int id2)
        {
            model.EventID = id;
            model.FightID = id2;
            if (ModelState.IsValid)
            {
                if (model.NewFightForm.FighterAID != model.NewFightForm.FighterBID)
                {
                    Fight editFight = new Fight
                    {
                        FightID = model.FightID,
                        EventID = model.EventID,
                        FighterAID = model.NewFightForm.FighterAID,
                        FighterBID = model.NewFightForm.FighterBID,
                        Division = model.NewFightForm.Division,
                        Winner = model.NewFightForm.Winner,
                        Method = model.NewFightForm.Method,
                        Round = model.NewFightForm.Round
                    };
                    _context.Fights.Update(editFight);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return UpdateFight(id);
        }

        [HttpGet]
        public IActionResult DeleteFight(int id)
        {
            Fight fightModel = _context.Fights.Find(id);

            FightDetails fightDetails = new FightDetails
            {
                FightID = id,
                Winner = fightModel.Winner,
                Round = fightModel.Round,
                Method = fightModel.Method,
                Divisions = fightModel.Division,
                FighterA = _context.Fighters.Find(fightModel.FighterAID),
                FighterB = _context.Fighters.Find(fightModel.FighterBID),
                FightersList = _context.Fighters.ToList(),
            };

            return View(fightDetails);
        }

        [HttpPost]
        public IActionResult DeleteFight(FightDetails model)
        {
            Fight fight = _context.Fights.Find(model.FightID);
            _context.Fights.Remove(fight);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteEvent(int id)
        {

            Event model = _context.Events.Find(id);
            FullEventDetails eventModel = new FullEventDetails();
            eventModel.EventInfo = model;
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
                eventModel.EventDetails.Add(new EventDetails()
                {
                    EventID = id,
                    FightID = item.FightID,
                    EventTitle = item.EventTitle,
                    Round = item.Round,
                    Method = item.Method,
                    WinnerName = item.WinnerName,
                    FighterAName = item.FighterAName,
                    FighterBName = item.FighterBName
                });

            }
            return View(eventModel);
        }

        [HttpPost]
        public IActionResult DeleteEvent(Event model)
        {
            // Fight fight = _context.Fights.Find(model.FightID);
            _context.Events.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
