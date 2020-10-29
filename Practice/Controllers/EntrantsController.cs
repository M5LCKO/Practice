using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrantsController : ControllerBase
    {

        private readonly ILogger<EntrantsController> _logger;

        public EntrantsController(ILogger<EntrantsController> logger)
        {
            _logger = logger;


        }

        [HttpGet]
        public IEnumerable<Entrants> Get()
        {

            using (ApplicationContext db = new ApplicationContext(Startup.options))
            {
                return db.Entrants.ToArray();
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(Entrants entrant, int Id)
        {
            using (ApplicationContext db = new ApplicationContext(Startup.options))
            {
               db.Entrants.Update(entrant);
               db.SaveChanges();
            }
            return StatusCode(200);
        }


        [HttpPost]
        public ActionResult Create(Entrants entrant)
        {
            using (ApplicationContext db = new ApplicationContext(Startup.options))
            {
                db.Entrants.Add(entrant);
                db.SaveChanges();
            }

            return StatusCode(200);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Entrants b = new Entrants { Id = id };
            using (ApplicationContext db = new ApplicationContext(Startup.options))
            {
                db.Entry(b).State = EntityState.Deleted;
                db.SaveChanges();
            }

            return StatusCode(200);
        }

    }
}
