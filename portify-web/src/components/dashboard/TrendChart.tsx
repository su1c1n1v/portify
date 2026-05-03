import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Filler,
  Tooltip,
} from 'chart.js';
import { Line } from 'react-chartjs-2';
import { trendData } from '../../data/mock';
import Card from '../shared/Card';

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Filler, Tooltip);

const TIME_RANGES = ['1W', '1M', '3M', '1Y', 'All'];

const TrendChart: React.FC = () => {
  const data = {
    labels: trendData.labels,
    datasets: [
      {
        label: 'Portfolio Value (€)',
        data: trendData.values,
        borderColor: '#6c63ff',
        backgroundColor: (ctx: any) => {
          const g = ctx.chart.ctx.createLinearGradient(0, 0, 0, 200);
          g.addColorStop(0, 'rgba(108,99,255,0.3)');
          g.addColorStop(1, 'rgba(108,99,255,0)');
          return g;
        },
        borderWidth: 2.5,
        pointRadius: 4,
        pointBackgroundColor: '#6c63ff',
        tension: 0.4,
        fill: true,
      },
    ],
  };

  const options: any = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      tooltip: { mode: 'index', intersect: false },
    },
    scales: {
      x: { grid: { display: false }, ticks: { color: '#7a8399' } },
      y: {
        grid: { color: '#2a2f3e' },
        ticks: { color: '#7a8399', callback: (v: any) => '€' + v.toLocaleString() },
      },
    },
  };

  const headerRight = (
    <div className="flex gap-1.5">
      {TIME_RANGES.map((r) => (
        <button
          key={r}
          className={`px-2.5 py-1 rounded-lg text-[11px] font-semibold border transition-opacity ${
            r === '1M'
              ? 'bg-accent text-white border-accent'
              : 'bg-surface2 text-[#e2e8f0] border-border hover:opacity-85'
          }`}
        >
          {r}
        </button>
      ))}
    </div>
  );

  return (
    <Card title="Portfolio Value Over Time" headerRight={headerRight}>
      <div style={{ height: 200 }}>
        <Line data={data} options={options} />
      </div>
    </Card>
  );
};

export default TrendChart;
