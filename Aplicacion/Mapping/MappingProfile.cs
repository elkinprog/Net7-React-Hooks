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
            .ForMember(a => a.Instructores, b => b.MapFrom(c => c.InstructoresLink.Select(d => d.Instructor).ToList()))
            .ForMember(a => a.Comentarios, b => b.MapFrom(c => c.ComentarioLista))
            .ForMember(a => a.Precio, b => b.MapFrom(c => c.PrecioPromocion));

            CreateMap<CursoInstructor, CursoInstructorDto>();

            CreateMap<Instructor, InstructorDto>();

            CreateMap<Precio, PrecioDto>();

            CreateMap<Usuario, UsuarioDto>();

        }



    }
}
