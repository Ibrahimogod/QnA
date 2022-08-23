namespace QnA.Demo.Models;

public class LoginModel
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}

public class AddQuestionModel
{
    [Required]
    [MinLength(10)]
    public string Content { get; set; }
}

public class AddQuestionAnswerModel
{
    [Required]
    [MinLength(10)]
    public string Answer { get; set; }
}

public class AddVoteModel
{
    [Required]
    public bool? IsUpVote { get; set; }
}
