using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Models.InputModels;
using GoodHamburger.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] string? order = null)
    {
        var result = await _orderService.GetAllAsync(page, pageSize, search, sort, order);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        if (result is null)
            return NotFound(new { error = $"Pedido '{id}' não encontrado." });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        try
        {
            var result = await _orderService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ProductNotExistException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (DuplicateOrderItemException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOrderException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderRequest request)
    {
        try
        {
            var result = await _orderService.UpdateAsync(id, request);
            return Ok(result);
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ProductNotExistException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (DuplicateOrderItemException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOrderException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
