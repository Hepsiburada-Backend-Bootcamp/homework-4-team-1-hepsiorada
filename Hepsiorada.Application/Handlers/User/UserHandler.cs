using Hepsiorada.Application.Commands.Product;
using Hepsiorada.Application.Commands.User;
using Hepsiorada.Application.Models;
using Hepsiorada.Domain.UnitOfWork;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hepsiorada.Application.Handlers.Product
{
    public class UserHandler : IRequestHandler<CreateUserCommand, Unit>,
                                  IRequestHandler<GetUsersCommand, List<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = request.Adapt<Domain.Entities.User>();

            if (userEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            //Save entities to db
            Domain.Entities.User user = await _unitOfWork.UserRepository.Add(userEntity);

            return Unit.Value;
        }

        public async Task<List<UserDTO>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.UserRepository.GetAll()).Adapt<List<UserDTO>>();
        }
    }
}
