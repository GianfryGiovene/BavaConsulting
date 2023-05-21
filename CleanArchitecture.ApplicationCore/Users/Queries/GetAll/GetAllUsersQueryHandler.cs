using AutoMapper;
using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Queries.GetAll;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserReadVM>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadVM>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<User> users;        

        if(request.Filter is null)
        {
            users = await _unitOfWork.UserRepository.GetAllAsync();
        }
        else
        {
            users = await _unitOfWork.UserRepository.GetAllAsync(u => u.Email.Contains(request.Filter.Trim()));
        }

        List<UserReadVM> usersRead = new List<UserReadVM>();
        foreach( var user in users)
        {
            var userVm = UserReadVM.Create(user.Id, user.Email, user.FiscalCode, user.FirstName, user.LastName, user.BirthDay, user.CreatedAd);
            usersRead.Add(userVm);
            //usersRead.Add(_mapper.Map<UserReadVM>(user));
        }
       
        return usersRead;
    }
}
