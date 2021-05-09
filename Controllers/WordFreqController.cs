using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordFreqApi.DB;
using WordFreqApi.DTO.Request;
using WordFreqApi.Models;

namespace WordFreqApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WordFreqController : ControllerBase
    {
        private readonly IDbContext _context;

        public WordFreqController(IDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Returns all the submissions stored in the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Submission>>> GetAllSubmissions()
        {
            var result = await _context.SubmissionItems.Include(x=>x.Source).Include(x=>x.ContentFreq).ToListAsync();
            return result;
        }

        /// <summary>
        /// Returns the submissions for a given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Submission>> GetSubmissionById(long id)
        {
            var existingSub = await _context.Set<Submission>().FindAsync(id);

            if (existingSub == null)
            {
                return NotFound();
            }

            return existingSub;
        }
    
        /// <summary>
        /// Add a new submission from the <paramref name="subToAdd"/>
        /// </summary>
        /// <param name="subToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Submission>> AddSubmission(SubmissionRequestDTO subToAdd)
        {
            var itemToAdd = new Submission
                {
                    Comment = subToAdd.Comment,
                    Content = subToAdd.Content,
                    Source = new Source
                            {
                                Date = subToAdd.Source.Date,
                                Url = subToAdd.Source.Url
                            },
                    Submitter = subToAdd.Submitter
                };
                
            var result = subToAdd.Content.Split(" ")
                        .GroupBy(x=>x)
                        .Select(x=> new {Word = x.Key, Freq = x.Count()})
                        .OrderByDescending(x=>x.Freq)
                        .ThenBy(x=>x.Word)
                        .Take(3);
            

            var contentsList  = new List<HighFrequencyWord>();

            foreach(var item in result)
            {
                contentsList.Add(
                        new HighFrequencyWord{
                            Frequency = item.Freq,
                            Word = item.Word
                        });            
            }
            
            itemToAdd.ContentFreq = contentsList;

            _context.Set<Submission>().Add(itemToAdd);

            var savedId = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubmissionById), new { id = savedId }, subToAdd);
        }

        /// <summary>
        /// Delete the submission based on the given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmissionById(long id)
        {
            var todoItem = await _context.Set<Submission>().FindAsync(id);
            if (todoItem == null)
            {
                    return NotFound();
            }

            _context.Set<Submission>().Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubmissionExists(long id)
        {
            return _context.Set<Submission>().Any(e => e.SubmissionId == id);
        }
    }
}
