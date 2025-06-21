using Footstep.Domain.Repositories;
using Footstep.Domain.Repositories.Traces;
using Footstep.Exception;
using Footstep.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Application.UseCases.Traces.Delete
{
    public class DeleteTraceUseCase : IDeleteTraceUseCase
    {
        private readonly ITracesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTraceUseCase(ITracesWriteOnlyRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid id)
        {
            var result = await _repository.Delete(id);

            if (result == false)
            {
                throw new NotFoundException(ResourceErrorMessages.TRACE_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
