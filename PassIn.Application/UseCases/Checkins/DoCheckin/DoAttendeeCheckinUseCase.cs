using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Checkins.DoCheckin;
public class DoAttendeeCheckinUseCase
{
    private readonly PassInDbContext _dbcontext;

    public DoAttendeeCheckinUseCase()
    {
        _dbcontext = new PassInDbContext();
    }

    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        _dbcontext.CheckIns.Add(entity);
        _dbcontext.SaveChanges();

        return new ResponseRegisteredJson
        {

            Id = entity.Id,

        };
    }

    private void Validate(Guid attendeeId)
    {
        var existAttendee = _dbcontext.Attendees.Any(attendee => attendee.Id == attendeeId);

        if (existAttendee == false)
        {
            throw new NotFoundException("The attendeewith this id was not found");
        }

        var existCheckin = _dbcontext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);

        if (existCheckin)
        {
            throw new ConflictException("Attendee can not do checking twice in the same event.");
        }
    }
}
