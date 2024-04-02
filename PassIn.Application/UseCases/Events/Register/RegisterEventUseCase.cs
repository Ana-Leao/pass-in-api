using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validate(request);
    }

    public void Validate(RequestEventJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("The title is invalid");
        }

        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ArgumentException("The details are invalid");
        }

        if(request.MaximumAttendees <= 0)
        {
            throw new ArgumentException("The maximun attendes is invalid");
        }
    }
}
