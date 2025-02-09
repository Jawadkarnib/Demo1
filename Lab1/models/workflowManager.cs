using Microsoft.Build.Framework;
using RulesEngine.Models;

namespace Lab1.Models;

// Base model for workflow definition
public class WorkflowModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Content { get; set; }
}

// Model for workflow input parameters
public class WorkflowInputModel
{
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public bool? PreviousWorkflowSuccess { get; set; } // Optional, only used in chained workflows
}

// Model for chained workflow request
public class ChainedWorkflowRequest
{
    [Required]
    public string FirstWorkflowName { get; set; }
    [Required]
    public string SecondWorkflowName { get; set; }
    [Required]
    public WorkflowInputModel InputParameters { get; set; }
}

// Class to manage workflow operations (if needed)
public class WorkflowManager
{
    private readonly RulesEngine.RulesEngine _rulesEngine;

    public WorkflowManager(RulesEngine.RulesEngine rulesEngine)
    {
        _rulesEngine = rulesEngine;
    }

    public async Task<List<RuleResultTree>> ExecuteWorkflow(string workflowName, WorkflowInputModel input)
    {
        // Implementation here
        throw new NotImplementedException();
    }

    public async Task<List<RuleResultTree>> ExecuteChainedWorkflows(
        string firstWorkflow, 
        string secondWorkflow, 
        WorkflowInputModel input)
    {
        // Implementation here
        throw new NotImplementedException();
    }
}

//input for workflow 1



//input 