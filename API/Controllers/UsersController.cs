using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
 _context = context;
        }


    //    [HttpGet]
    //    public ActionResult<IEnumerable<AppUser>> GetUsers ()
    //    {
    //     // creezi var doar daca o mai folosesti si pentru altceva altfel direct in return;
    //      var users = _context.Users.ToList();

    //      return users;
    //    }
       //        Varianta asincrona = jos

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
           return await _context.Users.ToListAsync();
        }


//        [HttpGet("{id}")]
//        public ActionResult<AppUser> GetUser(int id)
//        {
//         // creezi var doar daca o mai folosesti si pentru altceva altfel direct in return;
//         var users = _context.Users.Find(id);
// return users;
//        }
//        Varianta asincrona = jos

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
           return await _context.Users.FindAsync(id);
        }
    }
}