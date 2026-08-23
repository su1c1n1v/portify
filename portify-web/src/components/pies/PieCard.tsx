import { ArcElement, Chart as ChartJS } from 'chart.js';
import React from 'react';
import { Doughnut } from 'react-chartjs-2';
import type { Pie } from '../../types';

ChartJS.register(ArcElement);

function statusLabel(s: string) {
  if (s === 'AHEAD') return 'AHEAD';
  if (s === 'BEHIND') return 'BEHIND';
  return 'ON TRACK';
}

function statusCls(s: string) {
  if (s === 'AHEAD')  return 'text-accent2 bg-accent2/10';
  if (s === 'BEHIND') return 'text-danger bg-danger/10';
  return 'text-accent bg-accent/10';
}

interface PieCardProps {
  pie: Pie;
}

const PieCard: React.FC<PieCardProps> = ({ pie }) => {
  const positive = pie.returnPct >= 0;

  // Build color shades for doughnut segments
  const bgColors = pie.chartData.map((_, i) => {
    if (i === 0) return pie.color;
    const alphas = ['99', '66', '33'];
    return pie.color + (alphas[i - 1] ?? '22');
  });

  const chartData = {
    datasets: [{
      data: pie.chartData,
      backgroundColor: bgColors,
      borderWidth: 0,
      hoverOffset: 4,
    }],
  };

  const chartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    cutout: '60%',
    plugins: { legend: { display: false }, tooltip: { enabled: false } },
  };

  return (
    <div className="bg-surface border border-border rounded-card p-5 flex flex-col gap-3.5">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-2 font-bold text-[14px]">
          <span className="w-2.5 h-2.5 rounded-full" style={{ background: pie.color }} />
          {pie.id}
        </div>
        <span className={`text-[11px] font-semibold px-2 py-0.5 rounded-full ${statusCls(pie.status)}`}>
          {statusLabel(pie.status)}
        </span>
      </div>

      {/* Chart + Stats */}
      <div className="flex items-center gap-4">
        <div style={{ width: 100, height: 100, flexShrink: 0 }}>
          <Doughnut data={chartData} options={chartOptions} />
        </div>
        <div className="flex-1 flex flex-col gap-2">
          {[
            { label: 'Invested', value: `€${pie.invested.toLocaleString()}` },
            { label: 'Value',    value: `€${pie.value.toLocaleString()}` },
            { label: 'Return',   value: `${positive ? '+' : ''}${pie.returnPct}%`, colored: true },
            { label: 'Dividend', value: `€${pie.dividendGained.toFixed(2)}` },
          ].map(({ label, value, colored }) => (
            <div key={label} className="flex justify-between text-[12px]">
              <span className="text-muted">{label}</span>
              <span className={`font-semibold ${colored ? (positive ? 'text-accent2' : 'text-danger') : ''}`}>
                {value}
              </span>
            </div>
          ))}
        </div>
      </div>

      {/* Progress */}
      <div>
        <p className="text-[11px] text-muted mb-1.5">Goal Progress — {pie.goalProgress}%</p>
        <div className="h-1 bg-border rounded-full overflow-hidden">
          <div
            className="h-full rounded-full transition-all duration-300"
            style={{ width: `${pie.goalProgress}%`, background: pie.color }}
          />
        </div>
      </div>

      {/* Holdings */}
      <div className="flex flex-col gap-1">
        {pie.holdings.map((h) => (
          <div key={h.ticker} className="flex items-center text-[12px] gap-2">
            <span className="text-muted w-40 flex-shrink-0">{h.name}</span>
            <div className="flex-1 bg-border rounded-full h-[3px] overflow-hidden">
              <div
                className="h-full rounded-full"
                style={{ width: `${h.percentage}%`, background: pie.color }}
              />
            </div>
            <span className="font-semibold text-[11px] w-8 text-right">{h.percentage}%</span>
          </div>
        ))}
      </div>
    </div>
  );
};

export default PieCard;
