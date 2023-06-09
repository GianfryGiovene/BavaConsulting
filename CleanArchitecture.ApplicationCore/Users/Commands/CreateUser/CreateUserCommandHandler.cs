﻿using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Exceptions;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _unitOfWork.UserRepository.AnyAsync(u => u.Email.Equals(request.Email.ToLower()))) throw new EntityAlreadyExistsException("Email già registrata.");
            var user = User.Create(request.Email
                , request.Password
                , request.FiscalCode
                , request.FirstName
                , request.LastName
                , request.BirthDay);

            await _unitOfWork.UserRepository.CreateAsync(user);

            await _unitOfWork.CommitAsync();
            return user;
        }
        catch
        {
            _unitOfWork.Dispose();
            throw new Exception();
        }        
    }
}