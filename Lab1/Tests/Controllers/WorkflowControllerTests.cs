using Lab1.Controllers;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RulesEngine.Models;
using Xunit;

namespace Lab1.Tests.Controllers
{
    public class WorkflowControllerTests
    {
        private readonly Mock<workflowService> _mockWorkflowService;
        private readonly workflowController _controller;

        public WorkflowControllerTests()
        {
            _mockWorkflowService = new Mock<workflowService>();
            _controller = new workflowController(_mockWorkflowService.Object);
        }

        [Fact]
        public async Task CreateWorkflow_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var workflow = new WorkflowModel
            {
                Name = "TestWorkflow",
                Content = @"{
                    'WorkflowName': 'TestWorkflow',
                    'Rules': [
                        {
                            'RuleName': 'TestRule',
                            'Expression': 'input.Amount <= 1000'
                        }
                    ]
                }"
            };

            _mockWorkflowService.Setup(s => s.SaveWorkflow(It.IsAny<WorkflowModel>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateWorkflow(workflow);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Workflow {workflow.Name} created successfully", okResult.Value);
            _mockWorkflowService.Verify(s => s.SaveWorkflow(workflow), Times.Once);
        }

        [Fact]
        public async Task ExecuteWorkflow_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var workflowName = "TestWorkflow";
            var input = new WorkflowInputModel
            {
                Amount = 500,
                Category = "Travel"
            };

            var expectedResults = new List<RuleResultTree>
            {
                new RuleResultTree
                {
                    Rule = new Rule { RuleName = "TestRule" },
                    IsSuccess = true
                }
            };

            _mockWorkflowService.Setup(s => s.ExecuteWorkflow(workflowName, input))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _controller.ExecuteWorkflow(workflowName, input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResults = Assert.IsType<List<RuleResultTree>>(okResult.Value);
            Assert.Single(returnedResults);
            Assert.True(returnedResults[0].IsSuccess);
        }

        [Fact]
        public async Task ExecuteWorkflow_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var workflowName = "NonexistentWorkflow";
            var input = new WorkflowInputModel
            {
                Amount = 500,
                Category = "Travel"
            };

            _mockWorkflowService.Setup(s => s.ExecuteWorkflow(workflowName, input))
                .ThrowsAsync(new FileNotFoundException("Workflow not found"));

            // Act
            var result = await _controller.ExecuteWorkflow(workflowName, input);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            dynamic error = statusCodeResult.Value;
            Assert.Equal("Workflow not found", (string)error.error);
        }

        [Fact]
        public async Task ExecuteChainedWorkflows_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var request = new ChainedWorkflowRequest
            {
                FirstWorkflowName = "FirstWorkflow",
                SecondWorkflowName = "SecondWorkflow",
                InputParameters = new WorkflowInputModel
                {
                    Amount = 500,
                    Category = "Travel"
                }
            };

            var expectedResults = new List<RuleResultTree>
            {
                new RuleResultTree
                {
                    Rule = new Rule { RuleName = "ChainedRule" },
                    IsSuccess = true
                }
            };

            _mockWorkflowService.Setup(s => s.ExecuteChainedWorkflows(
                    request.FirstWorkflowName,
                    request.SecondWorkflowName,
                    request.InputParameters))
                .ReturnsAsync(expectedResults);

            // Act
            var result = await _controller.ExecuteChainedWorkflows(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResults = Assert.IsType<List<RuleResultTree>>(okResult.Value);
            Assert.Single(returnedResults);
            Assert.True(returnedResults[0].IsSuccess);
        }

        [Fact]
        public async Task ExecuteChainedWorkflows_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var request = new ChainedWorkflowRequest
            {
                FirstWorkflowName = "NonexistentWorkflow",
                SecondWorkflowName = "SecondWorkflow",
                InputParameters = new WorkflowInputModel
                {
                    Amount = 500,
                    Category = "Travel"
                }
            };

            _mockWorkflowService.Setup(s => s.ExecuteChainedWorkflows(
                    request.FirstWorkflowName,
                    request.SecondWorkflowName,
                    request.InputParameters))
                .ThrowsAsync(new FileNotFoundException("First workflow not found"));

            // Act
            var result = await _controller.ExecuteChainedWorkflows(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            dynamic error = statusCodeResult.Value;
            Assert.Equal("First workflow not found", (string)error.error);
        }
    }
}