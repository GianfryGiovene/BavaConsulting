using AutoMapper;
using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Queries.GetById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserReadVM>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserReadVM> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Value is null || request.Value.GetType() != typeof(UserId)) throw new Exception();

        var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id == request.Value);

        if (user == null) throw new Exception();

        var result = UserReadVM.Create(user.Id, user.Email, user.FiscalCode,user.FirstName, user.LastName, user.BirthDay, user.CreatedAd); 

        return result;
    }
}
