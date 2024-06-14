using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Lab8Wpf
{


    public class MediaLibraryDataContext : DataContext
    {
        public Table<Genres> Genres;
        public Table<Countries> Countries;
        public Table<ProductionCompanies> ProductionCompanies;
        public Table<Films> Films;

        public MediaLibraryDataContext(string connection) : base(connection) { }
    }
    [Table]
    public class Genres
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int GenreID { get; set; }

        [Column]
        public string GenreName { get; set; }

        public override string ToString()
        {
            return $"GenreID: {GenreID}, GenreName: {GenreName}";
        }
    }

    [Table]
    public class Countries
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int CountryID { get; set; }

        [Column]
        public string CountryName { get; set; }

        public override string ToString()
        {
            return $"CountryID: {CountryID}, CountryName: {CountryName}";
        }
    }

    // описание таблицы company
    [Table]
    public class ProductionCompanies
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int CompanyID { get; set; }

        [Column]
        public string CompanyName { get; set; }

        public override string ToString()
        {
            return $"CompanyID: {CompanyID}, CompanyName: {CompanyName}";
        }
    }


    [Table]
    public class Films
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int FilmID { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public int Duration { get; set; }

        [Column]
        public DateTime ReleaseDate { get; set; }

        [Column]
        public int CountryID { get; set; }

        [Column]
        public int GenreID { get; set; }

        [Column]
        public int CompanyID { get; set; }

        public override string ToString()
        {
            return $"FilmID: {FilmID}, Title: {Title}, Duration: {Duration}, ReleaseDate: {ReleaseDate.Year}, CountryID: {CountryID}, GenreID: {GenreID}, CompanyID: {CompanyID}";
        }
    }
}
