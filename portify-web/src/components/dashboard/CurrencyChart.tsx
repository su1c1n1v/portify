import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip } from 'chart.js';
import { Doughnut } from 'react-chartjs-2';
import { currencySummary } from '../../data/mock';
import Card from '../shared/Card';

ChartJS.register(ArcElement, Tooltip);

const CurrencyChart: React.FC = () => {
  const data = {
    labels: currencySummary.map((c) => c.currency),
    datasets: [
      {
        data: currencySummary.map((c) => c.percentage),
        backgroundColor: currencySummary.map((c) => c.color),
        borderWidth: 0,
        hoverOffset: 6,
      },
    ],
  };

  const options: any = {
    responsive: true,
    maintainAspectRatio: false,
    cutout: '68%',
    plugins: {
      legend: { display: false },
      tooltip: { callbacks: { label: (ctx: any) => ` ${ctx.label}: ${ctx.parsed}%` } },
    },
  };

  return (
    <Card title="Currency Exposure">
      <div className="flex items-center gap-5">
        <div style={{ width: 130, height: 130, flexShrink: 0 }}>
          <Doughnut data={data} options={options} />
        </div>
        <div className="flex flex-col gap-2.5">
          {currencySummary.map((c) => (
            <div key={c.currency} className="flex items-center gap-2 text-[13px]">
              <span className="w-2.5 h-2.5 rounded-full flex-shrink-0" style={{ background: c.color }} />
              <span className="flex-1 text-muted">{c.currency}</span>
              <span className="font-semibold">€{c.value.toLocaleString()}</span>
              <span className="text-muted text-[11px] w-9 text-right">{c.percentage}%</span>
            </div>
          ))}
        </div>
      </div>
    </Card>
  );
};

export default CurrencyChart;
