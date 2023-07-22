using Aplicacion.Cursos;
using AutoMapper;
using Dominio.Dtos;
using Dominio.Models;

namespace Aplicacion.Mapping
{
    public  class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<Comentario, ComentarioDto>();

            CreateMap<Curso, CursoDto>()
            .ForMember(x => x.Instructores, y => y
            .MapFrom(z => z.InstructoresLink
            .Select(a=>a.Instructor)
            .ToList()));

            CreateMap<CursoInstructor, CursoInstructorDto>();

            CreateMap<Instructor, InstructorDto>();

            CreateMap<Precio, PrecioDto>();

            CreateMap<Usuario, UsuarioDto>();

        }



    }
}
