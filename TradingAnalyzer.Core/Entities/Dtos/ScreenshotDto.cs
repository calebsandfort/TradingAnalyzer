using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
using TradingAnalyzer.Framework;
using Abp.Application.Services.Dto;

namespace TradingAnalyzer.Entities.Dtos
{
    [AutoMap(typeof(Screenshot))]
    public class ScreenshotDto : EntityDtoBase
    {
        public byte[] Data { get; set; }
    }
}
