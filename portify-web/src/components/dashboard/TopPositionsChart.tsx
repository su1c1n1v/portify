import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Tooltip,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
import { positions } from '../../data/mock';
import { CHART_COLORS } from '../../data/mock';
import Card from '../shared/Card';

ChartJS.register(CategoryScale, LinearScale, BarElement, Tooltip);

const PIE_BAR_COLORS: Record<string, string> = {
  Technology:  CHART_COLORS.accent,
  ETFs:        CHART_COLORS.blue,
  Health:      CHART_COLORS.green,
  Commodities: CHART_COLORS.purple,
  Consumer:    CHART_COLORS.orange,
  Industry:    CHART_COLORS.yellow,
};

const TopPositionsChart: React.FC = () => {
  const top = [...positions].sort((a, b) => b.value - a.value).slice(0, 8);

  const data = {
    labels: top.map((p) => p.ticker),
    datasets: [
      {
        label: 'Value (€)',
        data: top.map((p) => p.value),
        backgroundColor: top.map((p) => PIE_BAR_COLORS[p.pie] ?? CHART_COLORS.accent),
        borderRadius: 6,
        borderSkipped: false,
      },
    ],
  };

  const options: any = {
    responsive: true,
    maintainAspectRatio: false,
    indexAxis: 'y',
    plugins: { legend: { display: false } },
    scales: {
      x: {
        grid: { color: '#2a2f3e' },
        ticks: { color: '#7a8399', callback: (v: any) => '€' + v.toLocaleString() },
      },
      y: { grid: { display: false }, ticks: { color: '#7a8399' } },
    },
  };

  return (
    <Card title="Top Positions by Value">
      <div style={{ height: 220 }}>
        <Bar data={data} options={options} />
      </div>
    </Card>
  );
};

export default TopPositionsChart;
