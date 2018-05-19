using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.DAL.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using FileSystemElement = FileSystem.BLL.DTO.FileSystemElement;

namespace FileSystem.BLL {
    public class MappingsProfile : Profile {
        public MappingsProfile() {
            CreateMap<File, FileDTO>()
                .ForMember(f => f.Data, opts => opts.MapFrom(f => f.FileData));

            CreateMap<FileDTO, File>()
                .ForMember(f => f.FileData, opts => opts.MapFrom(f => f.Data));

            CreateMap<Folder, FolderDTO>()
                .ForMember(f => f.Elements,
                           opts =>
                           opts.MapFrom(f =>
                               Mapper.Map<List<FolderDTO>>(f.Folders)
                                    .ConvertAll(fold => (FileSystemElement)fold)
                               .Concat(Mapper.Map<List<FileDTO>>(f.Files)
                                    .ConvertAll(file => (FileSystemElement)file))));

            CreateMap<FolderDTO, Folder>();

            CreateMap<User, UserDTO>();
        }
    }
}
