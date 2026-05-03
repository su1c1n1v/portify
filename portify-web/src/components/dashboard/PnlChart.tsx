import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Tooltip,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
import { positions, CHART_COLORS } from '../../data/mock';
import Card from '../shared/Card';

ChartJS.register(CategoryScale, LinearScale, BarElement, Tooltip);

const PnlChart: React.FC = () => {
  const sorted = [...positions].sort((a, b) => b.unrealizedPnL - a.unrealizedPnL);

  const data = {
    labels: sorted.map((p) => p.ticker),
    datasets: [
      {
        label: 'Unrealized P&L (€)',
        data: sorted.map((p) => p.unrealizedPnL),
        backgroundColor: sorted.map((p) =>
          p.unrealizedPnL >= 0 ? CHART_COLORS.green : CHART_COLORS.red
        ),
        borderRadius: 5,
        borderSkipped: false,
      },
    ],
  };

  const options: any = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: { legend: { display: false } },
    scales: {
      x: { grid: { display: false }, ticks: { color: '#7a8399' } },
      y: {
        grid: { color: '#2a2f3e' },
        ticks: {
          color: '#7a8399',
          callback: (v: any) => (v >= 0 ? '+' : '') + '€' + v,
        },
      },
    },
  };

  return (
    <Card title="Unrealized P&L by Position">
      <div style={{ height: 220 }}>
        <Bar data={data} options={options} />
      </div>
    </Card>
  );
};

export default PnlChart;
