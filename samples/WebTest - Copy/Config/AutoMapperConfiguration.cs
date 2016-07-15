using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Config
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            BusinessEntitiesToDataContracts();
            AgentToBusinessEntities();
            AgentToDataContracts();
        }

        private static void BusinessEntitiesToDataContracts()
        {
            Mapper.CreateMap<WebTest.Entities.RegistrationType, WebTest.Models.RegistrationType>().ReverseMap();
        }

        private static void AgentToBusinessEntities()
        {

        }

        private static void AgentToDataContracts()
        {

        }
    }
}
