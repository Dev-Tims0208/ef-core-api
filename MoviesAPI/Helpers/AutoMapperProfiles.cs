using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<MovieTheater, MovieTheaterDTO>()
              .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
              .ForMember(x => x.Longtitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterCreationDTO, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longtitude, dto.Latitude))));

            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMovieGenres))
                .ForMember(x => x.MovieTheatersMovies, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));


        }

        private List<MovieGenres> MapMovieGenres(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieGenres>();

            if (movieCreationDTO.GenreIds == null)
            {
                return result;
            }

            foreach (var id in movieCreationDTO.GenreIds)
            {
                result.Add(new MovieGenres() { GenreId = id });
            }

            return result;
        }

        private List<MovieTheatersMovies> MapMovieTheatersMovies(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieTheatersMovies>();
            if (movieCreationDTO.MovieTheatersIds == null) { return result; }

            foreach (var id in movieCreationDTO.MovieTheatersIds)
            {
                result.Add(new MovieTheatersMovies() { MovieTheaterId = id });
            }

            return result;
        }

        private List<MovieActors> MapMoviesActors(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieActors>();
            if (movieCreationDTO.Actors == null) { return result; }

            foreach (var actor in movieCreationDTO.Actors)
            {
                result.Add(new MovieActors() { ActorId = actor.Id, Character = actor.Character });
            }

            return result;
        }


    }
}
