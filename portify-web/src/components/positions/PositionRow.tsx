import React from 'react';
import type { Position } from '../../types';
import { PIE_COLORS } from '../../data/mock';

const CURRENCY_STYLES: Record<string, { color: string; bg: string }> = {
  USD: { color: '#6c63ff', bg: 'rgba(108,99,255,.15)' },
  EUR: { color: '#00c9a7', bg: 'rgba(0,201,167,.15)' },
  GBP: { color: '#ffd166', bg: 'rgba(255,209,102,.15)' },
  GBX: { color: '#ff8a65', bg: 'rgba(255,138,101,.15)' },
};

interface PositionRowProps {
  position: Position;
}

const PositionRow: React.FC<PositionRowProps> = ({ position: p }) => {
  const cStyle = CURRENCY_STYLES[p.currency] ?? CURRENCY_STYLES.USD;
  const pieColor = PIE_COLORS[p.pie] ?? '#6c63ff';
  const positive = p.unrealizedPnL >= 0;

  return (
    <tr className="border-b border-border hover:bg-surface2 transition-colors last:border-none">
      {/* Stock */}
      <td className="px-3 py-3">
        <div className="flex items-center gap-2.5">
          <div className="w-8 h-8 rounded-lg bg-surface2 border border-border flex items-center justify-center text-[10px] font-bold text-accent flex-shrink-0">
            {p.ticker.slice(0, 4)}
          </div>
          <div>
            <p className="text-[13px] font-semibold">{p.name}</p>
            <p className="text-[11px] text-muted">{p.isin}</p>
          </div>
        </div>
      </td>
      {/* Value */}
      <td className="px-3 py-3 text-[13px]">€{p.value.toLocaleString()}</td>
      {/* Qty */}
      <td className="px-3 py-3 text-[13px]">{p.quantity.toFixed(2)}</td>
      {/* Avg Price */}
      <td className="px-3 py-3 text-[13px]">{p.avgPrice}</td>
      {/* Curr Price */}
      <td className="px-3 py-3 text-[13px]">{p.currentPrice}</td>
      {/* Currency */}
      <td className="px-3 py-3">
        <span className="px-2 py-0.5 rounded-full text-[11px] font-semibold" style={{ color: cStyle.color, background: cStyle.bg }}>
          {p.currency}
        </span>
      </td>
      {/* P&L */}
      <td className={`px-3 py-3 text-[13px] font-semibold ${positive ? 'text-accent2' : 'text-danger'}`}>
        {positive ? '+' : ''}€{p.unrealizedPnL.toFixed(2)}
      </td>
      {/* P&L % */}
      <td className={`px-3 py-3 text-[13px] font-semibold ${positive ? 'text-accent2' : 'text-danger'}`}>
        {positive ? '+' : ''}{p.unrealizedPnLPct.toFixed(1)}%
      </td>
      {/* Portfolio % */}
      <td className="px-3 py-3">
        <div className="flex items-center gap-2 text-[12px]">
          <span>{p.portfolioPct}%</span>
          <div className="w-20 h-1 bg-border rounded-full overflow-hidden">
            <div
              className="h-full rounded-full"
              style={{ width: `${p.portfolioPct}%`, background: positive ? '#6c63ff' : '#ff5370' }}
            />
          </div>
        </div>
      </td>
      {/* Pie */}
      <td className="px-3 py-3">
        <span
          className="px-2 py-0.5 rounded-full text-[11px] font-semibold"
          style={{ color: pieColor, background: pieColor + '22' }}
        >
          {p.pie}
        </span>
      </td>
    </tr>
  );
};

export default PositionRow;
