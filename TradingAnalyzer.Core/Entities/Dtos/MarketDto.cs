using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Entities.Dtos;

namespace TradingAnalyzer.Entities.Dtos
{
    [AutoMap(typeof(Market))]
    public class MarketDto : EntityDtoBase
    {
        public String Name { get; set; }
        public String Symbol { get; set; }

        [DataType(DataType.Currency)]
        public Double TickValue { get; set; }
        public Double TickSize { get; set; }

        [DataType(DataType.Currency)]
        public Double PointValue
        {
            get
            {
                return 1.0 / this.TickSize * this.TickValue;
            }
        }
    }
}
