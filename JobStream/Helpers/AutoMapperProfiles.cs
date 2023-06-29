using AutoMapper;
using JobStream.DTOs;
using JobStream.Entities;

namespace JobStream.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<Job, JobDto>().ReverseMap();

      CreateMap<JobBlock, JobBlockDto>();
      CreateMap<JobBlockDto, JobBlock>()
        .ForMember(jb => jb.ConditionBlock, t => t.Ignore())
        .ForMember(jb => jb.IfBlock, t => t.Ignore())
        .ForMember(jb => jb.ElseBlock, t => t.Ignore())
        .ForMember(jb => jb.Jobs, t => t.Ignore());

      CreateMap<JobProcess, JobProcessDto>().ReverseMap();

      CreateMap<JobProcessHistory, JobProcessHistoryDto>();

      CreateMap<JobResult, JobResultDto>();
    }
  }
}
