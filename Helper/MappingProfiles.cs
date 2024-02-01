using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Models;

namespace FamAppAPI.Helper
{
    /// <summary>
    /// The mapping profiles.
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfiles"/> class.
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Groups, GroupsDto>();
            CreateMap<GroupsDto, Groups>();

            CreateMap<Calendar, CalendarDto>();
            CreateMap<CalendarDto, Calendar>();

            CreateMap<Date, DateDto>();
            CreateMap<DateDto, Date>();

            CreateMap<UserInGroup, UserInGroupDto>();
            CreateMap<UserInGroupDto, UserInGroup>();
            CreateMap<UserInGroup, UserDto>();
            CreateMap<UserInGroup, GroupsDto>();
        }
    }
}
