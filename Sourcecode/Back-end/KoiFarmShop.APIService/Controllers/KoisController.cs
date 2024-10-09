using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Data.Models;
using KoiFarmShop.Data;

namespace KoiFarmShop.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoisController : ControllerBase
    {
        //private readonly FA_SE1854_SWP391_G3_KoiFarmShopContext _context;
        private readonly UnitOfWork _unitOfWork;

        public KoisController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public KoisController(FA_SE1854_SWP391_G3_KoiFarmShopContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Kois
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Koi>>> GetKois()
        {
            //return await _context.Kois.ToListAsync();
            return _unitOfWork.KoiRepository.GetAll();
        }

        // GET: api/Kois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Koi>> GetKoi(int id)
        {
            //var koi = await _context.Kois.FindAsync(id);
            var koi = _unitOfWork.KoiRepository.GetById(id);

            if (koi == null)
            {
                return NotFound();
            }

            return koi;
        }

        // PUT: api/Kois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKoi(int id, Koi koi)
        {
            if (id != koi.KoiId)
            {
                return BadRequest();
            }

            //_context.Entry(koi).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                _unitOfWork.KoiRepository.UpdateAsync(koi);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KoiExists(id))
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
        public async Task<ActionResult<Koi>> PostKoi(Koi koi)
        {
            //_context.Kois.Add(koi);
            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.KoiRepository.CreateAsync(koi);
            }
            catch (DbUpdateException)
            {
                if (KoiExists(koi.KoiId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKoi", new { id = koi.KoiId }, koi);
        }

        // DELETE: api/Kois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKoi(int id)
        {
            //var koi = await _context.Kois.FindAsync(id);
            var koi = await _unitOfWork.KoiRepository.GetByIdAsync(id);
            if (koi == null)
            {
                return NotFound();
            }

            await _unitOfWork.KoiRepository.SaveAsync();

            //_context.Kois.Remove(koi);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KoiExists(int id)
        {
            //return _context.Kois.Any(e => e.KoiId == id);
            return _unitOfWork.KoiRepository.GetByIdAsync(id) == null;
        }
    }
}
