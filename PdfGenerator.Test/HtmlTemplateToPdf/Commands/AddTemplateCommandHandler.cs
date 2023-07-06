

namespace PdfGenerator.Test.HtmlTemplateToPdf.Commands;

[TestClass]
public class AddTemplateCommandHandler
{
    [Test]
    public async Task Handler_Should_Successful_Add_Template()
    {
        // Arrange
        var aExpectedResult = true;
        var aCommand = new AddHtmlTemplateCommand("new_employee", "<p>Welcome [new_employee_name]</p>", "");
        var dataContextMock = await DatabaseContext.GetDatabaseContext();  
        var handler = new AddHtmlTemplateHandler(dataContextMock);
        // Act 
        var result = handler.Handle(aCommand, default);
        // Assert
        Assert.That(result.Result.Succeeded, Is.EqualTo(aExpectedResult));
    }
    
    [Test]
    public async Task Command_WhenTemplateNameIsNotUnique_ReturnInvalidOperationException()
    {
        // Arrange
        var aCommand = new AddHtmlTemplateCommand("testTemplate1", "<p>Welcome [new_employee_name]</p>", "");
        var dataContextMock = await DatabaseContext.GetDatabaseContext();  
        var handler = new AddHtmlTemplateHandler(dataContextMock);
        
        // Act and Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await handler.Handle(aCommand, CancellationToken.None);
        });
    }
    
    [Test]
    public async Task Command_WhenTemplateNameIsEmpty_ReturnsTemplateNameRequired()
    {
        // Arrange
        var expectedErrorMessage = "Name is required.";
        var command = new AddHtmlTemplateCommand("", "<p>Welcome [new_employee_name]</p>", "");
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
        var validator = new AddHtmlTemplateCommandValidator();
        var handler = new AddHtmlTemplateHandler(dataContextMock);

        // Act
        var validationResult = await validator.ValidateAsync(command);
        
        // Assert
        Assert.IsFalse(validationResult.IsValid);
        Assert.That(validationResult.Errors.Count, Is.EqualTo(1)); 
        Assert.That(validationResult.Errors[0].ErrorMessage, Is.EqualTo(expectedErrorMessage));
    }

    [Test]
    public async Task Command_WhenTemplateContentIsEmpty_ReturnsTemplateContentRequired()
    {
        // Arrange
        var expectedErrorMessage = "Content is required.";
        var command = new AddHtmlTemplateCommand("template_test", "", "");
        var dataContextMock = await DatabaseContext.GetDatabaseContext();
        var validator = new AddHtmlTemplateCommandValidator();
        var handler = new AddHtmlTemplateHandler(dataContextMock);

        // Act
        var validationResult = await validator.ValidateAsync(command);
        
        // Assert
        Assert.IsFalse(validationResult.IsValid);
        Assert.That(validationResult.Errors.Count, Is.EqualTo(1)); 
        Assert.That(validationResult.Errors[0].ErrorMessage, Is.EqualTo(expectedErrorMessage));
    }
    
    
}