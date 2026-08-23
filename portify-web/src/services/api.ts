// ---- Raw API response shapes ----

export interface ApiAccount {
  id: number;
  currency: string;
  totalValue: number;
  cash: {
    availableToTrade: number;
    inPies: number;
    reservedForOrders: number;
  };
  investments: {
    currentValue: number;
    realizedProfitLoss: number;
    totalCost: number;
    unrealizedProfitLoss: number;
  };
}

export interface ApiCurrencySummary {
  currency: string;
  quantity: number;
  percentage: number;
}

export interface ApiStockSummary {
  name: string;
  ticker: string;
  currentValue: number;
  quantity: number;
  currency: string;
  unrealizedProfitLoss: number;
  unrealizedProfitLossPercentage: number;
  portfolioProfitLossPercentage: number;
  percentage: number;
  averagePricePaid: string;
  currentPrice: string;
  pie: string;
}

export interface ApiPositionsSummary {
  totalStocks: number;
  totalCurrentValue: number;
  unrealizedTotalProfitLoss: number;
  currencies: ApiCurrencySummary[];
  stocks: ApiStockSummary[];
}

export interface ApiSettingSummary {
  id: number;
  name: string;
  creationDate?: number;
  currentValue: number;
  percentage: number;
  dividendCashAction: string;
  dividendGained: number;
}

export interface ApiPieSummary {
  settings: ApiSettingSummary;
  stocks: ApiStockSummary[];
}

// ---- Fetch functions ----

async function fetchJson<T>(path: string): Promise<T> {
  const res = await fetch(path);
  if (!res.ok) throw new Error(`API error ${res.status}: ${path}`);
  return res.json() as Promise<T>;
}

export const api = {
  getAccount: () => fetchJson<ApiAccount>('/api/accounts'),
  getPositions: () => fetchJson<ApiPositionsSummary>('/api/positions'),
  getPies: () => fetchJson<ApiPieSummary[]>('/api/pies'),
};
