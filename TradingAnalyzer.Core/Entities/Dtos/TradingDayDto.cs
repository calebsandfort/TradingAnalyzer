﻿using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities.Dtos;

namespace TradingAnalyzer.Entities
{
    [AutoMap(typeof(TradingDay))]
    public class TradingDayDto : EntityDtoBase
    {
        [DataType(DataType.DateTime)]
        public DateTime Day { get; set; }
    }
}
