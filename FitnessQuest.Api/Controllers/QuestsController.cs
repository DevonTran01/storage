using Microsoft.AspNetCore.Mvc;
using FitnessQuest.Api.Models;
using FitnessQuest.Api.Services;

namespace FitnessQuest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestsController : ControllerBase
{
    [HttpPost("generate")]
    public ActionResult<QuestDto> Generate([FromBody] GenerateQuestRequest req)
    {
        if (!Enum.TryParse<QuestDifficulty>(req.Difficulty, true, out var difficulty))
            return BadRequest(new ErrorDto("Invalid difficulty. Use Easy, Medium, or Hard."));

        var quest = QuestGenerator.Generate(difficulty); // ✅ static call
        return Ok(quest);
    }
}