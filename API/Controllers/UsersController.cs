using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
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
        // private readonly IUserRepository _unitOfWork.UserRepository;
        private readonly IMapper _mapper;
        public readonly IPhotoService _photoService;
        public IUnitOfWork _unitOfWork { get; }

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, 
        IPhotoService photoService)
{
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            // _unitOfWork.UserRepository = userRepository;
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

// [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
          var gender = await _unitOfWork.UserRepository.GetUserGender(User.GetUsername());
userParams.CurrentUsername = User.GetUsername();

if(string.IsNullOrEmpty(userParams.Gender)) userParams.Gender = gender == "male" ? "female" : "male";

        //    return await _context.Users.ToListAsync();
        // var users = await _unitOfWork.UserRepository.GetUsersAsync();
        // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
        // return Ok(usersToReturn);
        var users = await _unitOfWork.UserRepository.GetMembersAsync(userParams);

Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
 users.TotalCount, users.TotalPages);

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

// [Authorize(Roles = "Member")]
        [HttpGet("{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
        //    return await _context.Users.FindAsync(id);
        // var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        // return _mapper.Map<MemberDto>(user);
        return await _unitOfWork.UserRepository.GetMemberAsync(username);
        }
    
    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

        _mapper.Map(memberUpdateDto, user);

        _unitOfWork.UserRepository.Update(user);

        if(await _unitOfWork.Complete()) return NoContent();

        return BadRequest("Failed to update user.");     
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
          var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

          var result = await _photoService.AddPhotoAsync(file);

          if(result.Error != null) return BadRequest(result.Error.Message);

          var photo = new Photo
          {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
          };

          if(user.Photos.Count == 0)
          {
            photo.IsMain = true;
          }

          user.Photos.Add(photo);

          if(await _unitOfWork.Complete())
          {
            // return _mapper.Map<PhotoDto>(photo);
            return CreatedAtRoute("GetUser", new {username = user.UserName}, _mapper.Map<PhotoDto>(photo));
            }

          return BadRequest("Problema la adaugarea pozei");
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
      var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

      var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

      if (photo.IsMain) return BadRequest("Aceasta este deja imaginea ta de profil.");

      var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

      if(currentMain != null) currentMain.IsMain = false;

      photo.IsMain = true;

      if(await _unitOfWork.Complete()) return NoContent();

      return BadRequest("A aparut o problema in setarea pozei de profil");

      
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
      var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

      var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

      if(photo == null) return NotFound();

      if(photo.IsMain) return BadRequest("Nu poti sterge imaginea de profil.");

      if(photo.PublicId != null)
      {
        var result = await _photoService.DeletePhotoAsync(photo.PublicId);
      if(result.Error != null) return BadRequest(result.Error.Message);
      }

user.Photos.Remove(photo);

if(await _unitOfWork.Complete()) return Ok();

return BadRequest("Problema la stergerea imaginii");
    }
    }
}