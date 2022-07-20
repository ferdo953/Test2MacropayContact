using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using Test2MacropayContact.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test2MacropayContact.Controllers
{
    public class ContactsController : DefaultController
    {

        // GET: api/<ContactsController>
        //https://localhost:44377/api/Contacts?phrase=Az
        [Microsoft.AspNetCore.Mvc.HttpGet()]
        public ActionResult<List<Contacts>> Get()
        {
            Request.ContentType = "application/json";
            var request = HttpContext.Request.Query["phrase"];
            if(request.ToString() == null || request.ToString().Length == 0)
            {
                return BadRequest();
            }
            string FileJsoN = System.IO.File.ReadAllText(@"C:\Users\sysadmin\source\repos\Test2MacropayContact\Test2MacropayContact\Database\fakedatabase.js");
            var res = JsonSerializer.Deserialize<List<Contacts>>(FileJsoN).AsQueryable().OrderBy(x => x.name).ToList();
            var resultado = res.FindAll(x => x.name.ToUpper().Contains(request.ToString().ToUpper()));
            return resultado;
        }

        // GET api/<ContactsController>/cf63ff0d-ecaa-448d-9456-a225a44c3159
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public Microsoft.AspNetCore.Mvc.ActionResult<Contacts> Get(string id)
        {
            string FileJsoN = System.IO.File.ReadAllText(@"C:\Users\sysadmin\source\repos\Test2MacropayContact\Test2MacropayContact\Database\fakedatabase.js");
            var res = JsonSerializer.Deserialize<List<Contacts>>(FileJsoN);
            var resultado = res.Find(x => x.id == id);
            if(resultado != null)
            {
                return resultado;
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<ContactsController>/cf63ff0d-ecaa-448d-9456-a225a44c3159
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
             string FileJsoN = System.IO.File.ReadAllText(@"C:\Users\sysadmin\source\repos\Test2MacropayContact\Test2MacropayContact\Database\fakedatabase.js");
             var res = JsonSerializer.Deserialize<List<Contacts>>(FileJsoN);
            foreach (var item in res)
            {
                if (item.id == id) 
                {
                   var validacion = res.Remove(item);
                    System.IO.TextWriter writeFile = new StreamWriter(@"C:\Users\sysadmin\source\repos\Test2MacropayContact\Test2MacropayContact\Database\fakedatabase.js");
                    writeFile.Write(JsonSerializer.Serialize(res));
                    writeFile.Flush();
                    writeFile.Close();
                    if (validacion)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }

            return NotFound();
        }
    }
}
