using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(int genreId)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == genreId);
        }

        public async Task Remove(Genre genre)
        {
            _context.Genres.Remove(genre);
        }
    }
}
