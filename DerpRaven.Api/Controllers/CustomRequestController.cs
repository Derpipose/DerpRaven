﻿using DerpRaven.Api.Services;
using Microsoft.AspNetCore.Mvc;
using DerpRaven.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DerpRaven.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomRequestController : ControllerBase
{
    private readonly ICustomRequestService _customRequestService;

    public CustomRequestController(ICustomRequestService customRequestService)
    {
        _customRequestService = customRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomRequests()
    {
        var requests = await _customRequestService.GetAllCustomRequestsAsync();
        return Ok(requests);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomRequestById(int id)
    {
        var request = await _customRequestService.GetCustomRequestByIdAsync(id);
        if (request == null) return NoContent();
        return Ok(request);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCustomRequestsByUser(int userId)
    {
        var requests = await _customRequestService.GetCustomRequestsByUserIdAsync(userId);
        return Ok(requests);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetCustomRequestsByStatus(string status)
    {
        var requests = await _customRequestService.GetCustomRequestsByStatusAsync(status);
        return Ok(requests);
    }

    [HttpGet("type/{productType}")]
    public async Task<IActionResult> GetCustomRequestsByType(string productType)
    {
        var requests = await _customRequestService.GetCustomRequestsByTypeAsync(productType);
        return Ok(requests);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomRequest([FromBody] CustomRequestDto request)
    {
        bool wasCreated = await _customRequestService.CreateCustomRequestAsync(request);
        if (!wasCreated) return BadRequest();
        return Created();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] string status)
    {
        bool wasUpdated = await _customRequestService.ChangeStatusAsync(id, status);
        if (!wasUpdated) return BadRequest();
        return NoContent();
    }
}