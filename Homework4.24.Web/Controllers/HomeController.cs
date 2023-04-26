using Homework4._24.Data;
using Homework4._24.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework4._24.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connection = $@"data Source=.;Initial Catalog=MyFirstDatabase;Integrated Security=true;TrustServerCertificate=True";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople()
        {
            var repo = new PeopleRepository(_connection);
            List<Person> people = repo.GetPeople();
            return Json(people);
        }
        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connection);
            repo.AddPerson(person);
        }
        public IActionResult EditPerson(int id)
        {
            var repo = new PeopleRepository(_connection);
            Person p = repo.GetPerson(id);
            return Json(p);
        }
        [HttpPost]
        public void EditPerson(Person person)
        {
            var repo = new PeopleRepository(_connection);
            repo.UpdatePerson(person);
        }

        public void DeletePerson(int id)
        {
            var repo = new PeopleRepository(_connection);
            repo.DeletePerson(id);
        }
    }
}