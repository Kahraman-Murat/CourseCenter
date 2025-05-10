using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Authorize(Roles = "Admin,Content-Manager,Teacher,Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService, IMapper _mapper) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine("User Claims: *******************************");
            foreach (var claim in HttpContext.User.Claims)
                Console.WriteLine($"{claim.Type}: {claim.Value}");            
            Console.WriteLine("User Claims End: *******************************");

            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> usersWithRoles()
        {
            var users = await _userService.GetUsersWithRolesAsync();

            return Ok(users);
        }

        [HttpGet("userProfile")]
        public async Task<IActionResult> userProfile()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
            string claimValue = claim == null ? "0" : claim.Value;

            var users = await _userService.GetUsersWithRolesAsync();
            var user = users.AsQueryable().Where(x => x.Id.ToString() == claimValue).FirstOrDefault();
            var mappedUser = _mapper.Map<ResultUserWithRolesDto>(user);

            return Ok(mappedUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id); ;

            return Ok(user);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            // FluentValidation burada devreye giriyor,
            // ModelState kontrolüne gerek kalmıyor
            var (success, errors) = await _userService.CreateAsync(createUserDto);

            if (success)
                return StatusCode(StatusCodes.Status201Created, new { Result = "Kayıt başarılı." } );

            return BadRequest(new { Errors = errors });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var (success, errors) = await _userService.UpdateAsync(updateUserDto);

            if (success)
                return StatusCode(StatusCodes.Status201Created, new { Success = true, Message = "Kayıt başarılı." });

            return BadRequest(new { Success = false, Message = errors });
        }

        [HttpGet("GetRolesForUser/{id}")]
        public async Task<IActionResult> GetRolesForUser(int id)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //var result = await _userService.GetRolesForUserAsync(id, assembly);
            var result = await _userService.GetUserRolesAsync(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [HttpPost("AssignRoles")]
        public async Task<IActionResult> AssignRoles(AssignUserRolesDto assignUserRolesDto)
        {
            var result = await _userService.AssignRolesToUserAsync(assignUserRolesDto);
            if (result.Any())
                return Ok();

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("GetUserCountInRoles/{roleName}")]
        public async Task<IActionResult> GetUserCountInRoles(string roleName)
        {
            var userCountInRole = await _userService.GetUserCountInRolesAsync(roleName);

            return Ok(userCountInRole);
        }


        [HttpGet("GetUsersInRole/{roleName}")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var usersInRole = await _userService.GetUsersInRoleAsync(roleName);

            return Ok(usersInRole);
        }

        [AllowAnonymous]
        [HttpGet("GetLast4Teachers")]
        public async Task<IActionResult> GetLast4Teachers()
        {
            var datas = await _userService.GetLast4TeachersAsync();
            //var mappingDatas = _mapper.Map<List<ResultUserSocialMediasDto>>(datas);
            return Ok(datas);
        }

        [AllowAnonymous]
        [HttpGet("GetTeachersWithSocialMedia/{page}")]
        public async Task<IActionResult> GetTeachersWithSocialMedia(int page)
        {
            var datas = await _userService.GetTeachersWithSocialMediaAsync(page);
            //var mappingDatas = _mapper.Map<List<ResultUserSocialMediasDto>>(datas);
            return Ok(datas);
        }
    }
}
