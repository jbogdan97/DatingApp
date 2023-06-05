using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        // private readonly DataContext _context;
//         public UsersController(DataContext context)
//         {
//  _context = context;
//         }
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
{
            _userRepository = userRepository;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
        //    return await _context.Users.ToListAsync();
        // var users = await _userRepository.GetUsersAsync();
        // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
        // return Ok(usersToReturn);
        var users = await _userRepository.GetMembersAsync();
        return Ok(users);
        }


//        [HttpGet("{id}")]
//        public ActionResult<AppUser> GetUser(int id)
//        {
//         // creezi var doar daca o mai folosesti si pentru altceva altfel direct in return;
//         var users = _context.Users.Find(id);
// return users;
//        }
//        Varianta asincrona = jos

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
        //    return await _context.Users.FindAsync(id);
        // var user = await _userRepository.GetUserByUsernameAsync(username);
        // return _mapper.Map<MemberDto>(user);
        return await _userRepository.GetMemberAsync(username);
        }
    }
}