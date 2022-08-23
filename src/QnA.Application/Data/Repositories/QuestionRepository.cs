namespace QnA.Application.Data.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
	public QuestionRepository(QnADbContext qnADbContext) : base(qnADbContext)
	{
	}
}
