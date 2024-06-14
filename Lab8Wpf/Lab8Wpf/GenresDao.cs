using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Wpf
{
    public class GenresDao : IDao<Genres>
    {
        private readonly MediaLibraryDataContext _context;

        public GenresDao(MediaLibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Genres> GetAll()
        {
            return _context.Genres;
        }

        public Genres GetById(int id)
        {
            return _context.Genres.SingleOrDefault(g => g.GenreID == id);
        }

        public void Add(Genres genre)
        {
            Genres newGenre = new Genres
            {
                GenreName = genre.GenreName
            };

            _context.Genres.InsertOnSubmit(newGenre);
            _context.SubmitChanges();
        }

        public void Update(Genres genre)
        {
            var existingGenre = _context.Genres.SingleOrDefault(g => g.GenreID == genre.GenreID);
            if (existingGenre != null)
            {
                existingGenre.GenreName = genre.GenreName;
                _context.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            var genre = _context.Genres.SingleOrDefault(g => g.GenreID == id);
            if (genre != null)
            {
                _context.Genres.DeleteOnSubmit(genre);
                _context.SubmitChanges();
            }
        }
    }

}
