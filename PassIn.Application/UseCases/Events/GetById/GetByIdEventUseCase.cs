﻿using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetByIdEventUseCase
{
    public ResponseEventJson Execute(Guid id)
    {
        var dbContext = new PassInDbContext();

        var entity = dbContext.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == id);
        //var entity = dbContext.Events.Find(id);

        if(entity is null)
            throw new NotFoundException("An event with this id dont exist.");

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = entity.Attendees.Count(),
        };
    }
}
