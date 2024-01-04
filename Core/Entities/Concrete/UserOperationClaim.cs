using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim : IEntity
    {
        public Guid UserOperationClaimId { get; set; }
        public Guid UserId { get; set; }
        public Guid OperationClaimId { get; set; }
    }
}
