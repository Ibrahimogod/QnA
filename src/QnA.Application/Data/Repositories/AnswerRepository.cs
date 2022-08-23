namespace QnA.Application.Data.Repositories;

public class AnswerRepository : BaseRepository<Answer>
{
	public AnswerRepository(QnADbContext qnADbContext) : base(qnADbContext)
	{
	}
}
