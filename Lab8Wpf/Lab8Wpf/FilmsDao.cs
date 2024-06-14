using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab8Wpf
{
    public class FilmsDao : IDao<Films>
    {
        private readonly MediaLibraryDataContext _context;

        public FilmsDao(MediaLibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Films> GetAll()
        {
            return _context.Films;
        }

        public Films GetById(int id)
        {
            return _context.Films.SingleOrDefault(f => f.FilmID == id);
        }

        public void Add(Films film)
        {
            _context.Films.InsertOnSubmit(film);
            _context.SubmitChanges();
        }

        public void Update(Films film)
        {
            var existingFilm = _context.Films.SingleOrDefault(f => f.FilmID == film.FilmID);
            if (existingFilm != null)
            {
                existingFilm.Title = film.Title;
                existingFilm.Duration = film.Duration;
                existingFilm.ReleaseDate = film.ReleaseDate;
                existingFilm.CountryID = film.CountryID;
                existingFilm.GenreID = film.GenreID;
                existingFilm.CompanyID = film.CompanyID;
                _context.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            var film = _context.Films.SingleOrDefault(f => f.FilmID == id);
            if (film != null)
            {
                _context.Films.DeleteOnSubmit(film);
                _context.SubmitChanges();
            }
        }
    }

}
