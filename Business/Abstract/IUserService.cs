using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        DataResult<List<User>> GetAll();
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByUsername(string username);

        IDataResult<User> GetById(int userId);


    }
}
