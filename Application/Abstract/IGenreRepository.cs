using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IGenreRepository
    {
        Task<Genre> GetById(int genreId);
        Task<List<Genre>> GetAll();
        Task Add(Genre genre);
    }
}
