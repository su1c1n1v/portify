import { useEffect, useState } from 'react';
import type { ApiAccount, ApiPieSummary, ApiPositionsSummary } from '../services/api';
import { api } from '../services/api';
import type { AccountStats, CurrencySummary, Pie, Position } from '../types';

const CURRENCY_COLORS = ['#00c9a7', '#6c63ff', '#ffd166', '#ff8a65', '#ba68c8', '#4fc3f7'];
const PIE_COLORS = ['#6c63ff', '#00c9a7', '#ffd166', '#4fc3f7', '#ff8a65', '#ba68c8'];

function mapAccount(account: ApiAccount): AccountStats {
  const { investments } = account;
  const invested = investments.totalCost;
  return {
    totalValue: account.totalValue,
    invested,
    unrealizedPnL: investments.unrealizedProfitLoss,
    realizedPnL: investments.realizedProfitLoss,
    unrealizedPnLPct: invested ? (investments.unrealizedProfitLoss / invested) * 100 : 0,
    realizedPnLPct: invested ? (investments.realizedProfitLoss / invested) * 100 : 0,
  };
}

function mapCurrencies(summary: ApiPositionsSummary): CurrencySummary[] {
  return summary.currencies.map((c, i) => ({
    currency: c.currency as CurrencySummary['currency'],
    value: summary.totalCurrentValue * (c.percentage / 100),
    percentage: parseFloat(c.percentage.toFixed(2)),
    color: CURRENCY_COLORS[i % CURRENCY_COLORS.length],
  }));
}

function mapPositions(summary: ApiPositionsSummary, pies: ApiPieSummary[]): Position[] {
  return summary.stocks.map((s) => ({
    ticker: s.ticker,
    name: s.name,
    isin: '',
    value: s.currentValue,
    quantity: s.quantity,
    avgPrice: Number(s.averagePricePaid).toFixed(2).toString(),
    currentPrice: s.currentPrice,
    currency: s.currency as Position['currency'],
    unrealizedPnL: s.unrealizedProfitLoss,
    unrealizedPnLPct: s.unrealizedProfitLossPercentage,
    portfolioPct: s.percentage,
    pie: pies.find((p) => p.stocks.some((ps) => ps.ticker === s.ticker))?.settings.name ?? 'None',
  }));
}

function mapPies(apiPies: ApiPieSummary[]): Pie[] {
  return apiPies.map((p, i) => {
    const color = PIE_COLORS[i % PIE_COLORS.length];
    const totalUnrealizedPL = p.stocks.reduce((acc, s) => acc + s.unrealizedProfitLoss, 0);
    const estimatedInvested = p.settings.currentValue - totalUnrealizedPL;
    const returnPct = estimatedInvested > 0
      ? parseFloat(((totalUnrealizedPL / estimatedInvested) * 100).toFixed(2))
      : 0;

    return {
      id: p.settings.name as Pie['id'],
      status: 'ON_TRACK' as Pie['status'],
      invested: parseFloat(estimatedInvested.toFixed(2)),
      value: p.settings.currentValue,
      returnPct,
      dividendGained: p.settings.dividendGained,
      goalProgress: 0,
      color,
      holdings: p.stocks.map((s) => ({ ticker: s.ticker, percentage: Number(s.percentage.toFixed(1)), name: s.name })),
      chartData: p.stocks.map((s) => s.percentage),
    };
  });
}

export interface DashboardData {
  accountStats: AccountStats;
  currencies: CurrencySummary[];
  positions: Position[];
  pies: Pie[];
}

export interface DashboardState {
  data: DashboardData | null;
  loading: boolean;
  error: string | null;
  refresh: () => void;
}

export function useDashboardData(): DashboardState {
  const [data, setData] = useState<DashboardData | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [tick, setTick] = useState(0);

  useEffect(() => {
    let cancelled = false;
    setLoading(true);
    setError(null);

    // Sequential to avoid hitting Trading212 rate limits
    Promise.resolve()
      .then(() => api.getAccount())
      .then((account) => api.getPositions().then((positions) => ({ account, positions })))
      .then(({ account, positions }) => api.getPies().then((pies) => ({ account, positions, pies })))
      .then(({ account, positions, pies }) => {
        if (cancelled) return;
        setData({
          accountStats: mapAccount(account),
          currencies: mapCurrencies(positions),
          positions: mapPositions(positions,pies),
          pies: mapPies(pies),
        });
      })
      .catch((err: unknown) => {
        if (cancelled) return;
        setError(err instanceof Error ? err.message : 'Failed to load data');
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });

    return () => { cancelled = true; };
  }, [tick]);

  return { data, loading, error, refresh: () => setTick((t) => t + 1) };
}
