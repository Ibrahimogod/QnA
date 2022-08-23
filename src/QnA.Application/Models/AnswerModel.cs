﻿namespace QnA.Application.Models;

public class AnswerModel
{
    public int Id { get; set; }

    public string Content { get; set; }

    public int UpvotesCount { get; set; }

    public bool IsDownVoted { get; set; }

    public IEnumerable<VoteModel> Votes { get; set; }
}

