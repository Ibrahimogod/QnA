namespace QnA.Application.Services
{
    public interface IQuestionService
    {
        Task<global::System.Boolean> AddAnswerVoteAsync(Vote vote, CancellationToken cancellationToken);
        Task<global::System.Boolean> AddQuestionAnswerAsync(Answer answer, CancellationToken cancellationToken);
        Task<global::System.Boolean> AddQuestionAsync(Question question, CancellationToken cancellationToken);
        Task<global::System.Boolean> DeleteAnswerAsync(Answer answer, CancellationToken cancellationToken);
        Task<Answer> GetAnswerByIdAsync(global::System.Int32 id, CancellationToken cancellationToken);
        Task<Question> GetQuestionByIdAsync(global::System.Int32 id,CancellationToken cancellationToken);
        Task<IQueryable<Question>> GetQuestionsAsync();
    }
}