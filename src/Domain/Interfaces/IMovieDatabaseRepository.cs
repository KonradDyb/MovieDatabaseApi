﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMovieDatabaseRepository
    {    
        Task<IEnumerable<Movie>> GetMovies(Guid directorId);
        Task<Movie> GetMovie(Guid directorId, Guid movieId);
        Task AddMovie(Guid directorId, Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
        Task<IEnumerable<Director>> GetDirectors();
        Task<Director> GetDirector(Guid directorId);
        Task<IEnumerable<Director>> GetDirectors(IEnumerable<Guid> directorIds);
        //IEnumerable<Director> GetDirectors(int yearOfBirth);
        Task<IEnumerable<Director>> GetDirectors(IDirectorsResourceParameters directorsResourceParameters);
        Task AddDirector(Director director);
        void DeleteDirector(Director director);
        void UpdateDirector(Director director);
        bool DirectorExists(Guid directorId);
        bool Save();
    }
}
