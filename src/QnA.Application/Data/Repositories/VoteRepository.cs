namespace QnA.Application.Data.Repositories;

public class VoteRepository : BaseRepository<Vote>
{
	public VoteRepository(QnADbContext qnADbContext):base(qnADbContext)
	{
	}
}
