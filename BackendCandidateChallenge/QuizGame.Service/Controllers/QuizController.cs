using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Service.Model.Answers;
using QuizGame.Service.Model.Questions;
using QuizGame.Service.Model.Quizes;
using QuizGame.Service.Services.Answers;
using QuizGame.Service.Services.Questions;
using QuizGame.Service.Services.Quizes;

namespace QuizGame.Service.Controllers;

[Route("api/quizzes")]
public class QuizController : Controller
{
    private readonly IQuestionService _questionService;

    private readonly IQuizService _quizService;

    private readonly IAnswerService _answerService;

    public QuizController(
        IQuestionService questionService,
        IQuizService quizService,
        IAnswerService answerService)
    {
        _questionService = questionService;
        _quizService = quizService;
        _answerService = answerService;
    }

    // GET api/quizzes
    [HttpGet]
    public async Task<IEnumerable<QuizResponseModel>> Get()
    {
        var result = await _quizService.GetAll();
        return result;
    }

    // GET api/quizzes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _quizService.GetDetails(id);
        if (result == null) return NotFound();
        return Json(result);
    }

    // POST api/quizzes
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] QuizCreateModel value)
    {
        var id = await _quizService.Create(value);
        return Created($"/api/quizzes/{id}", null);
    }

    // PUT api/quizzes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] QuizUpdateModel value)
    {
        if (!await _quizService.Exist(id)) return NotFound();
        await _quizService.Update(id, value);
        return NoContent();
    }

    // DELETE api/quizzes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _quizService.Delete(id);
        if (result) return NotFound();
        return NoContent();
    }

    // POST api/quizzes/5/questions
    [HttpPost]
    [Route("{id}/questions")]
    public async Task<IActionResult> PostQuestion(int id, [FromBody] QuestionCreateModel value)
    {
        if (!await _quizService.Exist(id)) return NotFound();
        var questionId = await _questionService.Create(id, value);
        return Created($"/api/quizzes/{id}/questions/{questionId}", null);
    }

    // PUT api/quizzes/5/questions/6
    [HttpPut("{id}/questions/{qid}")]
    public async Task<IActionResult> PutQuestion(int id, int qid, [FromBody] QuestionUpdateModel value)
    {
        if (!await _questionService.Exist(qid)) return NotFound();
        await _questionService.Update(id, qid, value);
        return NoContent();
    }

    // DELETE api/quizzes/5/questions/6
    [HttpDelete]
    [Route("{id}/questions/{qid}")]
    public async Task<IActionResult> DeleteQuestion(int id, int qid)
    {
        var result = await _questionService.Delete(id);
        if (result) return NotFound();
        return NoContent();
    }

    // POST api/quizzes/5/questions/6/answers
    [HttpPost]
    [Route("{id}/questions/{qid}/answers")]
    public async Task<IActionResult> PostAnswer(int id, int qid, [FromBody] AnswerCreateModel value)
    {
        var answerId = await _answerService.Create(qid, value);
        return Created($"/api/quizzes/{id}/questions/{qid}/answers/{answerId}", null);
    }

    // PUT api/quizzes/5/questions/6/answers/7
    [HttpPut("{id}/questions/{qid}/answers/{aid}")]
    public async Task<IActionResult> PutAnswer(int id, int qid, int aid, [FromBody] AnswerUpdateModel value)
    {
        if (!await _answerService.Exist(aid)) return NotFound();
        await _answerService.Update(qid, aid, value);
        return NoContent();
    }

    // DELETE api/quizzes/5/questions/6/answers/7
    [HttpDelete]
    [Route("{id}/questions/{qid}/answers/{aid}")]
    public async Task<IActionResult> DeleteAnswer(int id, int qid, int aid)
    {
        var result = await _answerService.Delete(aid);
        if (result) return NotFound();
        return NoContent();
    }
}