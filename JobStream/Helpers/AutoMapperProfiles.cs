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

      CreateMap<JobBlock, JobBlockDto>().ReverseMap();

      CreateMap<JobProcess, JobProcessDto>().ReverseMap();

      CreateMap<JobProcessHistory, JobProcessHistoryDto>();

      CreateMap<JobResult, JobResultDto>();
    }
  }
}
