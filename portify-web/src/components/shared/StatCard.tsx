import React from 'react';

interface StatCardProps {
  label: string;
  value: string;
  change: string;
  positive?: boolean;
}

const StatCard: React.FC<StatCardProps> = ({ label, value, change, positive }) => (
  <div className="bg-surface border border-border rounded-card p-5">
    <p className="text-xs text-muted uppercase tracking-wider mb-2">{label}</p>
    <p className="text-2xl font-bold tracking-tight">{value}</p>
    <span
      className={`inline-flex items-center gap-1 text-xs font-semibold mt-2 px-2 py-0.5 rounded-full ${
        positive
          ? 'text-accent2 bg-accent2/10'
          : 'text-danger bg-danger/10'
      }`}
    >
      {change}
    </span>
  </div>
);

export default StatCard;
