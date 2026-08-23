export type PieStatus = 'AHEAD' | 'ON_TRACK' | 'BEHIND';
export type Currency = 'USD' | 'EUR' | 'GBP'| 'CHF' | 'GBX' | string;
export type PieName = string;

export interface Position {
  ticker: string;
  name: string;
  isin: string;
  value: number;
  quantity: number;
  avgPrice: string;
  currentPrice: string;
  currency: Currency;
  unrealizedPnL: number;
  unrealizedPnLPct: number;
  portfolioPct: number;
  pie: PieName;
}

export interface CurrencySummary {
  currency: Currency;
  value: number;
  percentage: number;
  color: string;
}

export interface Holding {
  ticker: string;
  percentage: number;
  name: string;
}

export interface Pie {
  id: PieName;
  status: PieStatus;
  invested: number;
  value: number;
  returnPct: number;
  dividendGained: number;
  goalProgress: number;
  holdings: Holding[];
  chartData: number[];
  color: string;
}

export interface AccountStats {
  totalValue: number;
  invested: number;
  unrealizedPnL: number;
  realizedPnL: number;
  unrealizedPnLPct: number;
  realizedPnLPct: number;
}
