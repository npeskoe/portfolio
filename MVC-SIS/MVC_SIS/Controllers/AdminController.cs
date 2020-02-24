using Exercises.Models.Data;
using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpPost]
        public ActionResult AddMajor(Major major)
        {

            if (ModelState.IsValidField("Major"))
            {
                if (string.IsNullOrEmpty(major.MajorName))
                {
                    ModelState.AddModelError("Major", "Major Name Cannot Be Blank!");
                }
                else if (major.MajorName.Any(char.IsDigit))
                {
                    ModelState.AddModelError("Major", "Major Cannot Contain Numbers!");
                }
                else
                {
                    MajorRepository.Add(major.MajorName);

                    return RedirectToAction("Majors");
                }

                return View(major);
            }
            return View(major);
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult EditMajor(Major major)
        {
            if (ModelState.IsValidField("Major"))
            {
                if (string.IsNullOrEmpty(major.MajorName))
                {
                    ModelState.AddModelError("Major", "Major Name Cannot Be Blank!");
                }
                else if (major.MajorName.Any(char.IsDigit))
                {
                    ModelState.AddModelError("Major", "Major Cannot Contain Numbers!");
                }
                else
                {
                    MajorRepository.Edit(major);
                    return RedirectToAction("Majors");
                }

                return View(major);
            }
            return View(major);
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult States()
        {
            var stateModel = StateRepository.GetAll();

            return View(stateModel.ToList());
        }
        
        [HttpGet]
        public ActionResult AddState()
        {
            return View(new State());
        }

        [HttpPost]
        public ActionResult AddState(State state)
        {
            if (ModelState.IsValidField("State"))
            {
                if (string.IsNullOrEmpty(state.StateName))
                {
                    ModelState.AddModelError("State", "State Name Cannot Be Blank!");
                }
                else if (state.StateName.Any(char.IsDigit))
                {
                    ModelState.AddModelError("State", "State Name Cannot Contain Numbers!");
                }
                else if (string.IsNullOrEmpty(state.StateAbbreviation))
                {
                    ModelState.AddModelError("State", "State Abbreviation Cannot Be Blank!");
                }
                else if (state.StateAbbreviation.Length > 2)
                {
                    ModelState.AddModelError("State", "State Abbreviation Must Be Only Two Characters!");
                }
                else
                {
                    StateRepository.Add(state);
                    return RedirectToAction("States");
                }

                return View(state);
            }
            return View(state);
        }

        [HttpGet]
        public ActionResult EditState(string id)
        {
            var state = StateRepository.Get(id);

            return View(state);
        }

        [HttpPost]
        public ActionResult EditState(State state)
        {
            if (ModelState.IsValidField("State"))
            {
                if (string.IsNullOrEmpty(state.StateName))
                {
                    ModelState.AddModelError("State", "State Name Cannot Be Blank!");
                }
                else if (state.StateName.Any(char.IsDigit))
                {
                    ModelState.AddModelError("State", "State Name Cannot Contain Numbers!");
                }
                else if (string.IsNullOrEmpty(state.StateAbbreviation))
                {
                    ModelState.AddModelError("State", "State Abbreviation Cannot Be Blank!");
                }
                else if (state.StateAbbreviation.Length > 2)
                {
                    ModelState.AddModelError("State", "State Abbreviation Must Be Only Two Characters!");
                }
                else if (state.StateAbbreviation.Any(char.IsDigit))
                {
                    ModelState.AddModelError("State", "State Abbreviation Cannot Contain Numbers!");
                }
                else
                {
                    State newState = new State();

                    newState.StateAbbreviation = state.StateAbbreviation;
                    newState.StateName = state.StateName;

                    StateRepository.Edit(newState);

                    return RedirectToAction("States");
                }

                return View(state);
            }
            return View(state);
        }

        [HttpGet]
        public ActionResult DeleteState(string id)
        {
            var state = StateRepository.Get(id);

            return View(state);
        }

        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }
    }
}