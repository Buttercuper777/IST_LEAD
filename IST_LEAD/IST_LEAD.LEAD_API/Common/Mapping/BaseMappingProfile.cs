using AutoMapper;
using IST_LEAD.Core.Models;
using IST_LEAD.DAL.Entities;

namespace IST_LEAD.LEAD_API.Common.Mapping
{
    public class BaseMappingProfile : Profile
    {

        public BaseMappingProfile()
        {
            CreateMap <UploadedFile, ExcelEntity>()
                .ForMember(x=> x.Id, o => o.Ignore())
                .ForMember(x => x.IsActive, o => o.Ignore());   
        }


    }
}
