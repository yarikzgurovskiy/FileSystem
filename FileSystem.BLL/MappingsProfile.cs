using AutoMapper;
using FileSystem.BLL.DTO;
using FileSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.BLL {
    public class MappingsProfile : Profile {
        public MappingsProfile() {
            CreateMap<File, FileDTO>()
                .ForMember(f => f.Data, opts => opts.MapFrom(f => f.FileData));
            CreateMap<FileDTO, File>()
                .ForMember(f => f.FileData, opts => opts.MapFrom(f => f.Data));
            CreateMap<Folder, FolderDTO>();
            CreateMap<FolderDTO, Folder>();
        }
    }
}
