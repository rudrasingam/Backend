using Microsoft.AspNetCore.Mvc;
using Restaurant.DTO;
using Restaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment([FromBody] PaymentDTO paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newPayment = await _paymentService.CreatePaymentAsync(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = newPayment.PaymentId }, newPayment);
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentDTO paymentDto)
        {
            if (id != paymentDto.PaymentId)
            {
                return BadRequest("ID mismatch.");
            }

            var existingPayment = await _paymentService.GetPaymentByIdAsync(id);
            if (existingPayment == null)
            {
                return NotFound("Payment not found.");
            }

            await _paymentService.UpdatePaymentAsync(paymentDto);
            return NoContent();  // 204 No Content is a typical response for successful PUT requests.
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var success = await _paymentService.DeletePaymentAsync(id);
            if (!success)
            {
                return NotFound("Payment not found.");
            }
            return NoContent();  // Successfully deleted
        }
    }
}
