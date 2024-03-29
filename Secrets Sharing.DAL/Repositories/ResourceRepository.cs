﻿using Microsoft.EntityFrameworkCore;
using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.Hasher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.DAL.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Resource entity)
        {
            await _context.Resources.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Resource entity)
        {
            _context.Resources.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Resource> Get(int id)
        {
            return await _context.Resources.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Resource>> GetAll()
        {
            return await _context.Resources.Include(r => r.User).ToListAsync();
        }
    }
}
