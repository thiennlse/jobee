using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.Extensions.Configuration;

namespace Services.UnitOfWork
{
    public class UnitOfWork
    {
        private AccountRepository _accountRepository;
        private ApplicationRepository _applicationRepository;
        private QuestionRepository _questionRepository;
        private IConfiguration _configuration;
        private BaseRepository<User> _baseRepository;
        private JobRepository _jobRepository;
        private SubcriptionPlanRepository _subcriptionPlanRepository;
        private PaymentRepository _paymentRepository;
        private DBContext _dbContext;
        public UnitOfWork(DBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        public PaymentRepository PaymentRepository
        {
            get
            {
                if( _paymentRepository == null )
                {
                    _paymentRepository = new PaymentRepository(_dbContext);
                }
                return _paymentRepository;
            }
        }

        public SubcriptionPlanRepository SubcriptionPlanRepository
        {
            get
            {
                if( _subcriptionPlanRepository == null)
                {
                    _subcriptionPlanRepository = new SubcriptionPlanRepository(_dbContext);
                }
                return _subcriptionPlanRepository;
            }
        }

        public AccountRepository AccountRepo
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_dbContext,_configuration);
                }
                return _accountRepository;
            }
        }

        public ApplicationRepository ApplicationRepo
        {
            get
            {
                if(_applicationRepository == null)
                {
                    _applicationRepository = new ApplicationRepository(_dbContext);
                }
                return _applicationRepository;
            }
        }

        public QuestionRepository QuestionRepo
        {
            get
            {
                if(_questionRepository == null)
                {
                    _questionRepository = new QuestionRepository(_dbContext);
                }
                return _questionRepository;
            }
        }

        public JobRepository JobRepo
        {
            get
            {
                if(_jobRepository == null)
                {
                    _jobRepository = new JobRepository(_dbContext);
                }
                return _jobRepository;
            }
        }

    }
}
