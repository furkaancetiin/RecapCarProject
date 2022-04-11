using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class FindexPointManager:IFindexPointService
    {
        IFindexPointDal _findexPointDal;
        public FindexPointManager(IFindexPointDal findexPointDal)
        {
            _findexPointDal = findexPointDal;
        }

        public IDataResult<FindexPoint> GetFindexPointByCustomerId(int customerId)
        {             
            return new SuccessDataResult<FindexPoint>(_findexPointDal.Get(fp => fp.CustomerId == customerId));
        }
    }
}
