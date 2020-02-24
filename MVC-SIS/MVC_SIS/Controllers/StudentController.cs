using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            bool isInt;

            if (ModelState.IsValidField("Student"))
            {
                studentVM.Student.Courses = new List<Course>();

                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);

                if (string.IsNullOrEmpty(studentVM.Student.FirstName))
                {
                    ModelState.AddModelError("Student", "Student First Name Cannot Be Blank!");
                }
                else if (studentVM.Student.FirstName.Any(char.IsDigit))
                {
                    ModelState.AddModelError("Student", "Student First Name Cannot Contain Numbers!");
                }
                else if (string.IsNullOrEmpty(studentVM.Student.LastName))
                {
                    ModelState.AddModelError("Student", "Student Last Name Cannot Be Blank!");
                }
                else if (studentVM.Student.LastName.Any(char.IsDigit))
                {
                    ModelState.AddModelError("Student", "Student Last Name Cannot Contain Numbers!");
                }
                else if (string.IsNullOrEmpty(studentVM.Student.Major.ToString()))
                {
                    ModelState.AddModelError("Student", "You Must Select a Major!");
                }
                else if (studentVM.Student.GPA <= 0)
                {
                    ModelState.AddModelError("Student", "Student GPA Cannot be 0!");
                }
                else if (studentVM.Student.GPA > 4)
                {
                    ModelState.AddModelError("Student", "Student GPA Must Be Between 0.1 and 4.0");
                }
                else if (studentVM.Student.Courses.Count == 0)
                {
                    ModelState.AddModelError("Student", "Student Must Take At Least One Course!");
                }
                else
                {
                    var totalStudents = StudentRepository.GetAll().Count();

                    studentVM.Student.StudentId = totalStudents + 1;

                    StudentRepository.Add(studentVM.Student);

                    return RedirectToAction("List");

                }

                var viewModel = new StudentVM();
                viewModel.SetCourseItems(CourseRepository.GetAll());
                viewModel.SetMajorItems(MajorRepository.GetAll());

                return View(viewModel);
            }

            var viewModel2 = new StudentVM();
            viewModel2.SetCourseItems(CourseRepository.GetAll());
            viewModel2.SetMajorItems(MajorRepository.GetAll());

            return View(viewModel2);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = StudentRepository.Get(id);

            var model = new StudentVM();

            model.SetCourseItems(CourseRepository.GetAll());
            model.SetMajorItems(MajorRepository.GetAll());

            model.Student.Major = MajorRepository.Get(student.Major.MajorId);

            model.Student.StudentId = student.StudentId;

            model.Student.FirstName = student.FirstName;

            model.Student.LastName = student.LastName;

            model.Student.GPA = student.GPA;

            model.Student.Major = student.Major;

            model.Student.Courses = student.Courses;

            if (student.Address != null)
            {
                var stuAddress = (from students in StudentRepository.GetAll()
                                  where student.StudentId == student.Address.AddressId
                                  select student.Address).FirstOrDefault();

                model.Student.Address = stuAddress;
            }
            else
            {
                model.Student.Address = null;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StudentVM model)
        {
            Student student = new Student();

            student.StudentId = model.Student.StudentId;
            student.FirstName = model.Student.FirstName;

            
            if (string.IsNullOrEmpty(student.FirstName))
            {
                ModelState.AddModelError("Student", "Student Last Name Cannot Be Blank!");
            }
            else if (model.Student.FirstName.Any(char.IsDigit))
            {
                ModelState.AddModelError("Student", "Student First Name Cannot Contain Numbers!");
            }

            student.LastName = model.Student.LastName;

            if (string.IsNullOrEmpty(student.LastName))
            {
                ModelState.AddModelError("Student", "Student Last Name Cannot Be Blank!");
            }
            else if (student.LastName.Any(char.IsDigit))
            {
                ModelState.AddModelError("Student", "Student Last Name Cannot Contain Numbers!");
            }

            student.GPA = model.Student.GPA;

            if (student.GPA == 0)
            {
                ModelState.AddModelError("Student", "Student GPA Cannot Be Zero!");
            }
            if (student.GPA > 4)
            {
                ModelState.AddModelError("Student", "Student GPA Must Be Between 0.1 and 4.0");
            }

            student.Major = MajorRepository.Get(model.Student.Major.MajorId);

            student.Courses = model.Student.Courses;

            if (string.IsNullOrEmpty(model.SelectedCourseIds.ToString()))
            {
                ModelState.AddModelError("Student", "Student Must Take At Least One Course!");
            }

            Address address = new Address();
            State state = new State();

            if (model.Student.Address.AddressId == 0)
            {
                address.AddressId = student.StudentId;
            }
            else
            {
                address.AddressId = model.Student.Address.AddressId;
            }

            address.Street1 = model.Student.Address.Street1;

            address.Street2 = model.Student.Address.Street2;

            address.City = model.Student.Address.City;

            state.StateName = model.Student.Address.State.StateName;

            var stateNames = (from states in StateRepository.GetAll()
                              select states.StateName).ToList();

            if (state.StateName != null && !stateNames.Contains(state.StateName))
            {
                ModelState.AddModelError("Student", "Error: State is Not a Valid Option in Database");
            }

            var stateAbb = (from states in StateRepository.GetAll()
                            where states.StateName == model.Student.Address.State.StateName
                            select states.StateAbbreviation).FirstOrDefault();

            state.StateAbbreviation = stateAbb;

            address.PostalCode = model.Student.Address.PostalCode;

            if (address.PostalCode != null && address.PostalCode.Length != 5)
            {
                ModelState.AddModelError("Student", "Please Enter a Five Digit Zip Code");
            }

            address.State = state;

            student.Address = address;

            if (ModelState.IsValid)
            {
                StudentRepository.Delete(model.Student.StudentId);

                StudentRepository.Add(student);

                return RedirectToAction("List");
            }
            else
            {

                var viewModel = new StudentVM();
                viewModel.SetCourseItems(CourseRepository.GetAll());
                viewModel.SetMajorItems(MajorRepository.GetAll());

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            StudentRepository.Delete(id);
            return RedirectToAction("List");
        }
    }
}