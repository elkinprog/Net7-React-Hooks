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
            CreateMap<Curso, CursoDto>()
            .ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructoresLink.Select(a=>a.Instructor).ToList()));

            CreateMap<Instructor, InstructorDto>();
            CreateMap<CursoInstructor, CursoInstructorDto>(); 
            CreateMap<Usuario, UsuarioDto>();

        }



    }
}
