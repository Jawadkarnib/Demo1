using Lab1.models;
using Lab1.Models;
using Newtonsoft.Json;
using RulesEngine.Models;

namespace Lab1;

public class workflowService
{
     private readonly string _workflowPath;
    private readonly IWebHostEnvironment _environment;

    public workflowService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _workflowPath = Path.Combine(_environment.WebRootPath, "workflows");
        if (!Directory.Exists(_workflowPath))
        {
            Directory.CreateDirectory(_workflowPath);
        }
    }

    public async Task SaveWorkflow(WorkflowModel workflow)
    {
        var filePath = Path.Combine(_workflowPath, $"{workflow.Name}.json");
        await File.WriteAllTextAsync(filePath, workflow.Content);
    }

    public async Task<string> GetWorkflow(string name)
    {
        var filePath = Path.Combine(_workflowPath, $"{name}.json");
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Workflow {name} not found");
            
        return await File.ReadAllTextAsync(filePath);
    }

    public async Task<List<RuleResultTree>> ExecuteWorkflow(string workflowName, WorkflowInputModel input)
    {
        var workflowJson = await GetWorkflow(workflowName);
        var workflows = JsonConvert.DeserializeObject<Workflow[]>(workflowJson);
        var rulesEngine = new RulesEngine.RulesEngine(workflows);

        return await rulesEngine.ExecuteAllRulesAsync(workflowName, input);
    }

    public async Task<List<RuleResultTree>> ExecuteChainedWorkflows(
        string firstWorkflow, 
        string secondWorkflow, 
        WorkflowInputModel input)
    {
        // Execute first workflow
        var firstResult = await ExecuteWorkflow(firstWorkflow, input);
        
        // Prepare input for second workflow
        input.PreviousWorkflowSuccess = firstResult.Any(r => r.IsSuccess);
        
        // Execute second workflow
        return await ExecuteWorkflow(secondWorkflow, input);
    }

}

