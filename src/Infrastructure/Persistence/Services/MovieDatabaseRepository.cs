﻿using Domain.Entities;
using Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Services
{
    public class MovieDatabaseRepository : IMovieDatabaseRepository, IDisposable
    {
        private readonly MovieDatabaseContext _context;

        public MovieDatabaseRepository(MovieDatabaseContext context )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddMovie(Guid directorId, Movie movie)
        {
            if (directorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(directorId));
            }

            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            // always set the directorId to the passed-in directorId
            movie.DirectorId = directorId;
            _context.Movies.Add(movie); 
        }         

        public void DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
  
        public Movie GetMovie(Guid directorId, Guid movieId)
        {
            if (directorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(directorId));
            }

            if (movieId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(movieId));
            }

            return _context.Movies
              .Where(x => x.DirectorId == directorId && x.Id == movieId).FirstOrDefault();
        }

        public IEnumerable<Movie> GetMovies(Guid directorId)
        {
            if (directorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(directorId));
            }

            return _context.Movies
                        .Where(x => x.DirectorId == directorId)
                        .OrderBy(x => x.Title).ToList();
        }

        public void UpdateMovie(Movie movie)
        {
            // no code in this implementation
        }

        public void AddDirector(Director director)
        {
            if (director == null)
            {
                throw new ArgumentNullException(nameof(director));
            }

            // the repository fills the id (instead of using identity columns)
            director.Id = Guid.NewGuid();

            foreach (var movie in director.Movies)
            {
                movie.Id = Guid.NewGuid();
            }

            _context.Directors.Add(director);
        }

        public bool DirectorExists(Guid directorId)
        {
            if (directorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(directorId));
            }

            return _context.Directors.Any(x => x.Id == directorId);
        }

        public void DeleteDirector(Director director)
        {
            if (director == null)
            {
                throw new ArgumentNullException(nameof(director));
            }

            _context.Directors.Remove(director);
        }
        
        public Director GetDirector(Guid directorId)
        {
            if (directorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(directorId));
            }

            return _context.Directors.FirstOrDefault(a => a.Id == directorId);
        }

        public IEnumerable<Director> GetDirectors()
        {
            return _context.Directors.ToList<Director>();
        }
         
        public IEnumerable<Director> GetDirectors(IEnumerable<Guid> directorIds)
        {
            if (directorIds == null)
            {
                throw new ArgumentNullException(nameof(directorIds));
            }

            return _context.Directors.Where(x => directorIds.Contains(x.Id))
                .OrderBy(x => x.FirstName)
                .OrderBy(x => x.LastName)
                .ToList();
        }

        public void UpdateDirector(Director director)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
               // dispose resources when needed
            }
        }
    }
}