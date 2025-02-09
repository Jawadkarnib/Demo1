using System.Text.Json;
using Lab1.models;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class workflowController : ControllerBase
{
    private readonly workflowService _workflowService;

    public workflowController(workflowService workflowService)
    {
        _workflowService = workflowService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateWorkflow([FromBody] WorkflowModel workflow)
    {
        await _workflowService.SaveWorkflow(workflow);
        return Ok($"Workflow {workflow.Name} created successfully");
    }

    [HttpPost("execute/{workflowName}")]
    public async Task<IActionResult> ExecuteWorkflow(string workflowName, [FromBody] WorkflowInputModel input)
    {
        try
        {
            var result = await _workflowService.ExecuteWorkflow(workflowName, input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("execute-chained")]
    public async Task<IActionResult> ExecuteChainedWorkflows([FromBody] ChainedWorkflowRequest request)
    {
        try
        {
            var result = await _workflowService.ExecuteChainedWorkflows(
                request.FirstWorkflowName,
                request.SecondWorkflowName,
                request.InputParameters);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}