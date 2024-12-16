﻿using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class ReviewRepository(BookDbContext dbContext) : IReviewRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(Review entity) => await _dbContext.AddAsync(entity);

    public async Task<Review?> GetReviewByIdAsync(Guid id) => await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
