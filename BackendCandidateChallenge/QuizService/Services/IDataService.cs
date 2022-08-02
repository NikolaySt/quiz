﻿using System.Threading.Tasks;

namespace QuizService.Services
{
    public interface IDataService<in TEntity>
        where TEntity : class
    {
        Task Save(TEntity entity);
    }
}