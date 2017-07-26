﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using TradingAnalyzer.Framework;
using Abp.Timing;
using TradingAnalyzer.Shared;
using TradingAnalyzer.Shared.Dtos;

namespace TradingAnalyzer.Entities.Dtos
{
    public class MonteCarloSimulationDto : EntityDtoBase
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [UIHint("MyIntStatic")]
        [Display(Name = "Sample Size")]
        public int NumberOfTradesInSample { get; set; }

        [UIHint("MyInt")]
        [Display(Name = "Trades/Iteration")]
        public int NumberOfTradesPerIteration { get; set; }

        [UIHint("MyInt")]
        [Display(Name = "Iterations")]
        public int NumberOfIterations { get; set; }

        [UIHint("MyPercentage")]
        [Display(Name = "Cumulative Profit K")]
        public Decimal CumulativeProfitK { get; set; }

        [Display(Name = "Cumulative Profit")]
        public Decimal CumulativeProfit { get; set; }

        [Display(Name = "Trading Edge")]
        public bool TradingEdge { get; set; }

        [UIHint("MyPercentage")]
        [Display(Name = "Consecutive Losses K")]
        public Decimal ConsecutiveLossesK { get; set; }

        [Display(Name = "Consecutive Losses")]
        public int ConsecutiveLosses { get; set; }

        [UIHint("MyPercentage")]
        [Display(Name = "Max Drawdown K")]
        public Decimal MaxDrawdownK { get; set; }

        [Display(Name = "Max Drawdown")]
        public Decimal MaxDrawdown { get; set; }

        [UIHint("MyCurrency")]
        [Display(Name = "Account Size")]
        public Decimal AccountSize { get; set; }

        [UIHint("MyCurrency")]
        [Display(Name = "Ruin Point")]
        public Decimal RuinPoint { get; set; }

        [UIHint("MyDecimal")]
        [Display(Name = "Max Drawdown Multiple")]
        public Decimal MaxDrawdownMultiple { get; set; }

        [UIHint("MyCurrency")]
        [Display(Name = "One Contract Funds")]
        public Decimal OneContractFunds { get; set; }

        [Display(Name = "MaxContracts")]
        public int MaxContracts { get; set; }

        [Display(Name = "Account")]
        public String TradingAccount { get; set; }
        [UIHint("TradingAccount")]
        [Display(Name = "Account")]
        public virtual int TradingAccountId { get; set; }

        public void Simulate(List<Trade> sample, IConsoleHubProxy consoleHubProxy)
        {
            Random random = new Random(Clock.Now.Millisecond);
            int sampleSize = sample.Count;
            List<MonteCarloSimulationIteration> iterations = new List<MonteCarloSimulationIteration>();

            for(int i = 0; i < this.NumberOfIterations; i++)
            {
                consoleHubProxy.WriteLine(ConsoleWriteLineInput.Create($"Simulation iteration {i+1} of {this.NumberOfIterations}"));

                MonteCarloSimulationIteration iteration = new MonteCarloSimulationIteration();
                for(int j = 0; j < this.NumberOfTradesPerIteration; j++)
                {
                    MonteCarloSimulationTrade trade = new MonteCarloSimulationTrade { NetProfit = sample[random.Next(sampleSize)].ProfitLossPerContract };
                    iteration.Trades.Add(trade);
                    trade.CumulativeProfit = iteration.Trades.Sum(x => x.NetProfit);
                    trade.Drawdown = trade.NetProfit - iteration.Trades.Max(x => x.CumulativeProfit);

                    if(trade.NetProfit < 0)
                    {
                        if(iteration.Trades.Count == 1)
                        {
                            trade.ConsecutiveLosses = 1;
                        }
                        else
                        {
                            trade.ConsecutiveLosses = iteration.Trades[iteration.Trades.Count - 2].ConsecutiveLosses + 1;
                        }
                    }
                }

                iteration.CumulativeProfit = iteration.Trades.Last().CumulativeProfit;
                iteration.MaxDrawdown = iteration.Trades.Min(x => x.Drawdown);
                iteration.ConsecutiveLosses = iteration.Trades.Max(x => x.ConsecutiveLosses);
                iterations.Add(iteration);
            }

            this.CumulativeProfit = Extensions.Percentile<Decimal>(iterations.Select(x => x.CumulativeProfit).ToList(), 1.0m - this.CumulativeProfitK);
            this.TradingEdge = this.CumulativeProfit > 0;

            this.ConsecutiveLosses = Extensions.Percentile<int>(iterations.Select(x => x.ConsecutiveLosses).ToList(), this.ConsecutiveLossesK);
            this.MaxDrawdown = Extensions.Percentile<Decimal>(iterations.Select(x => x.MaxDrawdown).ToList(), 1.0m - this.MaxDrawdownK);
        }
    }



    public class MonteCarloSimulationIteration
    {
        public Decimal CumulativeProfit { get; set; }
        public Decimal MaxDrawdown { get; set; }
        public int ConsecutiveLosses { get; set; }
        public List<MonteCarloSimulationTrade> Trades { get; set; }

        public MonteCarloSimulationIteration()
        {
            this.Trades = new List<MonteCarloSimulationTrade>();
        }
    }

    public class MonteCarloSimulationTrade
    {
        public Decimal NetProfit { get; set; }
        public Decimal CumulativeProfit { get; set; }
        public Decimal Drawdown { get; set; }
        public int ConsecutiveLosses { get; set; }

    }
}
