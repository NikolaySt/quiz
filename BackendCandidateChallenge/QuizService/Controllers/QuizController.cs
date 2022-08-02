using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizService.Data.Models;
using QuizService.Model.Answers;
using QuizService.Model.Questions;
using QuizService.Model.Quizes;
using QuizService.Services.Answers;
using QuizService.Services.Questions;
using QuizService.Services.Quizes;

namespace QuizService.Controllers;

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
    public async Task<QuizResponseModel> Get(int id)
    {
        var result = await _quizService.GetDetails(id);
        return result;
    }

    // POST api/quizzes
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] QuizCreateModel value)
    {
        var quiz = new Quiz()
        {
            Title = value.Title,
        };

        await _quizService.Save(quiz);

        return Created($"/api/quizzes/{quiz.Id}", null);
    }

    // PUT api/quizzes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] QuizUpdateModel value)
    {
        var quiz = await _quizService.Find(id);
        if (quiz == null) return NotFound();

        quiz.Title = value.Title;
        await _quizService.Save(quiz);

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
        var question = new Question()
        {
            QuizId = id,
            Text = value.Text
        };

        await _questionService.Save(question);

        return Created($"/api/quizzes/{id}/questions/{question.Id}", null);
    }

    // PUT api/quizzes/5/questions/6
    [HttpPut("{id}/questions/{qid}")]
    public async Task<IActionResult> PutQuestion(int id, int qid, [FromBody] QuestionUpdateModel value)
    {
        var question = await _questionService.Find(qid);
        if (question == null) return NotFound();

        question.Text = value.Text;
        question.CorrectAnswerId = value.CorrectAnswerId;
        question.QuizId = id;
        await _questionService.Save(question);

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
        var answer = new Answer()
        {
            QuestionId = qid,
            Text = value.Text
        };

        await _answerService.Save(answer);

        return Created($"/api/quizzes/{id}/questions/{qid}/answers/{answer.Id}", null);
    }

    // PUT api/quizzes/5/questions/6/answers/7
    [HttpPut("{id}/questions/{qid}/answers/{aid}")]
    public async Task<IActionResult> PutAnswer(int id, int qid, int aid, [FromBody] AnswerUpdateModel value)
    {
        var answer = await _answerService.Find(aid);
        if (answer == null) return NotFound();

        answer.Text = value.Text;
        answer.QuestionId = qid;
        answer.Text = value.Text;
        await _answerService.Save(answer);

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