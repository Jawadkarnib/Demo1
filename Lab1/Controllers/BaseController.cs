using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers;

public abstract class BaseController:ControllerBase
{
    public abstract String getDate(String language);

}