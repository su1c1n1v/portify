import React from 'react';
import CurrencyChart from '../components/dashboard/CurrencyChart';
import PnlChart from '../components/dashboard/PnlChart';
import TopPositionsChart from '../components/dashboard/TopPositionsChart';
import TrendChart from '../components/dashboard/TrendChart';
import PiesGrid from '../components/pies/PiesGrid';
import PositionsTable from '../components/positions/PositionsTable';
import StatCard from '../components/shared/StatCard';
import { accountStats as mockAccountStats } from '../data/mock';
import { useDashboardData } from '../hooks/useDashboardData';

const fmt = (n: number) =>
  '€' + Math.abs(n).toLocaleString('en', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

const Dashboard: React.FC = () => {
  const { data, loading, error, refresh } = useDashboardData();
  const stats = data?.accountStats ?? mockAccountStats;
  const currencies = data?.currencies ?? [];
  const positions = data?.positions ?? [];
  const pies = data?.pies ?? [];
  const pieNames = Array.from(new Set(positions.map((p) => p.pie)));

  return (
  <main className="flex-1 overflow-x-hidden p-7 flex flex-col gap-5">
    {/* Page Header */}
    <div className="flex items-center justify-between">
      <div>
        <h1 className="text-[22px] font-bold tracking-tight">Portfolio Dashboard</h1>
        <p className="text-[13px] text-muted mt-0.5">
          {loading ? 'Loading…' : error ? `Error: ${error}` : `Last updated: ${new Date().toLocaleString('en-GB', { dateStyle: 'medium', timeStyle: 'short' })} UTC`}
        </p>
      </div>
      <div className="flex gap-2.5">
        <button onClick={refresh} className="px-4 py-2 rounded-lg bg-surface2 text-[#e2e8f0] border border-border text-[13px] font-semibold hover:opacity-85 transition-opacity">
          ↻ Refresh
        </button>
        <button className="px-4 py-2 rounded-lg bg-accent text-white text-[13px] font-semibold hover:opacity-85 transition-opacity">
          + New Position
        </button>
      </div>
    </div>

    {/* Stat Cards */}
    <div className="grid grid-cols-4 gap-5">
      <StatCard
        label="Total Value"
        value={fmt(stats.totalValue)}
        change={`▲ ${stats.unrealizedPnLPct.toFixed(1)}% all time`}
        positive
      />
      <StatCard
        label="Invested"
        value={fmt(stats.invested)}
        change={`+${fmt(stats.unrealizedPnL)} gain`}
        positive
      />
      <StatCard
        label="Unrealized P&L"
        value={`+${fmt(stats.unrealizedPnL)}`}
        change={`▲ +${stats.unrealizedPnLPct.toFixed(2)}%`}
        positive
      />
      <StatCard
        label="Realized P&L"
        value={`${stats.realizedPnL < 0 ? '−' : '+'}${fmt(stats.realizedPnL)}`}
        change={`${stats.realizedPnLPct < 0 ? '▼' : '▲'} ${stats.realizedPnLPct.toFixed(2)}%`}
        positive={stats.realizedPnL >= 0}
      />
    </div>

    {/* Trend + Currency */}
    <div className="grid grid-cols-[2fr_1fr] gap-5">
      <TrendChart />
      <CurrencyChart currencies={currencies} />
    </div>

    {/* Top Positions + P&L */}
    <div className="grid grid-cols-2 gap-5">
      <TopPositionsChart positions={positions} />
      <PnlChart positions={positions} />
    </div>

    {/* Positions Table */}
    <PositionsTable positions={positions} pieNames={pieNames} />

    {/* Pies Section */}
    <div className="flex items-center gap-3 text-[16px] font-bold">
      My Pies
      <span className="flex-1 h-px bg-border" />
    </div>
    <PiesGrid pies={pies} />
  </main>
  );
};

export default Dashboard;
