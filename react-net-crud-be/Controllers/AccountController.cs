using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using react_net_crud_be.Models;

namespace react_net_crud_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountContext _accountContext;
        public AccountController(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_accountContext.Accounts == null)
            {
                return NotFound();
            }
            return await _accountContext.Accounts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_accountContext.Accounts == null)
            {
                return NotFound();
            }

            var account = await _accountContext.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }
            return account;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _accountContext.Accounts.Add(account);
            await _accountContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = account.ID }, account);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAccount(int id, Account account)
        {
            if (id != account.ID)
            {
                return BadRequest();
            }

            _accountContext.Entry(account).State = EntityState.Modified;

            try
            {
                await _accountContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            if (_accountContext.Accounts == null)
            {
                return NotFound();
            }

            var account = await _accountContext.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            _accountContext.Accounts.Remove(account);
            await _accountContext.SaveChangesAsync();

            return Ok();
        }

    }
}
