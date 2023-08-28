using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.Domain;
using ProductAPI.Model.DTO;
using ProductAPI.Repository;
using ProductAPI.Services.AuthServices;

namespace ProductAPI.Controllers
{

    
    
    public class AuthController : Controller
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;

        public AuthController(IAuthRepository authRepository, IMapper _mapper)
        {
            this.authRepository = authRepository;
            mapper = _mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("api/authentication")]
        public async Task<IActionResult> Login([FromForm] UserLoginDTO userdto)
        {
            try
            {
                var ExistingUser = await authRepository.IsUserEmailAndPasswordExistAsync(userdto);

                if (ExistingUser is not null)
                {
                    var tokenGenerator = new JWTTokenGenerator();

                    var token = tokenGenerator.CreateJWT(ExistingUser);

                    return Ok(new { ok = true, Token = token });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest(new { ok = false , message = "Invalid Credential" });
        }
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterDTO user)
        {
            try
            {
                if (user is null)
                {
                    return BadRequest(new {ok = false ,  message = "invalid user info" });
                }

                if (await authRepository.IsUserEmailExistAsync(user.Email))
                {
                    return BadRequest(new {ok = false , message = "Email already exist" });
                }

                var result = await authRepository.RegisterAsync(user);

               if(result)return Ok(new { ok = true , message = "register Successfull" });

                return BadRequest("internal server error");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
    
}
