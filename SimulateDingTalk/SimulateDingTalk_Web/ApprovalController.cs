using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OAApproval.DTOs;
using OAApproval.Services;

namespace OAApproval.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalEngine _approvalEngine;

        public ApprovalController(IApprovalEngine approvalEngine)
        {
            _approvalEngine = approvalEngine;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartProcess([FromBody] StartProcessRequest request)
        {
            var result = await _approvalEngine.StartProcessAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessTask([FromBody] ProcessTaskRequest request)
        {
            var result = await _approvalEngine.ProcessTaskAsync(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("withdraw/{instanceId}")]
        public async Task<IActionResult> Withdraw(int instanceId, [FromQuery] string userId)
        {
            var result = await _approvalEngine.WithdrawAsync(instanceId, userId);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}