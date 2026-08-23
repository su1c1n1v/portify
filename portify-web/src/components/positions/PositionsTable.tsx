import React, { useMemo, useState } from 'react';
import type { Currency, Position } from '../../types';
import Card from '../shared/Card';
import PositionRow from './PositionRow';

interface Props { positions: Position[]; pieNames: string[]; }

type SortKey = 'value' | 'unrealizedPnL' | 'unrealizedPnLPct' | 'portfolioPct' | 'quantity';
type SortDir = 'asc' | 'desc';

const COLUMNS: { key: SortKey | null; label: string }[] = [
  { key: null,              label: 'Stock' },
  { key: 'value',           label: 'Value' },
  { key: 'quantity',        label: 'Qty' },
  { key: null,              label: 'Avg Price' },
  { key: null,              label: 'Curr Price' },
  { key: null,              label: 'Currency' },
  { key: 'unrealizedPnL',   label: 'Unreal. P&L' },
  { key: 'unrealizedPnLPct',label: 'P&L %' },
  { key: 'portfolioPct',    label: 'Portfolio %' },
  { key: null,              label: 'Pie' },
];

const PAGE_SIZE = 10;

const SearchIcon = () => (
  <svg className="absolute left-2.5 text-muted pointer-events-none" width="14" height="14" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/>
  </svg>
);

const PositionsTable: React.FC<Props> = ({ positions: allPositions, pieNames }) => {
  const [search, setSearch]       = useState('');
  const [currency, setCurrency]   = useState<string>('All');
  const [pie, setPie]             = useState<string>('All');
  const [pnlFilter, setPnlFilter] = useState<string>('All');
  const [sortKey, setSortKey]     = useState<SortKey>('value');
  const [sortDir, setSortDir]     = useState<SortDir>('desc');
  const [page, setPage]           = useState(1);

  const handleSort = (key: SortKey | null) => {
    if (!key) return;
    if (sortKey === key) setSortDir((d) => (d === 'desc' ? 'asc' : 'desc'));
    else { setSortKey(key); setSortDir('desc'); }
    setPage(1);
  };

  const filtered = useMemo(() => {
    return allPositions
      .filter((p) => {
        const q = search.toLowerCase();
        if (q && !p.ticker.toLowerCase().includes(q) && !p.name.toLowerCase().includes(q)) return false;
        if (currency !== 'All' && p.currency !== currency) return false;
        if (pie !== 'All' && p.pie !== pie) return false;
        if (pnlFilter === 'Positive' && p.unrealizedPnL < 0) return false;
        if (pnlFilter === 'Negative' && p.unrealizedPnL >= 0) return false;
        return true;
      })
      .sort((a, b) => {
        const mul = sortDir === 'desc' ? -1 : 1;
        return (a[sortKey] - b[sortKey]) * mul;
      });
  }, [search, currency, pie, pnlFilter, sortKey, sortDir, allPositions]);

  const totalPages = Math.max(1, Math.ceil(filtered.length / PAGE_SIZE));
  const paged = filtered.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE);

  const selectCls = "bg-surface2 border border-border rounded-lg text-[#e2e8f0] px-3 py-1.5 text-[13px] outline-none cursor-pointer";

  return (
    <Card
      title="All Positions"
      headerRight={
        <div className="flex gap-2 flex-wrap">
          <div className="relative flex items-center">
            <SearchIcon />
            <input
              type="text"
              value={search}
              onChange={(e) => { setSearch(e.target.value); setPage(1); }}
              placeholder="Search ticker or name…"
              className="bg-surface2 border border-border rounded-lg text-[#e2e8f0] pl-8 pr-3 py-1.5 text-[13px] w-48 outline-none"
            />
          </div>
          <select className={selectCls} value={currency} onChange={(e) => { setCurrency(e.target.value); setPage(1); }}>
            <option value="All">All Currencies</option>
            {(['USD','EUR','GBP','GBX'] as Currency[]).map((c) => <option key={c}>{c}</option>)}
          </select>
          <select className={selectCls} value={pie} onChange={(e) => { setPie(e.target.value); setPage(1); }}>
            <option value="All">All Pies</option>
            {pieNames.map((p) => (
              <option key={p}>{p}</option>
            ))}
          </select>
          <select className={selectCls} value={pnlFilter} onChange={(e) => { setPnlFilter(e.target.value); setPage(1); }}>
            <option value="All">P&L: All</option>
            <option value="Positive">Positive only</option>
            <option value="Negative">Negative only</option>
          </select>
        </div>
      }
    >
      <div className="overflow-x-auto">
        <table className="w-full border-collapse">
          <thead>
            <tr>
              {COLUMNS.map((col) => (
                <th
                  key={col.label}
                  onClick={() => handleSort(col.key)}
                  className={`px-3 py-2.5 text-left text-[11px] uppercase tracking-wider bg-surface2 border-b border-border whitespace-nowrap first:rounded-tl-lg last:rounded-tr-lg select-none ${
                    col.key ? 'cursor-pointer hover:text-[#e2e8f0]' : ''
                  } ${sortKey === col.key ? 'text-accent' : 'text-muted'}`}
                >
                  {col.label}
                  {col.key && (
                    <span className={`ml-1 opacity-50 ${sortKey === col.key ? 'opacity-100 text-accent' : ''}`}>
                      {sortKey === col.key ? (sortDir === 'desc' ? '↓' : '↑') : '↕'}
                    </span>
                  )}
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {paged.map((p) => <PositionRow key={p.ticker} position={p} />)}
          </tbody>
        </table>
      </div>

      {/* Pagination */}
      <div className="flex justify-between items-center text-muted text-[12px]">
        <span>Showing {paged.length} of {filtered.length} positions</span>
        <div className="flex gap-1.5">
          <button
            onClick={() => setPage((p) => Math.max(1, p - 1))}
            disabled={page === 1}
            className="px-2.5 py-1 rounded-lg bg-surface2 border border-border text-[12px] font-semibold disabled:opacity-40"
          >
            ← Prev
          </button>
          {Array.from({ length: totalPages }, (_, i) => i + 1).map((n) => (
            <button
              key={n}
              onClick={() => setPage(n)}
              className={`px-2.5 py-1 rounded-lg border text-[12px] font-semibold ${
                page === n
                  ? 'bg-accent text-white border-accent'
                  : 'bg-surface2 text-[#e2e8f0] border-border'
              }`}
            >
              {n}
            </button>
          ))}
          <button
            onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
            disabled={page === totalPages}
            className="px-2.5 py-1 rounded-lg bg-surface2 border border-border text-[12px] font-semibold disabled:opacity-40"
          >
            Next →
          </button>
        </div>
      </div>
    </Card>
  );
};

export default PositionsTable;
