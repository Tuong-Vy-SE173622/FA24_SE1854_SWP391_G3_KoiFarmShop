using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UserController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public KoisController(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Kois
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //return await _context.Kois.ToListAsync();
            return _unitOfWork.UserRepository.GetAll();
        }

        // GET: api/Kois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int uid)
        {
            //var koi = await _context.Kois.FindAsync(id);
            var user = _unitOfWork.UserRepository.GetById(uid);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Kois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int uid, User user)
        {
            if (uid != user.UserId)
            {
                return BadRequest();
            }

            //_context.Entry(koi).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                _unitOfWork.UserRepository.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(uid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Kois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //_context.Kois.Add(koi);
            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.UserRepository.CreateAsync(user);
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { uid = user.UserId }, user);
        }

        // DELETE: api/Kois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //var koi = await _context.Kois.FindAsync(id);
            var koi = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (koi == null)
            {
                return NotFound();
            }

            await _unitOfWork.UserRepository.SaveAsync();

            //_context.Kois.Remove(koi);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int uid)
        {
            //return _context.Kois.Any(e => e.KoiId == id);
            return _unitOfWork.UserRepository.GetByIdAsync(uid) == null;
        }
    }
}